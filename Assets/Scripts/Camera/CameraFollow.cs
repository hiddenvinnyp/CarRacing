using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody carRigidbody;

    [Header("Offset")]
    [SerializeField] private float viewHeight;
    [SerializeField] private float height;
    [SerializeField] private float distance;

    [Header("Damping")]
    [SerializeField] private float rotationDamping;
    [SerializeField] private float heightDamping;
    [SerializeField] private float speedThreshold = 5.0f;

    private void FixedUpdate()
    {
        Vector3 velocity = carRigidbody.velocity;
        Vector3 targetRotation = target.eulerAngles;

        if (velocity.magnitude > speedThreshold)
            targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles; // переводим вектор направления в поворот и обратно в вектор вращения

        // Lerp
        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, rotationDamping * Time.fixedDeltaTime);
        float currentHeight = Mathf.Lerp(transform.position.y, target.position.y + height, heightDamping * Time.fixedDeltaTime);

        //Position
        Vector3 positionOffset = Quaternion.Euler(0, currentAngle/*targetRotation.y*/, 0) * Vector3.forward * distance;
        transform.position = target.position - positionOffset;
        transform.position = new Vector3(transform.position.x, currentHeight/*target.position.y + height*/, transform.position.z);

        // Rotation
        transform.LookAt(target.position + new Vector3(0, viewHeight, 0));
    }
}
