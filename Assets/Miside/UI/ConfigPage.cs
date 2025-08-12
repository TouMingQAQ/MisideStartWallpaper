using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPage : MonoBehaviour
{
    public Vector2IntInput animationRange;
    public Vector2Input lookAtOffsetX;
    public Vector2Input lookAtOffsetY;
    public Vector2Input gyroscopeScale;
    public Vector2Input gyroscopeAreaScale;
    public Toggle playSoundOnClick;
    public TMP_Text version;

    private void OnEnable()
    {
        LoadConfig();
    }

    void LoadConfig()
    {
        var config = MiSideStart.config;
        animationRange.Value = config.StartAnimationRange;
        lookAtOffsetX.Value = new  Vector2(config.LookAtOffsetMultiplier.x,config.LookAtOffsetMultiplier.z);
        lookAtOffsetY.Value = new  Vector2(config.LookAtOffsetMultiplier.y,config.LookAtOffsetMultiplier.w);
        gyroscopeScale.Value = config.gyroscopeScale;
        gyroscopeAreaScale.Value = config.gyroscopeSafeAreaScale;
        playSoundOnClick.isOn = config.PlaySoundOnClick;
        version.text = $"Version: {Application.version}";
    }

    public void SaveConfig()
    {
        var config = MiSideStart.config;
        config.StartAnimationRange = animationRange.Value;
        var lookAtOffsetXValue = lookAtOffsetX.Value;
        var lookAtOffsetYValue = lookAtOffsetY.Value;
        config.LookAtOffsetMultiplier = new Vector4(lookAtOffsetXValue.x,lookAtOffsetYValue.x,lookAtOffsetXValue.y,lookAtOffsetYValue.y);
        config.gyroscopeSafeAreaScale = gyroscopeAreaScale.Value;
        config.gyroscopeScale = gyroscopeScale.Value;
        config.PlaySoundOnClick = playSoundOnClick.isOn;
        MiSideStart.config = config;
        MiSideStart.instance.SaveConfig();
    }

    public void ResetConfig()
    {
        MiSideStart.config = MiSideConfig.Default();
        LoadConfig();
        MiSideStart.instance.SaveConfig();
    }
}
