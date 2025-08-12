using UnityEngine;

public class ControlPage : MonoBehaviour
{
    public AndroidSetting androidSetting;
    /// <summary>
    /// 重启
    /// </summary>
    public void ReStart()
    {
        androidSetting.Reload();
    }

    /// <summary>
    /// 设置壁纸
    /// </summary>
    public void SetWallpaper()
    {
        androidSetting.SetWallpaper();
    }
}
