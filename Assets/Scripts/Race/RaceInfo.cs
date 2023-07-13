using UnityEngine;

[CreateAssetMenu]
public class RaceInfo : ScriptableObject
{
    [SerializeField] private string sceneName;
    [SerializeField] private string title;
    [SerializeField] private Sprite icon;

    public string SceneName => sceneName;
    public string Title => title;
    public Sprite Icon => icon;
}
