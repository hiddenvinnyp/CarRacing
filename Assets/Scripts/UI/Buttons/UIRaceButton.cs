using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRaceButton : UIBlockedButton, IScriptableObjectProperty
{
    [SerializeField] private RaceInfo raceInfo;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;

    public string SceneName => raceInfo.SceneName;

    private void Start()
    {
        ApplyProperty(raceInfo);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (!Interactable) return;
        if (raceInfo == null) return;
        SceneManager.LoadScene(raceInfo.SceneName);
    }

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null) return;

        if (property is RaceInfo == false) return;
        raceInfo = property as RaceInfo;
        icon.sprite = raceInfo.Icon; 
        title.text = raceInfo.Title;
    }
}
