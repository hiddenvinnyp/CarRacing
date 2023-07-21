using UnityEngine;

public class Obstacle : MonoBehaviour, IDependancy<CarRespawner>
{
    private CarRespawner carRespawner;
    public void Construct(CarRespawner dependency) => carRespawner = dependency;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<Car>() == null) return;  
        
        carRespawner.Respawn();
    }
}
