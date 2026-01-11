using System;
using DG.Tweening;
using TFramework.Music;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public class MusicInfo : MonoBehaviour
{
    [SerializeField]
    private UnityMusicGroup musicGroup;
    [SerializeField]
    private UnityMusic musicPlay;
    [SerializeField]
    private Image musicCover;
    [SerializeField]
    private Text musicName;
    [SerializeField]
    private Text musicWriter;

    
    private Tween musicTween;
    private void Awake()
    {
        musicTween = musicCover.transform.DOLocalRotate(new Vector3(0, 0, -360f), 3, RotateMode.FastBeyond360)
            .SetLoops(-1,LoopType.Restart)
            .SetEase(Ease.Linear)
            .SetAutoKill(false)
            .Play();
        musicGroup.onMusicChange.AddListener(OnMusicChange);
        // musicPlay.onPlay.AddListener(OnMusicPlay);
        // musicPlay.onPlay.AddListener(OnMusicPause);
    }

    private void LateUpdate()
    {
        if (musicPlay.IsPlaying)
            musicTween.Play();
        else
            musicTween.Pause();
    }

    // void OnMusicPlay()
    // {
    //     musicTween.Play();
    // }
    //
    // void OnMusicPause()
    // {
    //     musicTween.Pause();
    // }
    void OnMusicChange(UnityMusicInfo musicInfo)
    {
        RefreshView();
        musicTween.Rewind();
    }
    [Button]
    public void RefreshView()
    {
        Empty();
        musicCover.sprite = musicGroup.MusicCover;
        musicName.text = musicGroup.MusicName;
        musicWriter.text = musicGroup.MusicWriter;
    }

    void Empty()
    {
        musicCover.sprite = musicGroup.DefaultMusicCover;
        musicName.text = "";
        musicWriter.text = "";
    }
}
