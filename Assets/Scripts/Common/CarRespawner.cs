using UnityEngine;

public class CarRespawner : MonoBehaviour, IDependancy<RaceStateTracker>, IDependancy<Car>, IDependancy<CarInputControl>
{
    [SerializeField] private float respawnHeight;
    private TrackPoint respawnPoint;

    private RaceStateTracker stateTracker;
    public void Construct(RaceStateTracker dependency) => stateTracker = dependency;
    private Car car;
    public void Construct(Car dependency) => car = dependency;
    private CarInputControl carInputControl;
    public void Construct(CarInputControl dependency) => carInputControl = dependency;

    public void Respawn()
    {
        if (respawnPoint == null) return;

        if (stateTracker.State != RaceState.Race) return;

        car.Respawn(respawnPoint.transform.position + respawnPoint.transform.up * respawnHeight,
            respawnPoint.transform.rotation);
        carInputControl.Reset();
    }

    private void Start()
    {
        stateTracker.TrackPointPassed += OnTrackPointPassed;
    }

    private void OnDestroy()
    {
        stateTracker.TrackPointPassed -= OnTrackPointPassed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        { 
            Respawn();
        }
    }

    private void OnTrackPointPassed(TrackPoint point)
    {
        respawnPoint = point;
    }
}
