using TMPro;
using UnityEngine;

public class UILap : MonoBehaviour, IDependancy<RaceStateTracker>
{
    [SerializeField] private TextMeshProUGUI text;

    private int lapsToComplete;
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker dependency) => raceStateTracker = dependency;

    private void Start()
    {
        if (FindObjectOfType<TrackpointCircuit>().Type == TrackType.Sprint)
        {
            text.enabled = false;
            return;
        }

        raceStateTracker.LapCompleted += OnLapCompleted;
        lapsToComplete = raceStateTracker.LapsToComplete;
        text.text = $"0/{lapsToComplete}";
    }

    private void OnDestroy()
    {
        raceStateTracker.LapCompleted -= OnLapCompleted;
    }

    private void OnLapCompleted(int lapAmount)
    {
        text.text = $"{lapAmount}/{lapsToComplete}";
    }
}
