using System;
using UnityEngine;

namespace TFramework.UIEffect
{
    [RequireComponent(typeof(UIEffect))]
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public class RotateEffect : MonoBehaviour
    {
        public static string EffectKey = "ROTATE_ON";
        public static string ToggleKey = "_IsRotate";
        public static string RotateSpeedKey = "_RotateSpeed";
        public static string RotateCenterKey = "_RotateCenter";
        public UIEffect effect;
        public float rotateSpeed = 0;
        public Vector2 rotateCenter = new Vector2(0.5f,0.5f);
        private void Awake()
        {
            TryGetComponent(out effect);
        }

        private void OnEnable()
        {
            EnableKey();
        }

        private void OnDisable()
        {
            DisableKey();
        }
        

        [ContextMenu("EnableKey")]
        public void EnableKey()
        {
            effect.graphic.material.SetFloat(ToggleKey,1);
            effect.graphic.material.EnableKeyword(EffectKey);
            rotateSpeed = effect.graphic.material.GetFloat(RotateSpeedKey);
            rotateCenter = effect.graphic.material.GetVector(RotateCenterKey);
        }
        [ContextMenu("DisableKey")]
        public void DisableKey()
        {
            effect.graphic.material.SetFloat(ToggleKey,0);
            effect.graphic.material.DisableKeyword(EffectKey);
            effect.graphic.material.SetFloat(RotateSpeedKey, rotateSpeed);
        }

        private void OnValidate()
        {
            if(effect == null)
                return;
            effect.graphic.material.SetFloat(RotateSpeedKey, rotateSpeed);
            effect.graphic.material.SetVector(RotateCenterKey, rotateCenter);
        }
    }

}
