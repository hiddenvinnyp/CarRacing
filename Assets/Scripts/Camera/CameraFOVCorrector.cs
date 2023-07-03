using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CameraFOVCorrector : MonoBehaviour
{
    [SerializeField] private Car car;

    [SerializeField] private float minFieldOfView;
    [SerializeField] private float maxFieldOfView;

    private Camera cam;
    private float defaultFOV;

    private void Start()
    {
        cam = GetComponent<Camera>();
        defaultFOV = cam.fieldOfView;
    }

    private void Update()
    {
        if (cam == null) return;

        cam.fieldOfView = Mathf.Lerp(minFieldOfView, maxFieldOfView, car.NormalizeLinearVelocity);
    }
}
