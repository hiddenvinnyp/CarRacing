using System;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip hover;

    private AudioSource audioSource;

    private UIButton[] buttons;

    private void Start()
    {
        audioSource = GetComponent<AudioSource> ();
        buttons = GetComponentsInChildren<UIButton>(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter += OnPointerEnter;
            buttons[i].PointerClick += OnPointerClicked;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter -= OnPointerEnter;
            buttons[i].PointerClick -= OnPointerClicked;
        }
    }

    private void OnPointerClicked(UIButton arg0)
    {
        audioSource.PlayOneShot(click);
    }

    private void OnPointerEnter(UIButton arg0)
    {
        audioSource.PlayOneShot(hover);
    }
}
