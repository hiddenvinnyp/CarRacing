using UnityEngine;
using UnityEngine.Events;

public class UIBlockedButton : UISelectableButton
{
    [SerializeField] private GameObject lockedMark;

    public UnityEvent OnUnLocked;

    public void SetLocked()
    {
        lockedMark.SetActive(true);
        Interactable = false;
    }

    public void SetUnLocked()
    {
        Interactable = true;
        lockedMark.SetActive(false);
        OnUnLocked?.Invoke();
    }
}
