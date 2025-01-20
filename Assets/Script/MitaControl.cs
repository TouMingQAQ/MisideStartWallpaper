using System;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class MitaControl : MonoBehaviour,IPointerClickHandler
{
    private static readonly int OnClick = Animator.StringToHash("OnClick");
    public MiSideStart start;
    public Animator animator;
    public LookAtIK lookAtIk;
    public ParticleSystem winkParticles;
    public Transform winkRoot;
    public AudioSource audioSource;
    public AudioClip winkClip;
    public AudioClip onClickClip;

    private void Awake()
    {
        winkParticles = Instantiate(winkParticles,winkRoot);
    }
    public void Wink()
    {
        audioSource.PlayOneShot(winkClip);
        winkParticles.Play();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!start.canControl)
            return;
        if(start.clickCount <= 0)
            return;
        start.clickCountTimer++;
        if (start.clickTimer <= 0)
        {
            start.clickTimer = Random.Range(start.clickDelayRange.x, start.clickDelayRange.y);
        }
        if (start.clickCountTimer >= start.clickCount)
        {
            SetAnimation();
            start.clickCountTimer = 0;
            start.clickCount = Random.Range(start.clickRange.x, start.clickRange.y);
            start.clickTimer = 0;
        }
        start.clickParticle.OnClick();
        if(MiSideStart.config.PlaySoundOnClick)
            audioSource.PlayOneShot(onClickClip);
        void SetAnimation()
        {
            start.HideControl();
            animator.SetTrigger(OnClick);
        }
    }
    public void OnStartAnimationEnd()
    {
        start.EnableControl();
    }
}
