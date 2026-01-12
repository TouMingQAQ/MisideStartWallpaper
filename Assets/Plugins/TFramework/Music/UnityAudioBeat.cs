using System;
using System.Collections.Generic;
using UnityEngine;
namespace TFramework.Music
{
    [Serializable]
    public class UnityAudioBeat : IMusicBeat
    {
        public float nodEnergyThreshold = 0.01f; // 初始阈值
        public float energyDecayFactor = 0.325f; // 衰减因子
        public float peakDetectionThreshold = 0.975f; // 峰值检测阈值
        public float smoothingFactor = 0.675f; // 平滑滤波因子
        public float averageEnergy = 0f;
        public float currentEnergy;
        public float previousEnergy;
        public float nodMinEnergy = 0.0125f;
        private float disEnergy;
        public bool beate = false;
        public void SetSample(float[] audioSamples)
        {
            if (audioSamples == null || audioSamples.Length == 0)
                return;
            beate = false;
            int count = audioSamples.Length;
            float energy = 0;
            foreach (float sample in audioSamples)
            {
                energy += sample * sample;
            }
            energy /= count; // 平均能量
            
            
            disEnergy = currentEnergy - energy;
            currentEnergy = energy;
            if (currentEnergy > nodMinEnergy && disEnergy > 0)
                beate = true;
            
            // // 平滑滤波
            // energy = SmoothingFilter(energy);
            //
            // // 更新平均能量
            // averageEnergy = averageEnergy * energyDecayFactor + energy * (1 - energyDecayFactor);
            //
            // float adaptiveThreshold = nodEnergyThreshold + nodEnergyThreshold * averageEnergy;
            //
            // // 检测节拍
            // if (energy > adaptiveThreshold && energy > peakDetectionThreshold * averageEnergy)
            // {
            //     beate = true;
            // }
            //
            // previousEnergy = energy;
            // currentEnergy = energy;
        }
        private float SmoothingFilter(float value)
        {
            return smoothingFactor * previousEnergy + (1 - smoothingFactor) * value;
        }


        public bool IsBeate() => beate;
    }
}