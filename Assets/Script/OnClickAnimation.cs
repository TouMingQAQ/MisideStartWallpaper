using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickAnimation : MonoBehaviour,IPointerClickHandler
{
    private static readonly int OnClick = Animator.StringToHash("OnClick");
    public Animator animator;
    public AudioClip clickAudioClip;
    public AudioSource audioSource;
    public MitaStart mitaStart;
    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     audioSource.PlayOneShot(clickAudioClip);
        //     animator.SetTrigger(OnClick);
        // }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mitaStart.HideControl();
        audioSource.PlayOneShot(clickAudioClip);
        animator.SetTrigger(OnClick);
    }
}
