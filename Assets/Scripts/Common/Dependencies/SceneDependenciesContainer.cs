using UnityEngine;

public class SceneDependenciesContainer : Dependency
{
    [SerializeField] private TrackpointCircuit trackpointCircuit;
    [SerializeField] private RaceStateTracker stateTracker;
    [SerializeField] private CarInputControl control;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController controller;
    [SerializeField] private RaceTimeTracker timeTracker;
    [SerializeField] private RaceResultTime resultTime;
    [SerializeField] private CarRespawner carRespawner;

    private void Awake()
    {
        FindAllObjectsToBind();
    }

    protected override void BindAll(MonoBehaviour monoBehaviorInScene)
    {
        Bind<TrackpointCircuit>(trackpointCircuit, monoBehaviorInScene);
        Bind<RaceStateTracker>(stateTracker, monoBehaviorInScene);
        Bind<CarInputControl>(control, monoBehaviorInScene);
        Bind<Car>(car, monoBehaviorInScene);
        Bind<CarCameraController>(controller, monoBehaviorInScene);
        Bind<RaceTimeTracker>(timeTracker, monoBehaviorInScene);
        Bind<RaceResultTime>(resultTime, monoBehaviorInScene);
        Bind<CarRespawner>(carRespawner, monoBehaviorInScene);
    }
}
