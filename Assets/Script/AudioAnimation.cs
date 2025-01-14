using System;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using UnityEngine;

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
    public float nodEnergyThreshold = 0.01f; // 初始阈值
    [SerializeField]
    private float currentEnergy;
    [SerializeField]
    private float previousEnergy;
    [SerializeField]
    private bool nod = false;
    [SerializeField]
    private string audioDeviceName;
    [SerializeField]
    private string audioDeviceID;

    private float averageEnergy = 0f;
    private float energyDecayFactor = 0.95f; // 衰减因子
    private float peakDetectionThreshold = 1.5f; // 峰值检测阈值
    private float smoothingFactor = 0.7f; // 平滑滤波因子

    private const float zeroCrossingRateThreshold = 0.05f;
    private const float shortTimeEnergyThreshold = 0.01f;

    // 添加 nodEnergy 属性
    public float nodEnergy { get; private set; }
    private bool isMusic = false; // 新增：用于存储音乐检测结果

    private void Awake()
    {
        capture = new WasapiLoopbackCapture();
        capture.Initialize();
        capture.Start();
        soundInSource = new SoundInSource(capture);
        soundInSource.DataAvailable += OnDataAvailable;
        _enumerator = new MMDeviceEnumerator();
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

    private void OnDestroy()
    {
        _enumerator.Dispose();
        capture.Stop();
        capture.Dispose();
        soundInSource.Dispose();
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
        if (nod && isMusic) // 确保只在检测为音乐且nod为true时执行
        {
            miside.NodOnShot();
            nod = false;
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