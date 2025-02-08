using System;
using DG.Tweening;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class MitaControl : MonoBehaviour,IPointerClickHandler
{
    private static readonly int OnClick = Animator.StringToHash("OnClick");
    private static readonly int Blink = Animator.StringToHash("Blink");
    private static readonly int BlinkState = Animator.StringToHash("BlinkState");

    public MiSideStart start;
    public Animator animator;
    public LookAtIK lookAtIk;
    public ParticleSystem winkParticles;
    public Transform winkRoot;
    public AudioSource audioSource;
    public AudioClip winkClip;
    public AudioClip onClickClip;
    public SkinnedMeshRenderer headRender;

    private bool _startAnimationEnd = false;
    private void Awake()
    {
        winkParticles = Instantiate(winkParticles,winkRoot);
    }
    public void Wink()
    {
        audioSource.PlayOneShot(winkClip);
        winkParticles.Play();
    }

    private void Update()
    {
        BlinkTimer();
    }

    private float blinkTimer = 0;
    private int blinkShapeIndex = 0;
    private int blinkWeight = 0;
    void BlinkTimer()
    {
        if(!_startAnimationEnd)
            return;
        if (blinkTimer <= 0)
        {
            DoBlink();
            blinkTimer = Random.Range(4f, 10f);
        }
        blinkTimer -= Time.deltaTime;
    }
    
    public void DoBlink()
    {
        if(!_startAnimationEnd)
            return;
        var state = Random.Range(0, 2);
        animator.SetTrigger(Blink);
        animator.SetFloat(BlinkState,state);
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
        _startAnimationEnd = true;
    }
}
