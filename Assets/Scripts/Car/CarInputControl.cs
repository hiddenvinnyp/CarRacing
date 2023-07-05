using System;
using UnityEngine;

public class CarInputControl : MonoBehaviour, IDependancy<Car>
{
    [SerializeField] private AnimationCurve brakeCurve;
    [SerializeField][Range(0.0f, 1.0f)] private float autoBrakeStrength = 0.5f;
    [SerializeField] private AnimationCurve steerCurve;

    private Car car;
    public void Construct(Car dependency) => car = dependency;

    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handbrakeAxis;

    private void Update()
    {
        wheelSpeed = car.WheelSpeed;
        //print($"Vel: {car.LinearVelocity} /n Wheel: {wheelSpeed} /n Max: {car.MaxSpeed}");
        UpdateAxis();

        UpdateTorttleAndBrake();
        UpdateSteer();   
        UpdateBrake();
        
        UpdateAutoBrake();
    }

    private void UpdateTorttleAndBrake()
    {
        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            car.ThrottleControl = Mathf.Abs(verticalAxis);
            car.BrakeControl = 0;
        } else
        {
            car.ThrottleControl = 0;
            car.BrakeControl = brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed);
        }

        // Gears
        if (verticalAxis < 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
        {
            car.ShiftToReverseGear();
        }

        if (verticalAxis > 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
        {
            car.ShiftToFirstGear();
        }
    }

    private void UpdateBrake() //ручной тормоз. в car и carchassis
    {
        
    }

    private void UpdateSteer()
    {
        car.SteerControl = steerCurve.Evaluate(wheelSpeed / car.MaxSpeed) * horizontalAxis;
    }

    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handbrakeAxis = Input.GetAxis("Jump");
    }

    private void UpdateAutoBrake()
    {
        if (verticalAxis == 0)
        {
            car.BrakeControl = brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed) * autoBrakeStrength;
        }
    }

    public void Stop()
    {
        verticalAxis = 0;
        horizontalAxis = 0;
        handbrakeAxis = 0;

        car.ThrottleControl = 0;
        car.SteerControl = 0;
        car.BrakeControl = 1;
        car.HandBrakeControl = 0;
    }
}
