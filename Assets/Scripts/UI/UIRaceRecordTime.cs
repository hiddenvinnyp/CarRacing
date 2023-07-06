using TMPro;
using UnityEngine;

public class UIRaceRecordTime : MonoBehaviour, IDependancy<RaceResultTime>, IDependancy<RaceStateTracker>
{
    [SerializeField] private GameObject goldRecordObject;
    [SerializeField] private GameObject playerRecordObject;
    [SerializeField] private TextMeshProUGUI goldRecordTime;
    [SerializeField] private TextMeshProUGUI playerRecordTime;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker dependency) => raceStateTracker = dependency;

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime dependency) => raceResultTime = dependency;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }
    private void OnRaceStarted()
    {
        if (raceResultTime.PlayerRecordTime == 0 || raceResultTime.PlayerRecordTime > raceResultTime.GoldTime)
        {
            goldRecordObject.SetActive(true);
            goldRecordTime.text = StringTime.SecondToTimeSpring(raceResultTime.GoldTime);
        }

        if (raceResultTime.PlayerRecordTime != 0)
        {
            playerRecordObject.SetActive(true);
            playerRecordTime.text = StringTime.SecondToTimeSpring(raceResultTime.PlayerRecordTime);
        }
    }

    private void OnRaceCompleted()
    {
        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
    }
}
