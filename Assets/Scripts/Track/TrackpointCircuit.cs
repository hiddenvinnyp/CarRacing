using System;
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
    private TrackPoint[] points;

    private int lapsComplited = -1;

    private void Start()
    {
        BuildCircuit();

        foreach (var point in points)
        {
            point.Triggered += OnTrackPointTriggered;
        }

        points[0].AssignAsTarget();
    }

    [ContextMenu(nameof(BuildCircuit))]
    private void BuildCircuit()
    {
        points = new TrackPoint[transform.childCount];

        for (int i = 0; i < points.Length; i++) 
        {
            points[i] = transform.GetChild(i).GetComponent<TrackPoint>();

            if (points[i] == null)
            {
                Debug.LogError($"No Trackpoint script on {points[i].name} onject");
                return;
            }

            points[i].ResetPoint();
        }

        for (int i = 0; i < points.Length - 1; i++)
        {
            points[i].NextNode = points[i + 1];
        }

        if (trackType == TrackType.Circular)
        { 
            points[points.Length - 1].NextNode = points[0];
            points[0].IsLast = true;
        }

        points[0].IsFirst = true;

        if (trackType == TrackType.Sprint)
            points[points.Length - 1].IsLast = true;
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
