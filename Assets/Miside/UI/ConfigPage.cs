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
        var config = MiSideStart.instance.config.value;
        animationRange.Value = config.StartAnimationRange;
        lookAtOffsetX.Value = new  Vector2(config.LookAtOffsetMultiplierX.x,config.LookAtOffsetMultiplierX.y);
        lookAtOffsetY.Value = new  Vector2(config.LookAtOffsetMultiplierY.y,config.LookAtOffsetMultiplierY.y);
        gyroscopeScale.Value = config.gyroscopeScale;
        gyroscopeAreaScale.Value = config.gyroscopeSafeAreaScale;
        playSoundOnClick.isOn = config.PlaySoundOnClick;
        version.text = $"Version: {Application.version}";
    }

    public void SaveConfig()
    {
        var config = MiSideStart.instance.config.value;
        config.StartAnimationRange = animationRange.Value;
        var lookAtOffsetXValue = lookAtOffsetX.Value;
        var lookAtOffsetYValue = lookAtOffsetY.Value;
        config.LookAtOffsetMultiplierX = new Vector2(lookAtOffsetXValue.x,lookAtOffsetXValue.y);
        config.LookAtOffsetMultiplierY = new Vector2(lookAtOffsetYValue.x,lookAtOffsetYValue.y);
        config.gyroscopeSafeAreaScale = gyroscopeAreaScale.Value;
        config.gyroscopeScale = gyroscopeScale.Value;
        config.PlaySoundOnClick = playSoundOnClick.isOn;
        MiSideStart.instance.SaveConfig();
    }

    public void ResetConfig()
    {
        MiSideStart.instance.config.value.Default();
        LoadConfig();
        MiSideStart.instance.SaveConfig();
    }
}
