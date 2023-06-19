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

    #region Public API
    public void Update()
    {
        SyncMeshTransform();
        //TODO: симулировать элементы подвески
    }

    public void ApplySteerAngle(float angle)
    {
        if (!isSteer) return;

        leftWheelCollider.steerAngle = angle;
        rightWheelCollider.steerAngle = angle;
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
    #endregion

    #region Private methods
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
