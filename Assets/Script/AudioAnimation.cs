using System;
using System.IO;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using Newtonsoft.Json;
using UnityEngine;
using VInspector;
public enum MusicHeadVersion
{
    V1 = 1,
    V2 = 2
}
[Serializable]
public class MusicHeadConfig
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
public class AudioAnimation : MonoBehaviour
{
    private const int BufferSize = 2048;   // 缓冲区大小

    private WasapiLoopbackCapture capture;
    private SoundInSource soundInSource;
    private MMDeviceEnumerator _enumerator;
    private byte[] buffer;
    [SerializeField]
    private float[] audioSamples;
    private int offset;
    private int byteCount;

    public MiSideStart miside;
    [Tab("V1")]
    public float nodMinEnergy = 0.0125f;
    [SerializeField,ReadOnly]
    private float disEnergy;
    [Tab("V2")]
    public float nodEnergyThreshold = 0.01f; // 初始阈值
    public float energyDecayFactor = 0.95f; // 衰减因子
    public float peakDetectionThreshold = 1.5f; // 峰值检测阈值
    public float smoothingFactor = 0.7f; // 平滑滤波因子
    [SerializeField,ReadOnly]
    private float averageEnergy = 0f;
    private const float zeroCrossingRateThreshold = 0.05f;
    private const float shortTimeEnergyThreshold = 0.01f;
    [Tab("Config")]
    public MusicHeadConfig config;
    public string configPath;
    [Tab("Info")]
    [ReadOnly]
    public float currentEnergy;
    [SerializeField,ReadOnly]
    private float previousEnergy;
    [SerializeField,ReadOnly]
    private bool nod = false;
    [SerializeField,ReadOnly]
    private string audioDeviceName;
    [SerializeField,ReadOnly]
    private string audioDeviceID;



    // 添加 nodEnergy 属性
    public float nodEnergy { get; private set; }
    private bool isMusic = false; // 新增：用于存储音乐检测结果

    private void Awake()
    {
#if UNITY_ANDROID
        configPath = Application.persistentDataPath + "/MusicHeadConfig.json";
#else
        configPath = Application.streamingAssetsPath + "/MusicHeadConfig.json";
#endif
        LoadConfig();
        capture = new WasapiLoopbackCapture();
        capture.Initialize();
        capture.Start();
        soundInSource = new SoundInSource(capture);
        soundInSource.DataAvailable += OnDataAvailable;
        _enumerator = new MMDeviceEnumerator();
    }
    void LoadConfig()
    {
        FileInfo fileInfo = new FileInfo(configPath);
        if (fileInfo.Directory == null)
        {
            Debug.LogError($"ConfigFileError:{configPath}");
            return;
        }
        if (!fileInfo.Directory.Exists)
            Directory.CreateDirectory(fileInfo.Directory.FullName);
        if (!fileInfo.Exists)
        {
            config = new();
            var json = JsonConvert.SerializeObject(config, Formatting.Indented, new VectorConverter());
            File.WriteAllText(configPath, json);
        }
        else
        {
            var json = File.ReadAllText(configPath);
            try
            {
                config = JsonConvert.DeserializeObject<MusicHeadConfig>(json, new VectorConverter());
            }
            catch (Exception e) // 使用异常对象来记录错误信息（解决捕捉异常而忽略了异常对象本身）
            {
                Debug.LogError($"Failed to deserialize config file: {e.Message}"); // 记录错误信息（此处为记录异常信息以便于调试和维护没有省略变量名）
                config = new();
                json = JsonConvert.SerializeObject(config, Formatting.Indented, new VectorConverter());
                File.WriteAllText(configPath, json);
            }
        }
        nodMinEnergy = config.v1Info.NodMinEnergy;
        nodEnergyThreshold = config.v2Info.NodEnergyThreshold;
        energyDecayFactor = config.v2Info.EnergyDecayFactor;
        peakDetectionThreshold = config.v2Info.PeakDetectionThreshold;
        smoothingFactor = config.v2Info.SmoothingFactor;
    }
    void ResetCapture()
    {
        capture.Stop();
        capture.Dispose();
        soundInSource.Dispose();
        capture = new WasapiLoopbackCapture();
        capture.Initialize();
        capture.Start();
        soundInSource = new SoundInSource(capture);
        soundInSource.DataAvailable += OnDataAvailable;
    }
    
