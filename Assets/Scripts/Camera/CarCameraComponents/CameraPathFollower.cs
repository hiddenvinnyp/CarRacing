using UnityEngine;

public class CameraPathFollower : CarCameraComponent
{
    [SerializeField] private Transform path;
    [SerializeField] private Transform lookTarget;
    [SerializeField] private float speed;

    private Vector3[] points;
    private int pointIndex;

    private void Start()
    {
        points = new Vector3[path.childCount];

        for (int i = 0; i < points.Length; ++i)
        {
            points[i] = path.GetChild(i).position;
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[pointIndex], speed * Time.deltaTime);

        if (transform.position == points[pointIndex])
        {
            if (pointIndex == points.Length - 1)
                pointIndex = 0;
            else
                pointIndex++;
        }

        transform.LookAt(lookTarget);
    }

    public void SetLookTarget(Transform target)
    {
        lookTarget = target;
    }

    public void StartMoveToNearestPoint()
    {
        float minDistance = float.MaxValue;
        float distance;

        for (int i = 0; i < points.Length; ++i) 
        {
            distance = Vector3.Distance(transform.position, points[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                pointIndex = i;
            }
        }
    }
}
