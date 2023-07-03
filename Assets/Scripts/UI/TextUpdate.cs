using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextUpdate : MonoBehaviour
{
    [SerializeField] private Car car;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = car.EngineRPM.ToString();
    }
}
