using UnityEngine;

public class UIPausePanel : MonoBehaviour, IDependancy<Pauser>
{
    [SerializeField] private GameObject panel;

    private Pauser pauser;

    public void Construct(Pauser dependency) => pauser = dependency;

    private void Start()
    {
        panel.SetActive(false);
        pauser.PauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChanged -= OnPauseStateChanged;
    }

    private void OnPauseStateChanged(bool isPaused)
    {
        panel.SetActive(isPaused);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauser.ChangePauseState();
        }
    }

    public void UnPause()
    {
        pauser.UnPause();
    }
}
