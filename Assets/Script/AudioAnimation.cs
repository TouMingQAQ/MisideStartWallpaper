using System;
using System.Collections.Generic;
using System.IO;
using CSCore.SoundIn;
using CSCore.Streams;
using UnityEngine;
using VInspector;

public class AudioAnimation : MonoBehaviour
{
    private WasapiLoopbackCapture capture;
    private SoundInSource soundInSource;
    private byte[] buffer;
    [SerializeField]
    private float[] audioSamples;
    private int offset;
    private int byteCount;
   
    public MiSideStart miside;
    public float nodEnergy;
    [SerializeField,ReadOnly]
    private float currentEnergy;
    [SerializeField,ReadOnly]
    private float disEnergy;
    [SerializeField,ReadOnly]
    private bool nod = false;
    private void Awake()
    {
        capture = new WasapiLoopbackCapture();
        capture.Initialize();
        capture.Start();
        soundInSource = new SoundInSource(capture);
        soundInSource.DataAvailable += OnDataAvailable;
    }

    private void OnDestroy()
    {
        soundInSource.Dispose();
        capture.Stop();
        capture.Dispose();
    }

    private void Update()
    {
        if(!MiSideStart.config.MusicHead)
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
        int count = sampleCount-startCount;
        audioSamples = new float[count];
        float energy = 0;
        for (int i = startCount; i < sampleCount; i++)
        {
            var sample = BitConverter.ToSingle(buffer, i * sizeof(float));
            audioSamples[i] = sample;
            energy += sample * sample; // 能量为振幅的平方和
        }
        energy /= count; // 平均能量
        disEnergy = currentEnergy - energy;
        currentEnergy = energy;
        if (currentEnergy > nodEnergy && disEnergy > 0)
            nod = true;
    }
  

}
