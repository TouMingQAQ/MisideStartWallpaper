using System;
using DG.Tweening;
using UnityEngine;

public class AndroidSetting : MonoBehaviour
{
    public RectTransform root;

    public void HideSetting()
    {
        root.gameObject.SetActive(false);
    }
    private void Awake()
    {
#if !UNITY_ANDROID
        DestroyImmediate(gameObject);
#endif
    }
    private void OnEnable()
    {
        root.DOAnchorPosY(250,0.3f).SetEase(Ease.Linear).SetLoops(1).SetAutoKill(true).Play();
    }

    private void OnDisable()
    {
        root.anchoredPosition = new Vector2(0, -100);
    }

   

    public void SetWallpaper()
    {
#if UNITY_ANDROID
        using AndroidJavaClass javaClass = new AndroidJavaClass("com.misideStart.wallpaper.UnityWallpaperService");
        javaClass.CallStatic("SetWallpaper");
#endif
    }
}
