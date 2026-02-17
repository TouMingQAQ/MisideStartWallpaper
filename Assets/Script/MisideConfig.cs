using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering.Universal;
[Serializable]
public class MiSideConfig
{
    /// <summary>
    /// 开场动画范围
    /// </summary>
    public Vector2Int StartAnimationRange;
    /// <summary>
    /// 视野跟踪幅度X
    /// </summary>
    public Vector2 LookAtOffsetMultiplierX;
    /// <summary>
    /// 视野跟踪幅度Y
    /// </summary>
    public Vector2 LookAtOffsetMultiplierY;
    /// <summary>
    /// 限制帧率
    /// </summary>
    public int TargetFrameRate;
    
    /// <summary>
    /// 跟随鼠标
    /// </summary>
    public LookAtState LookAtState;
    /// <summary>
    /// 跟随音乐点头
    /// </summary>
    public bool MusicHead;
    

    /// <summary>
    /// 触发点击动画的连续点击次数
    /// </summary>
    public int ClickCount;

    /// <summary>
    /// 点击时是否播放音频
    /// </summary>
    public bool PlaySoundOnClick;
    

    /// <summary>
    /// TAA抗锯齿质量
    /// </summary>
    public TemporalAAQuality TAAQuality;

    /// <summary>
    /// 安卓陀螺仪偏移倍率
    /// </summary>
    public Vector2 gyroscopeScale;

    /// <summary>
    /// 安卓陀螺仪偏移空间倍率
    /// </summary>
    public Vector2 gyroscopeSafeAreaScale;

    /// <summary>
    /// 壁纸版本
    /// </summary>
    public string WallpaperVersion;

    public void Default()
    {
        StartAnimationRange = new Vector2Int(0, 5);
        TargetFrameRate = 60;
        LookAtState = LookAtState.Always;
        MusicHead = true;
        ClickCount = 2;
        LookAtOffsetMultiplierX = new Vector4(3f, 2f);
        LookAtOffsetMultiplierY = new Vector4(3f, 5f);
        PlaySoundOnClick = true;
        TAAQuality = TemporalAAQuality.VeryLow;
        gyroscopeScale = new Vector2(20, 15);
        gyroscopeSafeAreaScale = new Vector2(1.2f, 1.1f);
        WallpaperVersion = MiSideStart.Version;
    }
}
    
[CreateAssetMenu(fileName = "MisideConfig",menuName = "Config/MisideConfig")]
public class MisideConfig : ScriptableObject
{
    public MiSideConfig value;
    private string ConfigPath
    {
        get
        {
#if UNITY_ANDROID
            return Application.persistentDataPath + "/MiSideStartConfig.json";
#else
            return Application.streamingAssetsPath + "/MiSideStartConfig.json";
#endif
        }
    }

    private void Reset()
    {
        value.Default();
    }

    public void Init()
    {
        LoadConfig();
    }
    public void LoadConfig()
    {
        FileInfo fileInfo = new FileInfo(ConfigPath);
        if (fileInfo.Directory == null)
        {
            Debug.LogError($"ConfigFileError:{ConfigPath}");
            return;
        }
        if (!fileInfo.Directory.Exists)
            Directory.CreateDirectory(fileInfo.Directory.FullName);
        if (!fileInfo.Exists)
        {
            value.Default();
            var json = JsonConvert.SerializeObject(value, Formatting.Indented, new VectorConverter());
            File.WriteAllText(ConfigPath, json);
        }
        else
        {
            var json = File.ReadAllText(ConfigPath);
            try
            {
                value = JsonConvert.DeserializeObject<MiSideConfig>(json, new VectorConverter());
            }
            catch (Exception e) // 使用异常对象来记录错误信息（解决捕捉异常而忽略了异常对象本身）
            {
                Debug.LogError($"Failed to deserialize config file: {e.Message}"); // 记录错误信息（此处为记录异常信息以便于调试和维护没有省略变量名）
                value.Default();
                json = JsonConvert.SerializeObject(value, Formatting.Indented, new VectorConverter());
                File.WriteAllText(ConfigPath, json);
            }
        }
        //版本不一致时，重写配置
        if (value.WallpaperVersion != MiSideStart.Version)
        {
            value.Default();
            var json = JsonConvert.SerializeObject(value, Formatting.Indented, new VectorConverter());
            File.WriteAllText(ConfigPath, json);
        }
        
    }
    public void SaveConfig()
    {
        FileInfo fileInfo = new FileInfo(ConfigPath);
        if (fileInfo.Directory == null)
        {
            Debug.LogError($"ConfigFileError:{ConfigPath}");
            return;
        }
        if (!fileInfo.Directory.Exists)
            Directory.CreateDirectory(fileInfo.Directory.FullName);
        var json = JsonConvert.SerializeObject(value, Formatting.Indented, new VectorConverter());
        File.WriteAllText(ConfigPath, json);
    }
}