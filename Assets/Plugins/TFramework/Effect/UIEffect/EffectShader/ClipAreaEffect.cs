using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TFramework.UIEffect
{
    [RequireComponent(typeof(UIEffect))]
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public sealed class ClipAreaEffect : MonoBehaviour
    {
        public static string EffectKey = "CLIPAREA_ON";
        public static string ToggleKey = "_IsClipArea";
        public static string ClipAreaKey = "_ClipArea";
        public UIEffect effect;
        public RectTransform clipArea;
        public RectTransform currentRect;
        private void Awake()
        {
            TryGetComponent(out effect);
            TryGetComponent(out currentRect);
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
        }
        [ContextMenu("DisableKey")]
        public void DisableKey()
        {
            effect.graphic.material.SetFloat(ToggleKey,0);
            effect.graphic.material.DisableKeyword(EffectKey);
            effect.graphic.material.SetVector(ClipAreaKey, new Vector4(0,0,0,0));
        }

        private void LateUpdate()
        {
            ApplyClipArea();
        }

        [ContextMenu("Apply ClipArea")]
        public void ApplyClipArea()
        {
            Vector4 clipAreaValue = new Vector4(0,0,0,0);
            var thisRect = currentRect.rect;
            if (clipArea != null)
            {
                var clipRect = clipArea.rect;
                var width = clipRect.width/thisRect.width/2;
                var height = clipRect.height/thisRect.height/2;
                var offsetWidth = clipArea.anchoredPosition.x/thisRect.width;
                var offsetHeight = clipArea.anchoredPosition.y/thisRect.height;
                offsetWidth+=0.5f;
                offsetHeight+=0.5f;
                var x = -width+offsetWidth;
                var y = -height+offsetHeight;
                var z = +width+offsetWidth;
                var w = +height+offsetHeight;
                clipAreaValue = new Vector4(x, y, z, w);
            }
            effect.graphic.material.SetVector(ClipAreaKey, clipAreaValue);
        }

        private void OnValidate()
        {
            ApplyClipArea();
        }
    }
}