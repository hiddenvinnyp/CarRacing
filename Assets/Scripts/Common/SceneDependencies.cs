using UnityEngine;

public interface IDependancy<T>
{
    void Construct(T dependency);
}

public class SceneDependencies : MonoBehaviour
{
    [SerializeField] private TrackpointCircuit trackpointCircuit;
    [SerializeField] private RaceStateTracker stateTracker;
    [SerializeField] private CarInputControl control;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController controller;

    private void Awake()
    {
        MonoBehaviour[] allMonobehavioursInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < allMonobehavioursInScene.Length; i++)
        {
            Bind(allMonobehavioursInScene[i]);
        }
    }

    private void Bind(MonoBehaviour mono)
    {
        (mono as IDependancy<TrackpointCircuit>)?.Construct(trackpointCircuit);
        (mono as IDependancy<RaceStateTracker>)?.Construct(stateTracker);
        (mono as IDependancy<CarInputControl>)?.Construct(control);
        (mono as IDependancy<Car>)?.Construct(car);
        (mono as IDependancy<CarCameraController>)?.Construct(controller);
    }
}
