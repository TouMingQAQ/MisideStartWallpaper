using System;
using DG.Tweening;
using FMODUnity;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.EventSystems;
using VInspector;
using Random = UnityEngine.Random;

public class MiSideStart : MonoBehaviour,IPointerClickHandler
{
    private static readonly int Init = Animator.StringToHash("Init");
    private static readonly int OnClick = Animator.StringToHash("OnClick");

    [Tab("Components")]
    public Animator animator;
    public MouseToWorldControl mouseControl;
    public LookAtIK lookAtIk;
    public ParticleSystem winkParticles;
    public Transform winkRoot;
    
    [Tab("Normal")][InspectorName("帧率限制")]
    public float targetFrameRate = 60;
    [InspectorName("开场动画随机范围")]
    public Vector2Int startAnimationRange = new Vector2Int(0, 5);
    [SerializeField,ReadOnly]
    private bool canControl = true;
    [Tab("Nod")]
    [SerializeField,ReadOnly]
    private bool noding;
    [SerializeField]
    private Vector3 headOffset = new Vector3(0,0.375f,0);
    [SerializeField]
    private Vector3 nodEnd = new Vector3(0,-0.1f,0);
    [SerializeField]
    private AnimationCurve nodCurve = AnimationCurve.Linear(0,0,1,1);
    [SerializeField]
    private float waitTime = 0.025f;
    [SerializeField]
    private Vector2 nodDuration = new Vector2(0.3f,0.4f);
    
    [Tab("OnClick")]
    public EventReference onClickMiSideAudio;
    public ClickParticle clickParticle;
    public int clickCount = 2;
    public Vector2 clickDelayRange = new Vector2(0.4f, 0.6f);
    public Vector2Int clickRange = new Vector2Int(2, 4);
    [SerializeField,ReadOnly]
    private float clickTimer = 0;
    [SerializeField,ReadOnly]
    private int clickCountTimer = 0;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        HideControl();
        winkParticles = Instantiate(winkParticles,winkRoot);
        animator.SetInteger(Init,Random.Range(startAnimationRange.x,startAnimationRange.y));
    }
    
    [ContextMenu("NodOnShot")]
    public void NodOnShot()
    {
        if(noding)
            return;
        noding = true;
        var offset =headOffset;
        var q = DOTween.Sequence();
        var tween = DOTween.To(()=>headOffset,x=>headOffset=x,nodEnd,nodDuration.x).SetEase(nodCurve);
        q.Append(tween);
        q.AppendInterval(waitTime);
        tween = DOTween.To(()=>headOffset,x=>headOffset=x,offset,nodDuration.y).SetEase(nodCurve);
        q.Append(tween);
        q.AppendCallback(()=>
        {
            noding = false;
            headOffset =offset;
        });
        q.Play().SetAutoKill(true);
    }
    private void Update()
    {
        lookAtIk.solver.headTargetOffset = headOffset;
        if (clickTimer >= 0)
        {
            clickTimer -= Time.deltaTime;
            if (clickTimer <= 0)
            {
                //点击清零
                clickCountTimer = 0;
            }
        }
    }


    public void Wink()
    {
        winkParticles.Play();
    }

    public void HideControl()
    {
        mouseControl.enabled = false;
        lookAtIk.solver.SetLookAtWeight(0);
        canControl = false;
    }
    

    public void OnStartAnimationEnd()
    {
        mouseControl.enabled = true;
        canControl = true;
        lookAtIk.solver.SetLookAtWeight(1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!canControl)
            return;
        clickCountTimer++;
        if (clickTimer <= 0)
        {
            clickTimer = Random.Range(clickDelayRange.x, clickDelayRange.y);
        }
        if (clickCountTimer >= clickCount)
        {
            SetAnimation();
            clickCountTimer = 0;
            clickCount = Random.Range(clickRange.x, clickRange.y);
            clickTimer = 0;
        }
        clickParticle.OnClick();
        RuntimeManager.PlayOneShot(onClickMiSideAudio);
        void SetAnimation()
        {
            HideControl();
            animator.SetTrigger(OnClick);
        }
    }
}
