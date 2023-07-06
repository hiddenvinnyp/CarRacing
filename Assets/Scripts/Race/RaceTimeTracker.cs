using UnityEngine;

public class RaceTimeTracker : MonoBehaviour, IDependancy<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker dependency) => raceStateTracker = dependency;

    private float currentTime;
    public float CurrentTime => currentTime;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }

    private void OnRaceStarted()
    {
        enabled = true;
        currentTime = 0;
    }

    private void OnRaceCompleted()
    {
        enabled = false;
    }
}
