using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour
{
    private const string levelCompletedTag = "completedLevelName";

    [SerializeField] private RaceInfo[] levels;

    private int maxCompletedLevelIndex;
    public int MaxCompletedLevelIndex => maxCompletedLevelIndex;

    private static List<string> openedLevels = new List<string>();
    public List<string> OpenedLevels => openedLevels;

    private void Awake()
    {
        LevelListUpdate();
    }

    private void LevelListUpdate()
    {
    string maxCompletedLevel = Load();
        if (maxCompletedLevel == null || maxCompletedLevel == "")
        {
            openedLevels.Add(levels[0].SceneName);
        } else
        {
            for (int i = 0; i < levels.Length; i++)
            {
                openedLevels.Add(levels[i].SceneName);
                if (levels[i].SceneName == maxCompletedLevel)
                {
                    openedLevels.Add(levels[i + 1]?.SceneName);
                    break;
                }
            }
        }
    }

    private void OnDestroy()
    {
        openedLevels.Clear();
    }

    public string Load()
    {
        return PlayerPrefs.GetString(levelCompletedTag, null);
    }

    public void Save(string sceneName)
    {
        LevelListUpdate();

        if (openedLevels.Contains(sceneName) && sceneName != openedLevels.Last()) return;

        PlayerPrefs.SetString(levelCompletedTag, sceneName);
    }

    public void LoadNextLevel(string sceneName)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].SceneName == sceneName)
            {
                if (i + 1 >= levels.Length) return;
                SceneManager.LoadScene(levels[i + 1].SceneName);
            }
        }
        
    }
}
