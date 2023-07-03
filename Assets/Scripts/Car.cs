using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    public event UnityAction<string> GearChanged;

    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [Header("Engine Torque")]
    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float engineMaxTorque;
    [SerializeField] private float engineMinRPM;
    [SerializeField] private float engineMaxRPM;
    ////Hide after DEBUG
    [SerializeField] private float engineRPM;
    [SerializeField] private float engineTorque;

    [SerializeField] private float maxSpeed;

    [Header("Gearbox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio; // передача дифференциала
    [SerializeField] private float upShiftEngineRPM; 
    [SerializeField] private float downShiftEngineRPM; 
    //Hide after DEBUG
    [SerializeField] private int selectedGearIndex;
    [SerializeField] private float selectedGear;
    [SerializeField] private float rearGear; // передача заднего хода

    [Header("DEBUG section")]
    //Hide after DEBUG
    [SerializeField] private float linearVelocity;
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;
    public float HandBrakeControl;

    private CarChassis chassis;

    public float LinearVelocity => chassis.LinearVelocity;
    public float NormalizeLinearVelocity => chassis.LinearVelocity / maxSpeed;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;
    public float EngineRPM => engineRPM;
    public float EngineMaxRPM => engineMaxRPM;

    #region Unity events
    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        linearVelocity = LinearVelocity;

        UpdateEngineTorque();
        AutoGearShift(); //АКБ

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.MotorTorque = engineTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
    }
    #endregion

    #region Public API
    public string GetSelectedGearName()
    {
        if (selectedGear == rearGear) return "R";
        if (selectedGear == 0) return "N";
        return (selectedGearIndex + 1).ToString();
    }

    public void UpGear()
    {
        ShiftGear(selectedGearIndex + 1);
    }

    public void DownGear()
    {
        ShiftGear(selectedGearIndex - 1);
    }

    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;
        GearChanged?.Invoke(GetSelectedGearName());
    }

    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }

    public void ShiftToNeutral()
    {
        selectedGear = 0;
        GearChanged?.Invoke(GetSelectedGearName());
    }
    #endregion

    #region Private section
    private void AutoGearShift()
    {
        if (selectedGear < 0) return;

        if (engineRPM >= upShiftEngineRPM)
            UpGear();
        if (engineRPM < downShiftEngineRPM)
            DownGear();

       // selectedGearIndex = Mathf.Clamp(selectedGearIndex, 0, gears.Length - 1);
    }

    private void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;

        GearChanged?.Invoke(GetSelectedGearName());
    }

    private void UpdateEngineTorque()
    {
        engineRPM = engineMinRPM + Mathf.Abs(chassis.GetAverageRPM() * selectedGear * finalDriveRatio);
        engineRPM = Mathf.Clamp(engineRPM, engineMinRPM, engineMaxRPM);

        engineTorque = engineTorqueCurve.Evaluate(engineRPM / engineMaxRPM) * engineMaxTorque * Mathf.Sign(selectedGear);//* finalDriveRatio * gears[0];
    }
    #endregion
}
