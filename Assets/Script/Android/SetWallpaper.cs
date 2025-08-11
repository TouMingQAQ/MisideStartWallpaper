using System;
using UnityEngine;

public class SetWallpaper : MonoBehaviour
{
    public static SetWallpaper wallpaper;
    public GameObject root;
    private void Awake()
    {
        wallpaper = this;
#if !UNITY_ANDROID
        root.SetActive(false);    
#endif
    }

    public static void SetPreview(bool isPreview) => SetWallpaper.wallpaper.IsPreview(isPreview);
    public void IsPreview(bool isPreview)
    {
        root.SetActive(isPreview);
    }

    public void SetWallpaperBtn()
    {
#if UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.MisideWallpaperService");
        unityPlayer.CallStatic("SetWallpaper");
#endif
    }
}
