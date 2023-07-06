using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceResultTime : MonoBehaviour, IDependancy<RaceTimeTracker>, IDependancy<RaceStateTracker>
{
    public static string SaveMark = "_player_best_time";
    public event UnityAction ResultUpdated;

    [SerializeField] private float goldTime;

    private float playerRecordTime;
    private float currentTime;

    public float GoldTime => goldTime;
    public float PlayerRecordTime => playerRecordTime;
    public float CurrentTime => currentTime;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker dependency) => raceStateTracker = dependency;

    private RaceTimeTracker timeTracker;
    public void Construct(RaceTimeTracker dependency) => timeTracker = dependency;

    private void Awake()
    {
        Load();
    }

    private void Start()
    {
        raceStateTracker.Completed += OnRaceCompleted;
    }

    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceCompleted()
    {
        float absoluteRecord = GetAbsoluteRecord();

        if (timeTracker.CurrentTime < absoluteRecord || playerRecordTime == 0)
        {
            playerRecordTime = timeTracker.CurrentTime;
            Save();
        }

        currentTime = timeTracker.CurrentTime;
        ResultUpdated?.Invoke();
    }

    public float GetAbsoluteRecord()
    {
        if (playerRecordTime != 0 && playerRecordTime < goldTime)
            return playerRecordTime;
        else 
            return goldTime;
    }

    private void Load()
    {
        playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
    }
}
