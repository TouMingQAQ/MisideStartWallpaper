using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class OnClickAnimation : MonoBehaviour,IPointerClickHandler
{
    private static readonly int OnClick = Animator.StringToHash("OnClick");
    public Animator animator;
    public AudioClip clickAudioClip;
    public AudioSource audioSource;
    public MitaStart mitaStart;
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
        audioSource.PlayOneShot(clickAudioClip);
    }

    void SetAnimation()
    {
        mitaStart.HideControl();
        animator.SetTrigger(OnClick);
    }
}
