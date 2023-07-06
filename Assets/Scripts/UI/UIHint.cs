using TMPro;
using UnityEngine;

public abstract class UIHint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private string text;

    protected virtual void Start()
    {
        textField.text = text;
    }
}
