using System;
using UnityEngine;

public class RaceInputController : MonoBehaviour
{
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private RaceStateTracker raceStateTracker;

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
