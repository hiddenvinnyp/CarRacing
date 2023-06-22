using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [Header("Engine Torque")]
    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSpeed;

    [Header("DEBUG section")]
    //Hide after DEBUG
    [SerializeField] private float linearVelocity;
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;
    public float HandBrakeControl;

    private CarChassis chassis;
    private float engineTorque;

    public float LinearVelocity => chassis.LinearVelocity;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;

    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        linearVelocity = LinearVelocity;
        engineTorque = engineTorqueCurve.Evaluate(LinearVelocity / maxSpeed) * maxMotorTorque;

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.MotorTorque = engineTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
    }
}
