using TMPro;
using UnityEngine;

public class UIRecordTable : MonoBehaviour, IDependancy<RaceResultTime>
{
    [SerializeField] private GameObject recordObject;
    [SerializeField] private GameObject playerResultObject;
    [SerializeField] private TextMeshProUGUI recordTime;
    [SerializeField] private TextMeshProUGUI playerResultTime;

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime dependency) => raceResultTime = dependency;

    private void Start()
    {
        raceResultTime.ResultUpdated += OnResultUpdated;

        recordObject.SetActive(false);
        playerResultObject.SetActive(false);
        enabled = false;
    }

    private void OnDestroy()
    {
        raceResultTime.ResultUpdated -= OnResultUpdated;
    }

    private void OnResultUpdated()
    {
        enabled = true;

        if (raceResultTime.PlayerRecordTime < raceResultTime.GoldTime && raceResultTime.CurrentTime > raceResultTime.PlayerRecordTime)
        {
            recordTime.text = StringTime.SecondToTimeSpring(raceResultTime.PlayerRecordTime);
        }
        else if (raceResultTime.PlayerRecordTime >= raceResultTime.GoldTime)
        {
            recordTime.text = StringTime.SecondToTimeSpring(raceResultTime.GoldTime);
        }
        else
        {
            recordTime.text = "updated!";
        }
        recordObject.SetActive(true);

        playerResultTime.text = StringTime.SecondToTimeSpring(raceResultTime.CurrentTime);
        playerResultObject.SetActive(true);
    }
}