    private void OnApplicationQuit()
    {
        _enumerator?.Dispose();
        soundInSource?.Dispose();
        capture?.Stop();
        capture?.Dispose();
    }

    private void Update()
    {
        var device = _enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        var deviceName = device.FriendlyName;
        if (deviceName != audioDeviceName)
        {
            Debug.Log($"[<color=green>AudioDeviceChange</color>]:{audioDeviceName}=>{deviceName}");
            audioDeviceName = device.FriendlyName;
            audioDeviceID = device.DeviceID;
            ResetCapture();
        }
        if (!MiSideStart.config.MusicHead)
            return;
        if (config.MusicHeadVersion == MusicHeadVersion.V1)
        {
            if (nod)
            {
                miside?.NodOnShot();
                nod = false;
            }
        }
        else
        {
            if (nod && isMusic) // 确保只在检测为音乐且nod为true时执行
            {
                miside?.NodOnShot();
                nod = false;
            }
        }
     
    }

    private void OnDataAvailable(object sender, DataAvailableEventArgs e)
    {
        buffer = e.Data;
        offset = e.Offset;
        byteCount = e.ByteCount;
        ParseBuffer();
    }

    private void ParseBuffer()
    {
        if (buffer == null || buffer.Length == 0)
            return;

        int sampleCount = byteCount / sizeof(float);
        int startCount = offset / sizeof(float);
        int count = sampleCount - startCount;
        audioSamples = new float[count];
        float energy = 0;
        for (int i = startCount; i < sampleCount; i++)
        {
            var sample = BitConverter.ToSingle(buffer, i * sizeof(float));
            audioSamples[i - startCount] = sample; // Corrected index
            energy += sample * sample; // 能量为振幅的平方和
        }
        energy /= count; // 平均能量

        // 更新 nodEnergy
        nodEnergy = energy;

        if (config.MusicHeadVersion == MusicHeadVersion.V2)
        {
            // 平滑滤波
            energy = SmoothingFilter(energy);

            // 更新平均能量
            averageEnergy = averageEnergy * energyDecayFactor + energy * (1 - energyDecayFactor);

            // 检查是否为音乐
            isMusic = CheckIsMusic(audioSamples); // 实时更新音乐检测结果

            // 自适应阈值检测（仅当是音乐时）
            if (isMusic)
            {
                float adaptiveThreshold = nodEnergyThreshold + nodEnergyThreshold * averageEnergy;

                // 检测节拍
                if (energy > adaptiveThreshold && energy > peakDetectionThreshold * averageEnergy)
                {
                    nod = true;
                }
            }

            previousEnergy = energy;
            currentEnergy = energy;
        }
        else
        {
            disEnergy = currentEnergy - energy;
            currentEnergy = energy;
            if (currentEnergy > nodMinEnergy && disEnergy > 0)
                nod = true;
        }
    
    }

    private float CalculateZeroCrossingRate(float[] audioSamples)
    {
        int zeroCrossingCount = 0;
        for (int i = 1; i < audioSamples.Length; i++)
        {
            if (audioSamples[i] * audioSamples[i - 1] < 0)
            {
                zeroCrossingCount++;
            }
        }
        return (float)zeroCrossingCount / audioSamples.Length;
    }

    private float CalculateShortTimeEnergy(float[] audioSamples)
    {
        float energy = 0;
        foreach (float sample in audioSamples)
        {
            energy += sample * sample;
        }
        return energy / audioSamples.Length;
    }

    private bool CheckIsMusic(float[] audioSamples)
    {
        float zeroCrossingRate = CalculateZeroCrossingRate(audioSamples);
        float shortTimeEnergy = CalculateShortTimeEnergy(audioSamples);
        return zeroCrossingRate > zeroCrossingRateThreshold && shortTimeEnergy > shortTimeEnergyThreshold;
    }

    private float SmoothingFilter(float value)
    {
        return smoothingFactor * previousEnergy + (1 - smoothingFactor) * value;
    }
}