public class UIReloadHint : UIHint, IDependancy<RaceStateTracker>
{
    private RaceStateTracker stateTracker;
    public void Construct(RaceStateTracker dependency) => stateTracker = dependency;

    protected override void Start()
    {
        base.Start();
        gameObject.SetActive(false);

        stateTracker.Completed += OnCompleted;
    }

    private void OnDestroy()
    {
        stateTracker.Completed -= OnCompleted;
    }

    private void OnCompleted()
    {
        gameObject.SetActive(true);
    }
}
