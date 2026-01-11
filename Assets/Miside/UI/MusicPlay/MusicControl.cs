using System;
using TFramework.Music;
using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    public UnityMusicGroup musicGroup;

    [SerializeField]
    private Button lastBtn;
    [SerializeField]
    private Button playPauseBtn;
    [SerializeField]
    private Button nextBtn;
    [SerializeField]
    private Button volumeBtn;
    
    [SerializeField]
    private Image playPauseImg;
    [SerializeField]
    private Sprite playSprite;
    [SerializeField]
    private Sprite pauseSprite;
    [SerializeField]
    private Image volumeImage;
    [SerializeField]
    private Sprite muteVolumeSprite;

    [SerializeField]
    private MusicVolumeControl volumeControl;

    [SerializeField]
    private Text musicProgressText;
    [SerializeField]
    private Slider musicProgress;

    [SerializeField]
    private Dropdown dropDown;

    private void Awake()
    {
        dropDown.onValueChanged.AddListener(OnDropDownSelect);
    
        musicProgress.maxValue = 1;
        musicProgress.minValue = 0;
        
        lastBtn.onClick.AddListener(OnLastBtn);
        playPauseBtn.onClick.AddListener(OnPlayPauseBtn);
        nextBtn.onClick.AddListener(OnNextBtn);
        volumeBtn.onClick.AddListener(OnSwitchMute);
    }

    public void RefreshView()
    {
        var currentIndex = musicGroup.CurrentIndex;
        var infoGroup = musicGroup.MusicInfoList;
        var totalCount = infoGroup.Count;
        lastBtn.interactable = currentIndex > 0;
        nextBtn.interactable = currentIndex < totalCount;
        playPauseImg.overrideSprite = musicGroup.IsPlaying ? pauseSprite : playSprite;
        volumeImage.overrideSprite = musicGroup.IsMute ? muteVolumeSprite : null;
        volumeControl.RefreshView();
        dropDown.value = currentIndex;
        UpdateMusicProgress();
    }

    /// <summary>
    /// 高频方法
    /// </summary>
    public void UpdateMusicProgress()
    {
        musicProgressText.text = musicGroup.TimeStr;
        musicProgress.value = musicGroup.Progress;
    }

    public void RefreshDropDown()
    {
        var infoGroup = musicGroup.MusicInfoList;
        dropDown.options.Clear();
        foreach (var musicInfo in infoGroup)
        {
            dropDown.options.Add(new Dropdown.OptionData()
            {
                image = musicInfo.MusicCover,
                text = $"{musicInfo.MusicName}[{musicInfo.MusicWriter}]"
            });
        }
    }

    void OnDropDownSelect(int index)
    {
        musicGroup.Play(index);
        RefreshView();
    }

    void OnLastBtn()
    {
        musicGroup.Last();
        RefreshView();
    }

    void OnNextBtn()
    {
        musicGroup.Next();
        RefreshView();
    }

    void OnPlayPauseBtn()
    {
        musicGroup.PlayPause();
        RefreshView();
    }

    void OnSwitchMute()
    {
        musicGroup.SwitchMute();
        RefreshView();
    }
}
