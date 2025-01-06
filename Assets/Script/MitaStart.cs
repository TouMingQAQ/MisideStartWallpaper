using System;
using RootMotion.FinalIK;
using UnityEngine;
using Random = UnityEngine.Random;

public class MitaStart : MonoBehaviour
{
    private static readonly int Init = Animator.StringToHash("Init");
    public Animator animator;
    public MouseToWorldControl mouseControl;
    public OnClickAnimation onClickAnimation;
    public LookAtIK lookAtIk;
    public Vector2Int startAnimationRange;
    public ParticleSystem winkParticles;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        HideControl();
        animator.SetInteger(Init,Random.Range(startAnimationRange.x,startAnimationRange.y));
    }

    public void Wink()
    {
        winkParticles.Play();
    }

    public void HideControl()
    {
        mouseControl.enabled = false;
        lookAtIk.enabled = false;
        onClickAnimation.enabled = false;
    }

    public void OnStartAnimationEnd()
    {
        mouseControl.enabled = true;
        onClickAnimation.enabled = true;
        lookAtIk.enabled = true;
    }
}
