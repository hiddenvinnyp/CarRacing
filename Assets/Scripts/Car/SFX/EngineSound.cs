using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    [SerializeField] private Car car;

    [SerializeField] private float pitchModifier;
    [SerializeField] private float volumeModifier;
    [SerializeField] private float rpmModifier;

    [SerializeField] private float basePitch = 1.0f;
    [SerializeField] private float baseVolume = 0.4f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource> ();
    }

    private void Update()
    {
        if (audioSource == null) return;
        
        audioSource.pitch = basePitch + pitchModifier * (car.EngineRPM / car.EngineMaxRPM) * rpmModifier;
        audioSource.volume = baseVolume + volumeModifier * (car.EngineRPM / car.EngineMaxRPM);
    }
}
