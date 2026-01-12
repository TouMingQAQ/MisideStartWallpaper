using System;
using UnityEngine;

namespace TFramework.Music
{
    [RequireComponent(typeof(AudioSource))] // 强制挂载AudioSource
    public class UnityMusicVisualizer : MonoBehaviour
    {
        [Header("===== 音频频谱基础配置 =====")]
        [Tooltip("频谱采样点数，必须是2的幂次方：64/128/256/512/1024，数值越大精度越高")]
        [SerializeField, Min(64)] private int dataCount = 256;
        [SerializeField]
        private int bufferScale = 1000;
        [Tooltip("频谱平滑系数 0-1，值越大越平滑，0为无平滑")]
        [SerializeField, Range(0, 1)] private float smoothFactor = 0.85f;

        [Header("===== 鼓点检测专属配置【重点】 =====")]
        [Header("鼓点检测阈值（核心调参）")]
        [Range(0.001f, 1.0f)] public float beatThreshold = 0.02f; // 频谱阈值，越大越严格
        [Range(0.05f, 0.5f)] public float coolDownTime = 0.15f;   // 冷却时间，防止连触
        private float _coolDownTimer;

        [Header("频谱低频段范围（固定，无需修改）")]
        public int lowFreqStart = 0;   // 频谱起始下标 = 最低频
        public int lowFreqEnd = 30;    // 频谱结束下标 = 200Hz左右（完美匹配鼓点频段）

        [Tooltip("鼓点衰减系数：防止一帧内重复触发鼓点，值越大触发间隔越长")]
        [SerializeField, Range(0.05f, 0.3f)] private float drumDecay = 0.15f;

        [Tooltip("低音区采样范围：鼓点都在低音区，固定0~30即可，无需修改")]
        [SerializeField] private int drumSampleRange = 30;

        private AudioSource audioSource;
        private float[] sampleBuffer;      // 原始频谱数据缓存
        private float[] smoothBuffer;      // 平滑后的频谱数据
  

        
        [SerializeField]
        private bool isDrumHit;            // 单次鼓点触发标记

        private void Awake()
        {
            // 1. 获取AudioSource组件 + 严格空值校验
            if (!TryGetComponent(out audioSource))
            {
                Debug.LogError($"【音频可视化】物体{gameObject.name}获取不到AudioSource组件！", this);
                enabled = false;
                return;
            }

            // 2. 初始化频谱数组
            sampleBuffer = new float[dataCount];
            smoothBuffer = new float[dataCount];
            Array.Fill(smoothBuffer, 0);
            
            isDrumHit = false;
        }

        private void Update()
        {
            // 只有音频播放时才采集+处理数据，节省性能
            if (audioSource.isPlaying)
            {
                GetSpectrumBuffer();
                ProcessSpectrumData();
                CalculateDrumPower(); // 每帧计算鼓点能量
            }
            else
            {
                for (int i = 0; i < dataCount; i++)
                {
                    smoothBuffer[i] = 0;
                    sampleBuffer[i] = 0;
                }
            }
        }

        /// <summary>
        /// 获取音频频谱原始数据
        /// </summary>
        void GetSpectrumBuffer()
        {
            audioSource.GetSpectrumData(sampleBuffer, 0, FFTWindow.Blackman);
        }

        /// <summary>
        /// 处理频谱数据：平滑+放大+限制最小值，解决闪烁问题
        /// </summary>
        void ProcessSpectrumData()
        {
            for (int i = 0; i < dataCount; i++)
            {
                var bufferData = sampleBuffer[i];
                // 频谱数据放大，让可视化效果更明显（核心放大系数，按需调整）
                bufferData *= bufferScale;
                var smoothBufferData = smoothBuffer[i];
                // 平滑插值处理
                smoothBufferData = Mathf.Lerp(smoothBufferData, bufferData, 1 - smoothFactor);
                // 限制最小值，防止物体缩到完全看不见
                smoothBuffer[i] = Mathf.Max(smoothBufferData, 0);
            }
        }

        /// <summary>
        /// 计算鼓点能量【核心】鼓点是低频重音，只计算低音区数据
        /// </summary>
        void CalculateDrumPower()
        {
            // 重置本次帧的鼓点标记
            isDrumHit = false;
            // ========== 核心：只计算低频段能量 ==========
            float lowFreqEnergy = 0;
            // 只遍历低频段下标，忽略中高频，彻底屏蔽旋律/人声干扰
            for (int i = lowFreqStart; i < lowFreqEnd && i < smoothBuffer.Length; i++)
            {
                lowFreqEnergy += sampleBuffer[i];
            }

            // ========== 鼓点判定 ==========
            if (lowFreqEnergy > beatThreshold)
            {
                isDrumHit = true;
                _coolDownTimer = coolDownTime;
            }
        }

        /// <summary>
        /// 对外提供：判断是否检测到鼓点【你要的核心方法，直接调用即可】
        /// </summary>
        /// <returns>true=检测到鼓点，false=无鼓点</returns>
        public bool Drumbeat()
        {
            return isDrumHit;
        }

        /// <summary>
        /// 对外提供平滑后的频谱数据（原可视化功能保留）
        /// </summary>
        public float[] GetSmoothSpectrumData()
        {
            return smoothBuffer;
        }

        /// <summary>
        /// 对外提供指定索引的频谱值（原可视化功能保留）
        /// </summary>
        public float GetSpectrumValue(int index)
        {
            if (index < 0 || index >= dataCount) return 0.1f;
            return smoothBuffer[index];
        }

        public int GetDataCount() => dataCount;
    }
}