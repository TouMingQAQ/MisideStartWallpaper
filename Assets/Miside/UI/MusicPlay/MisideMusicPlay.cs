using System;
using Cysharp.Threading.Tasks;
using TFramework.Music;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MisideMusicPlay : MonoBehaviour
{
    [SerializeField]
    private RectTransform root;
    [SerializeField]
    private UnityMusicGroup musicGroup;
    [SerializeField]
    private MusicInfo musicInfo;
    [SerializeField]
    private MusicControl control;
    private void Awake()
    {
        Init();
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

    private void LateUpdate()
    {
        control.UpdateMusicProgress();
    }

    void StartWaitLoad()
    {
        
    }

    void EndWaitLoad()
    {
        
    }
}
