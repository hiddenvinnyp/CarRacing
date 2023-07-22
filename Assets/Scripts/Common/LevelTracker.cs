using System;
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
            print("add " + openedLevels.Last());
        } else
        {
            for (int i = 0; i < levels.Length; i++)
            {
                openedLevels.Add(levels[i].SceneName);
                print("else add " + openedLevels.Last());
                if (levels[i].SceneName == maxCompletedLevel)
                {
                    openedLevels.Add(levels[i + 1]?.SceneName);
                    print("else if add " + openedLevels.Last());
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

        //проверять, что сцена в списке позже раннее записанной
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
