using System;
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

    private float averageEnergy = 0f;
    private float energyDecayFactor = 0.95f; // 衰减因子
    private float peakDetectionThreshold = 1.5f; // 峰值检测阈值
    private float smoothingFactor = 0.7f; // 平滑滤波因子

    public float nodEnergy; // 添加 nodEnergy 属性

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
        if (!MiSideStart.config.MusicHead)
            return;
        if (nod)
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

        // 将 buffer 转换为浮点数据
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

        // 平滑滤波
        energy = SmoothingFilter(energy);

        // 更新平均能量
        averageEnergy = averageEnergy * energyDecayFactor + energy * (1 - energyDecayFactor);

        // 自适应阈值检测
        float adaptiveThreshold = nodEnergy + nodEnergyThreshold * averageEnergy;

        // 检测节拍
        if (energy > adaptiveThreshold && energy > peakDetectionThreshold * averageEnergy)
        {
            nod = true;
        }

        previousEnergy = energy;
        currentEnergy = energy;
    }

    private float SmoothingFilter(float value)
    {
        return smoothingFactor * previousEnergy + (1 - smoothingFactor) * value;
    }
}
