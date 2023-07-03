using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class SpeedRelativeSound : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField][Range(0.0f, 1.0f)] private float speed;
    [SerializeField][Range(0.0f, 1.0f)] public float baseVolume;

    private AudioSource audioSource;
    private float normSpeed;

    private void Start()
    {
        audioSource = GetComponent<AudioSource> ();
    }

    private void Update()
    {
        normSpeed = car.NormalizeLinearVelocity;
        if (audioSource == null) return;
        if (normSpeed >= speed)
        {
            audioSource.volume = baseVolume + normSpeed;
            if (!audioSource.isPlaying)
                audioSource.Play();
        } else 
            audioSource.Stop();
    }
}
