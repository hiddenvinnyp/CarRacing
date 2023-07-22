using UnityEngine;

public class UIRaceButtonUnlocker : MonoBehaviour, IDependancy<LevelTracker>
{
    private UIRaceButton[] buttons;
    private LevelTracker levelTracker;

    public void Construct(LevelTracker dependency) => levelTracker = dependency;

    void Start()
    {
        buttons = GetComponentsInChildren<UIRaceButton>(true);        

        foreach (UIRaceButton button in buttons)
        {
            button.SetLocked();

            if (levelTracker.OpenedLevels.Contains(button.SceneName))
            {
                button.SetUnLocked();
            }
        }
    }
}
