using UnityEngine;
using UnityEngine.Events;

public class TrackPoint : MonoBehaviour
{
    public event UnityAction<TrackPoint> Triggered;
    public TrackPoint NextNode;
    public bool IsFirst;
    public bool IsLast;

    protected bool isTarget;
    public bool IsTarget => isTarget;
    public bool IsCenter => !IsFirst && !IsLast;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<Car>() == null) return;

        Triggered?.Invoke(this);
    }

    public void Passed()
    {
        isTarget = false;
    }

    public void AssignAsTarget()
    {
        isTarget = true;
    }

    public void ResetPoint()
    {
        NextNode = null;
        IsFirst = false;
        IsLast = false;
    }
}
