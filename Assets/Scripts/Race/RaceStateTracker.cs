using System;
using UnityEngine;
using UnityEngine.Events;

public enum RaceState
{
    Preparetion,
    CountDown,
    Race,
    Passed
}

public class RaceStateTracker : MonoBehaviour, IDependancy<TrackpointCircuit>
{
    public event UnityAction PreparationStarted; // Подготовка к старту, таймер
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapCompleted;

    [SerializeField] private Timer countdownTimer;
    [SerializeField] private int lapsToComplete;
    public int LapsToComplete => lapsToComplete;

    private TrackpointCircuit trackpointCircuit;
    public void Construct(TrackpointCircuit trackpointCircuit) =>
        this.trackpointCircuit = trackpointCircuit;

    private RaceState state;
    public RaceState State => state;
    public Timer CountdownTimer => countdownTimer;

    private void Start()
    {
        StartState(RaceState.Preparetion);
        countdownTimer.enabled = false;

        countdownTimer.Finished += OnCountdownTimerFinished;
        trackpointCircuit.TrackPointTriggered += OnTrackPointTriggered;
        trackpointCircuit.LapComplited += OnLapComplited;
    }


    private void OnDestroy()
    {
        countdownTimer.Finished -= OnCountdownTimerFinished;
        trackpointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
        trackpointCircuit.LapComplited -= OnLapComplited;
    }

    private void StartState(RaceState state)
    {
        this.state = state;
    }

    private void OnCountdownTimerFinished()
    {
        StartRace();
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackPointPassed?.Invoke(trackPoint);
    }

    private void OnLapComplited(int lapAmount)
    {
        if (trackpointCircuit.Type == TrackType.Sprint)
        {
            CompleteRace();
        }

        if (trackpointCircuit.Type == TrackType.Circular)
        {
            if (lapAmount == lapsToComplete)
                CompleteRace();
            else
                CompleteLap(lapAmount);
        }
    }

    public void LaunchPreparationStart()
    {
        if (state != RaceState.Preparetion) return;
        StartState(RaceState.CountDown);

        countdownTimer.enabled = true;
        PreparationStarted?.Invoke();
    }

    private void StartRace()
    {
        if (state != RaceState.CountDown) return;
        StartState(RaceState.Race);

        Started?.Invoke();
    }

    private void CompleteRace()
    {
        if (state != RaceState.Race) return;
        StartState(RaceState.Passed);

        Completed?.Invoke();
    }

    private void CompleteLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }
}
