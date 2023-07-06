using TMPro;
using UnityEngine;

public class UITrackTime : MonoBehaviour, IDependancy<RaceTimeTracker>, IDependancy<RaceStateTracker>
{
    [SerializeField] private TextMeshProUGUI text;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker dependency) => raceStateTracker = dependency;
    
    private RaceTimeTracker timeTracker;
    public void Construct(RaceTimeTracker dependency) => timeTracker = dependency;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void Update()
    {
        text.text = StringTime.SecondToTimeSpring(timeTracker.CurrentTime);
    }

    private void OnRaceCompleted()
    {
        text.enabled = false;
    }

    private void OnRaceStarted()
    {
        text.enabled = true;
    }
}
