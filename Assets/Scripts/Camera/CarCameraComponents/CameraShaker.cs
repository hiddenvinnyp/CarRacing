using UnityEngine;
using UnityEngine.Rendering;

public class CameraShaker : CarCameraComponent
{
    [SerializeField][Range(0.0f, 1.0f)] private float normalizeSpeedShake;
    [SerializeField] private float shakeAmount;
    [SerializeField] private Volume volume;

    private float normSpeed;

    private void Update()
    {
        normSpeed = car.NormalizeLinearVelocity;

        volume.weight = Mathf.Clamp(normSpeed, 0.0f, 1.0f);

        if (car.NormalizeLinearVelocity >= normalizeSpeedShake) 
            transform.localPosition += Random.insideUnitSphere * shakeAmount * normSpeed * Time.deltaTime;

    }
}
