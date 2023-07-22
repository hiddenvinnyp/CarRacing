using UnityEngine;
using UnityEngine.SceneManagement;

public class UINextScene : MonoBehaviour, IDependancy<LevelTracker>
{
    private LevelTracker levelTracker;
    public void Construct(LevelTracker dependency) => levelTracker = dependency;

    public void LoadNextLevel()
    {
        levelTracker.LoadNextLevel(SceneManager.GetActiveScene().name);
    }
}
