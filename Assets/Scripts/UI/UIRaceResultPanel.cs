using System;
using TMPro;
using UnityEngine;

public class UIRaceResultPanel : MonoBehaviour, IDependancy<RaceResultTime>
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI recordTime;
    [SerializeField] private TextMeshProUGUI currentTime;

    private RaceResultTime raceResultTime;

    public void Construct(RaceResultTime dependency) => raceResultTime = dependency;

    private void Start()
    {
        resultPanel.SetActive(false);
        raceResultTime.ResultUpdated += OnResultUpdated;
    }

    private void OnDestroy()
    {
        raceResultTime.ResultUpdated -= OnResultUpdated;
    }

    private void OnResultUpdated()
    {
        resultPanel.SetActive(true);

        recordTime.text = StringTime.SecondToTimeSpring(raceResultTime.GetAbsoluteRecord());
        currentTime.text = StringTime.SecondToTimeSpring(raceResultTime.CurrentTime);
    }
}
