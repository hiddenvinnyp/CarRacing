using System;
using TMPro;
using UnityEngine;

public class UICountDownTimer : MonoBehaviour, IDependancy<RaceStateTracker>
{

    [SerializeField] private TextMeshProUGUI text;

    private Timer countdownTimer;
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker dependency) => raceStateTracker = dependency;

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Started += OnRaceStarted;

        text.enabled = false;
    }

    private void Update()
    {
        text.text = raceStateTracker.CountdownTimer.Value.ToString("F0");

        if (text.text == "0")
            text.text = "GO!";
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnRaceStarted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void OnPreparationStarted()
    {
        text.enabled = true;
        enabled = true;
    }
}
