using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour
{
    public event UnityAction<bool> PauseStateChanged;

    private bool isPaused;
    public bool IsPaused => isPaused;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        UnPause();
    }

    public void ChangePauseState()
    {
        if (isPaused)
            UnPause();
        else
            Pause();
    }

    public void Pause()
    {
        if (isPaused) return;

        Time.timeScale = 0;
        //AudioListener.pause = true;
        isPaused = true;
        PauseStateChanged?.Invoke(isPaused);
    }

    public void UnPause()
    {
        if (!isPaused) return;

        Time.timeScale = 1;
        //AudioListener.pause = false;
        isPaused = false;
        PauseStateChanged?.Invoke(isPaused);
    }
}
