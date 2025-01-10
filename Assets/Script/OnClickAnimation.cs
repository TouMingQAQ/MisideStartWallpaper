using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class OnClickAnimation : MonoBehaviour,IPointerClickHandler
{
    private static readonly int OnClick = Animator.StringToHash("OnClick");
    public Animator animator;
    public EventReference onClickMisideAudio;
    public MiSideStart misideStart;
    public ClickParticle clickParticle;

    public int clickCount = 2;
    public float clickTimer = 0;
    public int clickCountTimer = 0;
    public Vector2 clickDelayRange = new Vector2(1f, 2f);
    public Vector2Int clickRange = new Vector2Int(2, 3);

    private void Awake()
    {
       
    }

    private void Update()
    {
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

    public void OnPointerClick(PointerEventData eventData)
    {
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
        RuntimeManager.PlayOneShot(onClickMisideAudio);
    }

    void SetAnimation()
    {
        misideStart.HideControl();
        animator.SetTrigger(OnClick);
    }
}
