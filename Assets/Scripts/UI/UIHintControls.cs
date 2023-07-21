using System;
using UnityEngine;

public class UIHintControls : UIHint, IDependancy<RaceStateTracker>
{
    private RaceStateTracker stateTracker;
    public void Construct(RaceStateTracker dependency) => stateTracker = dependency;

    protected override void Start()
    {
        base.Start();
        gameObject.SetActive(false);
        stateTracker.PreparationStarted += OnPreparationStarted;
        stateTracker.Started += OnRaceStarted;
    }

    private void OnDestroy()
    {
        stateTracker.PreparationStarted -= OnPreparationStarted;
        stateTracker.Started -= OnRaceStarted;
    }

    private void OnRaceStarted()
    {
        gameObject.SetActive(false);
    }

    private void OnPreparationStarted()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.W))
            gameObject.SetActive(false);
    }
}
