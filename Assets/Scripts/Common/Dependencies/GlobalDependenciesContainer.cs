using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDependenciesContainer : Dependency
{
    [SerializeField] private Pauser pauser;
    [SerializeField] private LevelTracker levelTracker;

    private static GlobalDependenciesContainer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected override void BindAll(MonoBehaviour monoBehaviorInScene)
    {
        Bind<Pauser>(pauser, monoBehaviorInScene);
        Bind<LevelTracker>(levelTracker, monoBehaviorInScene);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectsToBind();
    }
}
