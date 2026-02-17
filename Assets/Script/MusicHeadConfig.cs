using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
public enum MusicHeadVersion
{
    V1 = 1,
    V2 = 2
}
[Serializable]
public class MisideMusicConfig
{
    public MusicHeadVersion MusicHeadVersion = MusicHeadVersion.V1;
    public V1Info v1Info = new();
    public V2Info v2Info = new();
    [Serializable]
    public class V1Info
    {
        public float NodMinEnergy =  0.0125f;//触发阈值
    }
    [Serializable]
    public class V2Info
    {
        public float NodEnergyThreshold = 0.01f; // 初始阈值
        public float EnergyDecayFactor = 0.95f; // 衰减因子
        public float PeakDetectionThreshold = 1.5f; // 峰值检测阈值
        public float SmoothingFactor = 0.7f; // 平滑滤波因子
    }
}
[CreateAssetMenu(menuName = "Config/MusicHeadConfig",fileName = "MusicHeadConfig")]
public class MusicHeadConfig : ScriptableObject
{

    public string ConfigPath
    {
        get
        {
#if UNITY_ANDROID
        return Application.persistentDataPath + "/MusicHeadConfig.json";
#else
            return Application.streamingAssetsPath + "/MusicHeadConfig.json";
#endif
        }
    }
    public MisideMusicConfig value;
    
    public void LoadConfig()
    {
        var filePath = ConfigPath;
        FileInfo fileInfo = new FileInfo(filePath);
        if (fileInfo.Directory == null)
        {
            Debug.LogError($"ConfigFileError:{filePath}");
            return;
        }
        if (!fileInfo.Directory.Exists)
            Directory.CreateDirectory(fileInfo.Directory.FullName);
        if (!fileInfo.Exists)
        {
            value = new();
            var json = JsonConvert.SerializeObject(value, Formatting.Indented, new VectorConverter());
            File.WriteAllText(filePath, json);
        }
        else
        {
            var json = File.ReadAllText(filePath);
            try
            {
                value = JsonConvert.DeserializeObject<MisideMusicConfig>(json, new VectorConverter());
            }
            catch (Exception e) // 使用异常对象来记录错误信息（解决捕捉异常而忽略了异常对象本身）
            {
                Debug.LogError($"Failed to deserialize config file: {e.Message}"); // 记录错误信息（此处为记录异常信息以便于调试和维护没有省略变量名）
                value = new();
                json = JsonConvert.SerializeObject(value, Formatting.Indented, new VectorConverter());
                File.WriteAllText(filePath, json);
            }
        }
     
    }
}