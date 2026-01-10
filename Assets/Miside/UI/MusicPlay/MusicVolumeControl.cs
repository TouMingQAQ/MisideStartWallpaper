using System;
using DG.Tweening;
using TFramework.Music;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicVolumeControl : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IScrollHandler
{
    [SerializeField]
    private MusicControl musicControl;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Text volumeText;
    [SerializeField]
    private RectTransform volumeView;

    private Tween show;

    private void Awake()
    {
        volumeView.localScale = new Vector3(1, 0, 1);
        show = volumeView.DOScale(new Vector3(1,1,1), 0.2f).SetLoops(1).Pause();
        volumeSlider.maxValue = 1;
        volumeSlider.minValue = 1;
    }

    public void RefreshView()
    {
        volumeSlider.value = musicControl.musicGroup.Volume;
        volumeText.text = musicControl.musicGroup.Volume.ToString("P0");
    }

    private void OnEnable()
    {
        volumeView.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        volumeView.gameObject.SetActive(true);
        show.Restart();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        volumeView.gameObject.SetActive(false);
    }

    public void OnScroll(PointerEventData eventData)
    {
        musicControl.musicGroup.Volume += eventData.delta.y;
        RefreshView();
    }
}
