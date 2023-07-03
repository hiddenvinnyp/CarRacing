using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class GearboxSound : MonoBehaviour
{
    [SerializeField] private Car car;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource> ();
        car.GearChanged += OnGearChanged;
    }

    private void OnDestroy()
    {
        car.GearChanged -= OnGearChanged;
    }

    private void OnGearChanged(string gearName)
    {
        audioSource.Play();
    }
}
