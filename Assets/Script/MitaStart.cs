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
    public Vector3 headOffset;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        HideControl();
        animator.SetInteger(Init,Random.Range(startAnimationRange.x,startAnimationRange.y));
    }

    private void Update()
    {
        lookAtIk.solver.headTargetOffset = headOffset;
    }


    public void Wink()
    {
        winkParticles.Play();
    }

    public void HideControl()
    {
        mouseControl.enabled = false;
        lookAtIk.solver.SetLookAtWeight(0);
        onClickAnimation.enabled = false;
    }
    

    public void OnStartAnimationEnd()
    {
        mouseControl.enabled = true;
        onClickAnimation.enabled = true;
        lookAtIk.solver.SetLookAtWeight(1);
    }
}
