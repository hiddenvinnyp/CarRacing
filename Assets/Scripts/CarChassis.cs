using System;
using UnityEngine;

public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxles;

    public float MotorTorque;
    public float BrakeTorque;
    public float SteerAngle;

    private void FixedUpdate()
    {
        UpdateWheelAxles();
    }

    private void UpdateWheelAxles()
    {
        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();
            wheelAxles[i].ApplyMotorTorque(MotorTorque);
            wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
            wheelAxles[i].ApplySteerAngle(SteerAngle);
        }
    }
}
