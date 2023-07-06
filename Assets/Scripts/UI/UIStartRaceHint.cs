public class UIStartRaceHint : UIHint, IDependancy<RaceStateTracker>
{
    private RaceStateTracker stateTracker;
    public void Construct(RaceStateTracker dependency) => stateTracker = dependency;

    protected override void Start()
    {
        base.Start();

        stateTracker.PreparationStarted += OnPreparationStarted;
    }

    private void OnDestroy()
    {
        stateTracker.PreparationStarted -= OnPreparationStarted;
    }

    private void OnPreparationStarted()
    {
        gameObject.SetActive(false);
    }
}
