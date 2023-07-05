using System;
using UnityEngine;

public class RaceInputController : MonoBehaviour, IDependancy<RaceStateTracker>, IDependancy<CarInputControl>
{
    private CarInputControl carInputControl;
    private RaceStateTracker raceStateTracker;

    public void Construct(RaceStateTracker dependency) => raceStateTracker = dependency;
    public void Construct(CarInputControl dependency) => carInputControl = dependency;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceFinished;

        carInputControl.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceFinished;
    }

    private void OnRaceStarted()
    {
        carInputControl.enabled = true;
    }

    private void OnRaceFinished()
    {
        carInputControl.Stop();
        carInputControl.enabled = false;
    }
}
