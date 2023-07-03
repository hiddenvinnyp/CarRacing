using UnityEngine;
using UnityEngine.Events;

public enum TrackType
{
    Circular,
    Sprint
}

public class TrackpointCircuit : MonoBehaviour
{
    public event UnityAction<int> LapComplited;
    public event UnityAction<TrackPoint> TrackPointTriggered;

    [SerializeField] private TrackType trackType;
    [SerializeField] private TrackPoint[] points;

    private int lapsComplited = -1;

    private void Start()
    {
        LapComplited += (t) => Debug.Log("lap compl");
        foreach (var point in points)
        {
            point.Triggered += OnTrackPointTriggered;
        }

        points[0].AssignAsTarget();
    }

    private void OnDestroy()
    {
        foreach (var point in points)
        {
            point.Triggered -= OnTrackPointTriggered;
        }
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        if (trackPoint.IsTarget == false) return;

        trackPoint.Passed();
        trackPoint.NextNode?.AssignAsTarget();

        TrackPointTriggered?.Invoke(trackPoint);

        if (trackPoint.IsLast)
        {
            lapsComplited++;
            if (trackType == TrackType.Sprint)
            {
                LapComplited?.Invoke(lapsComplited);
            }

            if (trackType == TrackType.Circular)
            {
                if (lapsComplited > 0)
                {
                    LapComplited?.Invoke(lapsComplited);
                }
            }
        }
    }
}
