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

    private Queue<float> zeroCrossingRateHistory = new Queue<float>(10);
    private Queue<float> shortTimeEnergyHistory = new Queue<float>(10);
    private Queue<float> spectralCentroidHistory = new Queue<float>(10);
    private Queue<float> spectralFlatnessHistory = new Queue<float>(10);

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

    public bool CheckIsMusic()
        {
            if (!IsListening)
            {
                return false;
            }

            try
            {
                float zeroCrossingRate = CalculateZeroCrossingRate(audioSamples);
                float shortTimeEnergy = CalculateShortTimeEnergy(audioSamples);
                float spectralCentroid = CalculateSpectralCentroid(audioSamples, 44100); // 假设采样率为44100
                float spectralFlatness = CalculateSpectralFlatness(audioSamples);

                // 更新历史数据
                zeroCrossingRateHistory.Enqueue(zeroCrossingRate);
                shortTimeEnergyHistory.Enqueue(shortTimeEnergy);
                spectralCentroidHistory.Enqueue(spectralCentroid);
                spectralFlatnessHistory.Enqueue(spectralFlatness);

                if (zeroCrossingRateHistory.Count > 10)
                {
                    zeroCrossingRateHistory.Dequeue();
                    shortTimeEnergyHistory.Dequeue();
                    spectralCentroidHistory.Dequeue();
                    spectralFlatnessHistory.Dequeue();
                }

                // 计算平均值
                float avgZeroCrossingRate = zeroCrossingRateHistory.Average();
                float avgShortTimeEnergy = shortTimeEnergyHistory.Average();
                float avgSpectralCentroid = spectralCentroidHistory.Average();
                float avgSpectralFlatness = spectralFlatnessHistory.Average();

                const float zeroCrossingRateThreshold = 0.05f;
                const float shortTimeEnergyThreshold = 0.01f;
                const float spectralCentroidThreshold = 1000.0f; // 1000 Hz
                const float spectralFlatnessThreshold = 0.5f;

                return avgZeroCrossingRate > zeroCrossingRateThreshold &&
                       avgShortTimeEnergy > shortTimeEnergyThreshold &&
                       avgSpectralCentroid > spectralCentroidThreshold &&
                       avgSpectralFlatness > spectralFlatnessThreshold;
            }
            catch (Exception ex)
            {
                return false;
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

        private float CalculateSpectralCentroid(float[] audioSamples, int sampleRate)
        {
            float numerator = 0;
            float denominator = 0;
            float[] magnitudes = new float[audioSamples.Length / 2];

            // 计算频谱
            for (int i = 0; i < audioSamples.Length / 2; i++)
            {
                magnitudes[i] = (float)Math.Sqrt(audioSamples[2 * i] * audioSamples[2 * i] + audioSamples[2 * i + 1] * audioSamples[2 * i + 1]);
            }

            // 计算频谱质心
            for (int i = 0; i < magnitudes.Length; i++)
            {
                float frequency = i * (float)sampleRate / audioSamples.Length;
                numerator += frequency * magnitudes[i];
                denominator += magnitudes[i];
            }

            return numerator / denominator;
        }

        private float CalculateSpectralFlatness(float[] audioSamples)
        {
            float geometricMean = 0;
            float arithmeticMean = 0;

            // 计算频谱
            float[] magnitudes = new float[audioSamples.Length / 2];
            for (int i = 0; i < audioSamples.Length / 2; i++)
            {
                magnitudes[i] = (float)Math.Sqrt(audioSamples[2 * i] * audioSamples[2 * i] + audioSamples[2 * i + 1] * audioSamples[2 * i + 1]);
            }

            // 计算几何平均和算术平均
            for (int i = 0; i < magnitudes.Length; i++)
            {
                geometricMean += (float)Math.Log(magnitudes[i]);
                arithmeticMean += magnitudes[i];
            }

            geometricMean = (float)Math.Exp(geometricMean / magnitudes.Length);
            arithmeticMean /= magnitudes.Length;

            return geometricMean / arithmeticMean;
        }

    private float SmoothingFilter(float value)
    {
        return smoothingFactor * previousEnergy + (1 - smoothingFactor) * value;
    }
}
