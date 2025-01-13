using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MitaMisiteAction.Nod
{
    public class MitaNodController
    // 米塔点头控制器
    {
        private WasapiLoopbackCapture capture;
        private byte[] buffer;
        private int byteCount;
        private float[] audioSamples;
        public bool nod { get; private set; };
        private bool verbose = false;
        private float currentEnergy = 0;
        private float nodEnergy = 0.01f;
        private object lockObject = new object(); // 用于线程同步
        private bool isListening = false;

        public void Stop()
        {
            capture.StopRecording();
            capture.Dispose();
            isListening = false;
            if (verbose)
            {
                Console.WriteLine("Stopped listening for mita nod.");
            }
        }

        public void Start()
        {
            capture.StartRecording();
            isListening = true;
            if (verbose)
            {
                Console.WriteLine("Started listening for mita nod.");
            }
        }

        public bool canNod() 
        {
            return nod;
        }

        public MitaNodController(int bufferSize = 1024,bool startListen = true, bool verbose = false)
        {
            this.verbose = verbose;
            capture = new WasapiLoopbackCapture();
            capture.DataAvailable += onDataRecv;
            audioSamples = new float[bufferSize];
            if (startListen) 
            {
                Start(); // 开始监听
            }
        }

        private void onDataRecv(object sender, WaveInEventArgs e)
        {
            buffer = e.Buffer;
            byteCount = e.BytesRecorded;
            lock (lockObject) // 确保线程安全
            {
                ParseBuffer();
            }
        }

        private void ParseBuffer()
        {
            if (buffer == null || buffer.Length % sizeof(float) != 0)
                return;

            int sampleCount = byteCount / sizeof(float);
            float energy = 0;

            for (int i = 0; i < sampleCount; i++)
            {
                var sample = BitConverter.ToSingle(buffer, i * sizeof(float));
                energy += sample * sample; // 能量为振幅的平方和
            }

            energy /= sampleCount; // 平均能量

            lock (lockObject) // 确保线程安全
            {
                float energyDifference = currentEnergy - energy;
                currentEnergy = energy;

                if (currentEnergy > nodEnergy && energyDifference > 0)
                {
                    nod = true;
                }
                else
                {
                    nod = false;
                }
                if (verbose)
                {
                    Console.WriteLine($"Energy: {currentEnergy:F3}, Nod: {nod}")
                }
            }
        }
    }
}