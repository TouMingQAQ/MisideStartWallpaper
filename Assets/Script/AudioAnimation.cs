using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using VInspector;
using CSCore.SoundIn;
using CSCore.Streams;

public class MitaNodController : MonoBehaviour
{
    private WasapiLoopbackCapture capture;
    private SoundInSource soundInSource;
    private byte[] buffer;
    [SerializeField]
    private float[] audioSamples;
    private int byteCount;
    private int offset;
    [SerializeField, ReadOnly]
    public bool nod { get; private set; }
    [SerializeField] // 设置调试信息，在Inspector面板中显示
    private bool verbose = false;
    [SerializeField, ReadOnly]
    private float currentEnergy;
    private float nodEnergy = 0.01f;
    private bool isListening = false;

    private void Awake()
    {
        Initialize(1024);
        StartListening(); // 开始监听
    }

    private void Initialize(int bufferSize)
    {
        capture = new WasapiLoopbackCapture();
        soundInSource = new SoundInSource(capture);
        soundInSource.DataAvailable += onDataRecv;
        audioSamples = new float[bufferSize];
        currentEnergy = 0;
        nod = false;
        isListening = false;
    }

    public void StopListening()
    {
        try
        {
            capture.Stop();
            capture.Dispose();
            isListening = false;
            if (verbose)
            {
                Debug.Log("Stopped listening for mita nod.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error stopping listening: " + ex.Message);
        }
    }

    public void StartListening()
    {
        try
        {
            capture.Initialize();
            capture.Start();
            isListening = true;
            if (verbose)
            {
                Debug.Log("Started listening for mita nod.");
            }
        }
        catch (Exception ex)
        {
            string errMsg = $"Error starting listening: {typeof(ex).Name} - {ex.Message}";
            Debug.LogError(errMsg);
            if (verbose)
            {
                string fullErrMsg = $"\nVerbose mode enabled. Here is a StackTrace:\n{ex.StackTrace}";
                Debug.LogError(fullErrMsg); // 输出完整的错误信息，包括堆栈跟踪
            }
        }
    }

    public bool CanNod()
    {
        return nod;
    }

    private void onDataRecv(object sender, DataAvailableEventArgs e)
    {
        buffer = e.Data;
        byteCount = e.ByteCount;
        offset = e.Offset;
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

        float energy = 0;
        for (int i = startCount; i < sampleCount; i++)
        {
            var sample = BitConverter.ToSingle(buffer, i * sizeof(float));
            audioSamples[i - startCount] = sample;
            energy += sample * sample; // 能量为振幅的平方和
        }
        energy /= count; // 平均能量

        float disEnergy = currentEnergy - energy;
        currentEnergy = energy;

        nod = currentEnergy > nodEnergy && disEnergy > 0;

        if (verbose)
        {
            string debugStr = $"Energy: {currentEnergy:F2} DisEnergy: {disEnergy:F2} Nod: {nod}";
            Debug.Log(debugStr);
        }
    }

    private void OnDestroy()
    {
        StopListening();
    }
}