//RealToon - Custom Shadow Resolution
//MJQStudioWorks
//©2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealToon.Script
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Light))]
    [AddComponentMenu("RealToon/Tools/Custom Shadow Resolution")]
    public class CustomShadowResolution : MonoBehaviour
    {
        [Header("Custom Shadow Resolution V1.0.0")]
        [Header("Note: Higher Shadow Resolution = More GPU RAM Usage.")]
        [Header("For RealToon Built-In RP Only")]

        [Space(10)]

        [SerializeField]
        [Tooltip("Input value")]
        public int Value = 2048;

        [SerializeField]
        [Tooltip("Final Resolution (Value * 2)")]
        public int FinalResolution = 4096;

        [Space(10)]

        [SerializeField]
        [Tooltip("Reset to default value")]
        public bool Reset = false;

        void Update()
        {
            this.GetComponent<Light>().shadowCustomResolution = FinalResolution;
            FinalResolution = Value * 2;

            if (Reset == true)
            {
                Value = 2048;
                FinalResolution = 4096;
                Reset = false;
            }

            if (Value < 0)
            {

                Value = 0;
                FinalResolution = 0;

            }
        }

    }
}
