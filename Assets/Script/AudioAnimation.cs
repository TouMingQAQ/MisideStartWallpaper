using System;
using System.IO;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using Newtonsoft.Json;
using UnityEngine;
using VInspector;

[Serializable]

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
    public float energyDecayFactor = 0.325f; // 衰减因子
    public float peakDetectionThreshold = 0.975f; // 峰值检测阈值
    public float smoothingFactor = 0.675f; // 平滑滤波因子
    [SerializeField,ReadOnly]
    private float averageEnergy = 0f;
    private const float zeroCrossingRateThreshold = 0.05f;
    private const float shortTimeEnergyThreshold = 0.01f;
    [Tab("Config")]
    public MusicHeadConfig config;
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
#if !UNITY_ANDROID



    // 添加 nodEnergy 属性
    public float nodEnergy { get; private set; }
    private bool isMusic = false; // 新增：用于存储音乐检测结果

    private void Awake()
    {
        LoadConfig();
    }

    private void Start()
    {
        if (!MiSideStart.instance.config.value.MusicHead)
            return;
#if MISIDE_MUSIC_ON
        return;
#endif
        capture = new WasapiLoopbackCapture();
        capture.Initialize();
        capture.Start();
        soundInSource = new SoundInSource(capture);
        soundInSource.DataAvailable += OnDataAvailable;
        _enumerator = new MMDeviceEnumerator();
    }

    public void LoadConfig()
    {
        config.LoadConfig();
        nodMinEnergy = config.value.v1Info.NodMinEnergy;
        nodEnergyThreshold = config.value.v2Info.NodEnergyThreshold;
        energyDecayFactor = config.value.v2Info.EnergyDecayFactor;
        peakDetectionThreshold = config.value.v2Info.PeakDetectionThreshold;
        smoothingFactor = config.value.v2Info.SmoothingFactor;
    }
    void ResetCapture()
    {
        capture?.Stop();
        capture?.Dispose();
        soundInSource?.Dispose();
        capture = null;
        soundInSource = null;
        if (!MiSideStart.instance.config.value.MusicHead)
            return;
#if MISIDE_MUSIC_ON
        return;
#endif
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
        _enumerator = null;
        soundInSource = null;
        capture = null;
    }

    private void Update()
    {
        if (!MiSideStart.instance.config.value.MusicHead)
            return;
#if MISIDE_MUSIC_ON
        return;
#endif
        var device = _enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        var deviceName = device.FriendlyName;
        if (deviceName != audioDeviceName)
        {
            Debug.Log($"[<color=green>AudioDeviceChange</color>]:{audioDeviceName}=>{deviceName}");
            audioDeviceName = device.FriendlyName;
            audioDeviceID = device.DeviceID;
            ResetCapture();
        }
        
        if (config.value.MusicHeadVersion == MusicHeadVersion.V1)
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

        if (config.value.MusicHeadVersion == MusicHeadVersion.V2)
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
    #endif
}