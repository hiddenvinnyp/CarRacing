using System;
using UnityEngine;

[Serializable]
public class WheelAxle
{
    [SerializeField] private WheelCollider leftWheelCollider;
    [SerializeField] private WheelCollider rightWheelCollider;

    [SerializeField] private Transform leftWheelMesh;
    [SerializeField] private Transform rightWheelMesh;

    [SerializeField] private bool isMotor;
    [SerializeField] private bool isSteer;

    [SerializeField] private float wheelWidth;
    [SerializeField] private float antiRollForce;

    [SerializeField] private float additionalWheelDownForce;

    [SerializeField] private float baseForwardStiffness = 1.5f;
    [SerializeField] private float stabilityForwardFactor = 1.0f;
    [SerializeField] private float baseSidewaysStiffness = 2.0f;
    [SerializeField] private float stabilitySidewaysFactor = 1.0f;

    public bool IsMotor => isMotor;
    public bool IsSteer => isSteer;

    private float radius, angleSign;
    private WheelHit leftWheelHit, rightWheelHit;
    private float travelLeft, travelRight, forcrDir;
    private WheelFrictionCurve leftForward, rightForward, leftSideways, rightSideways;

    #region Public API
    public void Update()
    {
        UpdateWheelHits();

        ApplyAntiRoll();
        ApplyDownForce();
        CorrectStiffness();
        SyncMeshTransform();
    } 

    public void ConfigureVehicleSubsteps(float speedThreshold, int speedBelowThreshold, int speedAboveThreshold)
    {
        leftWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, speedAboveThreshold);
        rightWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, speedAboveThreshold);
    }

    public void ApplySteerAngle(float angle, float wheelBaseLength)
    {
        if (!isSteer) return;

        // ”гол јккермана
        radius = Mathf.Abs(wheelBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(angle))));

        angleSign = Mathf.Sign(angle);

        if (angle > 0)
        {
            leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + wheelWidth*0.5f)) * angleSign;
            rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - wheelWidth * 0.5f)) * angleSign;
        } 
        else if (angle < 0)
        {
            leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - wheelWidth * 0.5f)) * angleSign;
            rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + wheelWidth * 0.5f)) * angleSign;
        } 
        else
        {
            leftWheelCollider.steerAngle = 0;
            rightWheelCollider.steerAngle = 0;
        }
    }

    public void ApplyMotorTorque(float motorTorque)
    {
        if (!isMotor) return;

        leftWheelCollider.motorTorque = motorTorque;
        rightWheelCollider.motorTorque = motorTorque;
    }

    public void ApplyBrakeTorque(float brakeTorque)
    {
        leftWheelCollider.brakeTorque = brakeTorque;
        rightWheelCollider.brakeTorque = brakeTorque;
    }

    public float GetRadius()
    {
        return leftWheelCollider.radius;
    }

    public float GetAverageRPM()
    {
        return (leftWheelCollider.rpm + rightWheelCollider.rpm) * 0.5f;
    }
    #endregion

    #region Private methods
    private void UpdateWheelHits()
    {
        leftWheelCollider.GetGroundHit(out leftWheelHit);
        rightWheelCollider.GetGroundHit(out rightWheelHit);
    }

    private void CorrectStiffness()
    {
        leftForward = leftWheelCollider.forwardFriction;
        rightForward = rightWheelCollider.forwardFriction;

        leftSideways = leftWheelCollider.sidewaysFriction;
        rightSideways = rightWheelCollider.sidewaysFriction;

        leftForward.stiffness = baseForwardStiffness + Mathf.Abs(leftWheelHit.forwardSlip) * stabilityForwardFactor;
        rightForward.stiffness = baseForwardStiffness + Mathf.Abs(rightWheelHit.forwardSlip) * stabilityForwardFactor;

        leftSideways.stiffness = baseSidewaysStiffness + Mathf.Abs(leftWheelHit.sidewaysSlip) * stabilitySidewaysFactor;
        rightSideways.stiffness = baseSidewaysStiffness + Mathf.Abs(rightWheelHit.sidewaysSlip) * stabilitySidewaysFactor;

        leftWheelCollider.forwardFriction = leftForward;
        rightWheelCollider.forwardFriction = rightForward;

        leftWheelCollider.sidewaysFriction = leftSideways;
        rightWheelCollider.sidewaysFriction = rightForward;
    }

    private void ApplyDownForce()
    {
        if (leftWheelCollider.isGrounded)
            leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * -additionalWheelDownForce *
                leftWheelCollider.attachedRigidbody.velocity.magnitude, leftWheelCollider.transform.position);

        if (rightWheelCollider.isGrounded)
            rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelHit.normal * -additionalWheelDownForce *
                rightWheelCollider.attachedRigidbody.velocity.magnitude, rightWheelCollider.transform.position);
    }

    private void ApplyAntiRoll()
    {
        travelLeft = 1.0f;
        travelRight = 1.0f;

        if (leftWheelCollider.isGrounded)
            travelLeft = (-leftWheelCollider.transform.InverseTransformPoint(leftWheelHit.point).y
                - leftWheelCollider.radius) / leftWheelCollider.suspensionDistance;

        if (rightWheelCollider.isGrounded)
            travelRight = (-rightWheelCollider.transform.InverseTransformPoint(rightWheelHit.point).y
                - rightWheelCollider.radius) / rightWheelCollider.suspensionDistance;

        forcrDir = travelLeft - travelRight;

        if (leftWheelCollider.isGrounded)
            leftWheelCollider.attachedRigidbody.AddForceAtPosition(leftWheelCollider.transform.up * -forcrDir * antiRollForce,
                leftWheelCollider.transform.position);

        if (rightWheelCollider.isGrounded)
            rightWheelCollider.attachedRigidbody.AddForceAtPosition(rightWheelCollider.transform.up * forcrDir * antiRollForce,
                rightWheelCollider.transform.position);
    }
    private void SyncMeshTransform()
    {
        UpdateWheelTransform(leftWheelCollider, leftWheelMesh);
        UpdateWheelTransform(rightWheelCollider, rightWheelMesh);
    }

    private Vector3 position;
    private Quaternion rotation;

    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
    #endregion
}
