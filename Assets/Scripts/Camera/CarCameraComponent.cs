using UnityEngine;

[RequireComponent(typeof(CarCameraController))]
public abstract class CarCameraComponent : MonoBehaviour
{
    protected Car car;
    protected Camera cam;

    public virtual void SetProperties(Car car, Camera cam)
    {
        this.car = car;
        this.cam = cam;
    }
}
