using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    //Hide after DEBUG
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;
    public float HandBrakeControl;

    private CarChassis chassis;

    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        chassis.MotorTorque = maxMotorTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
    }
}
