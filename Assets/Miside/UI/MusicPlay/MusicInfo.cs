using System;
using TFramework.Music;
using UnityEngine;
using UnityEngine.UI;

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
    private void Awake()
    {
        musicGroup.onMusicChange.AddListener(OnMusicChange);
    }

    void OnMusicChange(UnityMusicInfo musicInfo)
    {
        RefreshView();
    }

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
