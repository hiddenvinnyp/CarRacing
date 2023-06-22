using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxles;
    [SerializeField] private float wheelBaseLength;
    [SerializeField] private Transform centerOfMass;

    [Header("AngularDrag")]
    [SerializeField] private float angularDragMin;
    [SerializeField] private float angularDragMax;
    [SerializeField] private float angularDragFactor;

    [Header("Down Force")]
    [SerializeField] private float downForceMin;
    [SerializeField] private float downForceMax;
    [SerializeField] private float downForceFactor;

    //Hide after DEBUG
    public float MotorTorque;
    public float BrakeTorque;
    public float SteerAngle;

    public float LinearVelocity => carRigidbody.velocity.magnitude * 3.6f; // Ïונוגמה ג ךל/ק

    private Rigidbody carRigidbody;
    private float downForce;
    private int amountMotorWheel = 0;

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();

        if (centerOfMass != null)
            carRigidbody.centerOfMass = centerOfMass.localPosition;

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            if (wheelAxles[i].IsMotor)
                amountMotorWheel += 2;
        }
    }

    private void FixedUpdate()
    {
        UpdateAngularDrag();
        UpdateDownForce();
        UpdateWheelAxles();
    }

    private float sum;
    public float GetAverageRPM()
    {
        sum = 0;

        for (int i = 0; i < wheelAxles.Length; ++i)
        {
            sum += wheelAxles[i].GetAverageRPM();
        }

        return sum / wheelAxles.Length;
    }

    public float GetAverageRadius()
    {
        sum = 0;

        for (int i = 0; i < wheelAxles.Length; ++i)
        {
            sum += wheelAxles[i].GetRadius();
        }

        return sum / wheelAxles.Length;
    }

    public float GetWheelSpeed()
    {
        return GetAverageRPM() * GetAverageRadius() * 2 * 0.1885f; // 2Pr * 0,06 טח ל/ל -> ךל/ק
    }

    private void UpdateAngularDrag()
    {
        carRigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
    }

    private void UpdateDownForce()
    {
        downForce = Mathf.Clamp(downForceFactor * LinearVelocity, downForceMin, downForceMax);
        carRigidbody.AddForce(-transform.up * downForce);
    }

    private void UpdateWheelAxles()
    {
        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();
            wheelAxles[i].ApplyMotorTorque(MotorTorque / amountMotorWheel);
            wheelAxles[i].ApplyBrakeTorque(BrakeTorque);
            wheelAxles[i].ApplySteerAngle(SteerAngle, wheelBaseLength);
        }
    }
}
