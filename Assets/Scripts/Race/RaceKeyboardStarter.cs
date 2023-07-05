using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour, IDependancy<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;

    public void Construct(RaceStateTracker dependency) => raceStateTracker = dependency;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            raceStateTracker.LaunchPreparationStart();
    }
}
