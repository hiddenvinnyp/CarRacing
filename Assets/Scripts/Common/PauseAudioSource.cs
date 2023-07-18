using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class PauseAudioSource : MonoBehaviour, IDependancy<Pauser>
{
    private AudioSource audioSource;
    private Pauser pauser;
    public void Construct(Pauser dependency) => pauser = dependency;

    private void Start()
    {
        audioSource = GetComponent<AudioSource> ();
        pauser.PauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChanged -= OnPauseStateChanged;
    }

    private void OnPauseStateChanged(bool pause)
    {
        if (!pause)
        {
            audioSource.Play ();
        }

        if (pause)
        {
            audioSource.Stop();
        }
    }
}
