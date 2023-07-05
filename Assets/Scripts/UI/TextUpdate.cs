using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextUpdate : MonoBehaviour, IDependancy<Car>
{
    private Car car;
    private Text text;

    public void Construct(Car dependency) => car = dependency;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = car.EngineRPM.ToString();
    }
}
