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

    [SerializeField]
    private MiSideStart miSideStart;

    [SerializeField]
    private UnityMusicVisualizer visualizer;
    [SerializeField]
    private UnityAudioBeat beate;
    private void Awake()
    {
        Init();
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
}
