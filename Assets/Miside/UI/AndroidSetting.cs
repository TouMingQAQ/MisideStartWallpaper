using System;
using DG.Tweening;
using TFramework.Component.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidSetting : MonoBehaviour
{
    public RectTransform root;
    public CanvasGroup canvasGroup;
    public TabGroup tabGroup;
    Sequence sequence;
    private void Awake()
    {
        canvasGroup.alpha = 0;
        root.anchoredPosition = new  Vector2(-1080, 0);
        sequence = DOTween.Sequence();
        sequence.Insert(0,canvasGroup.DOFade(1, 0.4f).SetEase(Ease.InOutSine));
        sequence.Insert(0.2f,root.DOAnchorPosX(0, 0.3f).SetEase(Ease.InOutSine));
        sequence.SetLoops(1).SetAutoKill(false);
#if !UNITY_ANDROID
        DestroyImmediate(gameObject);
#endif
    }
    public void IsWallpaper()
    {
        DestroyImmediate(this.gameObject);
    }
    public void HideSetting()
    {
        sequence.PlayBackwards();
    }
    

    public void ShowSetting()
    {
        tabGroup.Select(0);
        sequence.PlayForward();
    }
    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
    
    public void SetWallpaper()
    {
#if UNITY_ANDROID
        using AndroidJavaClass javaClass = new AndroidJavaClass("com.misideStart.wallpaper.UnityWallpaperService");
        javaClass.CallStatic("SetWallpaper");
#endif
    }
}
