using CSCore.SoundIn;
using CSCore.Streams;
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
        private SoundInSource soundInSource;
        private byte[] buffer;
        [SerializeField]
        private float[] audioSamples;
        private int byteCount;
        private int offset;
        [SerializeField,ReadOnly]
        public bool nod { get; private set; };
        private bool verbose = false;
        [SerializeField,ReadOnly]
        private float currentEnergy;
        private float nodEnergy = 0.01f;
        private bool isListening = false;

        public void Stop()
        {
            capture.Stop();
            capture.Dispose();
            isListening = false;
            if (verbose)
            {
                Console.WriteLine("Stopped listening for mita nod.");
            }
        }

        public void Start()
        {
            capture.Initialize();
            capture.Start();
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
            soundInSource = new SoundInSource(capture);
            soundInSource.DataAvailable += onDataRecv;
            audioSamples = new float[bufferSize];
            if (startListen) 
            {
                Start(); // 开始监听
            }
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
            {
                nod = true;
            }
            else
            {
                nod = false;
            }
            if (verbose)
            {
                string debugStr = $"Energy: {currentEnergy:F2} DisEnergy: {disEnergy:F2} Nod: {nod}";
                Console.WriteLine(debugStr);
            }
        }
    }
}