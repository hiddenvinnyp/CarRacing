using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheelColliders;
    [SerializeField] private Transform[] wheelMeshes;
    [SerializeField] private float motorTorque;
    [SerializeField] private float breakTorque;
    [SerializeField] private float steerAngle;

    private Vector3 position;
    private Quaternion rotation;

    void Update()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            wheelColliders[i].motorTorque = Input.GetAxis("Vertical") * motorTorque;
            wheelColliders[i].brakeTorque = Input.GetAxis("Jump") * breakTorque;
            wheelColliders[i].GetWorldPose(out position, out rotation);

            wheelMeshes[i].position = position;
            wheelMeshes[i].rotation = rotation;
        }

        wheelColliders[0].steerAngle = Input.GetAxis("Horizontal") * steerAngle;
        wheelColliders[1].steerAngle = Input.GetAxis("Horizontal") * steerAngle;
    }
}
