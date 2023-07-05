using System;
using UnityEngine;

public class CarCameraController : MonoBehaviour, IDependancy<RaceStateTracker>, IDependancy<Car>
{
    [SerializeField] private Camera cam;
    [SerializeField] private CameraFollow follower;
    [SerializeField] private CameraShaker shaker;
    [SerializeField] private CameraFOVCorrector corrector;
    [SerializeField] private CameraPathFollower pathFollower;

    private Car car;
    private RaceStateTracker raceStateTracker;

    public void Construct(Car dependency) => car = dependency;
    public void Construct(RaceStateTracker dependency) => raceStateTracker = dependency;

    private void Awake()
    {
        follower.SetProperties(car, cam);
        shaker.SetProperties(car, cam);
        corrector.SetProperties(car, cam);
        pathFollower.SetProperties(car, cam);
    }

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Completed += OnCompleted;

        follower.enabled = false;
        pathFollower.enabled = true;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Completed -= OnCompleted;
    }

    private void OnCompleted()
    {
        follower.enabled = false;
        pathFollower.enabled = true;
        pathFollower.SetLookTarget(car.transform);
        pathFollower.StartMoveToNearestPoint();
    }

    private void OnPreparationStarted()
    {
        follower.enabled = true;
        pathFollower.enabled = false;
    }
}
