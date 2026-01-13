using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TFramework.Music;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MisideMusicPlay : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private RectTransform root;
    [SerializeField]
    private UnityMusicGroup musicGroup;
    [SerializeField]
    private MusicInfo musicInfo;
    [SerializeField]
    private MusicControl control;

    [SerializeField]
    private MiSideStart miSideStart;

    [SerializeField]
    private UnityMusicVisualizer visualizer;
    [SerializeField]
    private UnityAudioBeat beate;

    private Tween showPlay;
    private void Awake()
    {
        Init();
        root.anchoredPosition = new Vector2(0, 0);
        var width = root.rect.width;
        showPlay = root.DOAnchorPosX(-width, 0.5f).SetLoops(1).SetAutoKill(false).Pause();
    }

    private void Update()
    {
        if(visualizer.IsBeate(beate))
            miSideStart.NodOnShot();
        control.UpdateMusicProgress();
    }

    async void Init()
    {
        StartWaitLoad();
        await musicGroup.LoadMusicAsync();
        EndWaitLoad();
        musicGroup.Play(musicGroup.CurrentIndex);
        musicInfo.RefreshView();
        control.RefreshView();
        control.RefreshDropDown();
    }
    

    void StartWaitLoad()
    {
        
    }

    void EndWaitLoad()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        showPlay.PlayForward();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        showPlay.PlayBackwards();
    }
}
