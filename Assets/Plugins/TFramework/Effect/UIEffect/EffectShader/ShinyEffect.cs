using System;
using UnityEngine;

namespace TFramework.UIEffect
{
    [RequireComponent(typeof(UIEffect))]
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public sealed class ShinyEffect : MonoBehaviour
    {
        public static string EffectKey = "SHINY_ON";
        public static string ToggleKey = "_IsShiny";
        public static string ColorKey = "_ShinyColor";
        public static string RotateKey = "_ShinyRotate";
        public static string SpeedKey = "_ShinySpeed";
        public static string DirectionKey = "_ShinyDirection";
        public static string DurationKey = "_ShinyDurationTime";
        public static string STKey = "_ShinyTex_ST";
        public static string MainTexKey = "_ShinyTex";
        public UIEffect effect;
        
        
        public Texture shinyTexture;
        [ColorUsage(true,true)]
        public Color color = Color.white;
        public float rotate = 0;
        public float speed = 0.5f;
        [Range(1,10)]
        public float duration = 1;
        public Vector2 direction = new Vector2(1,0);
        public Vector2 tilling = new Vector2(1,1);

        private void Awake()
        {
            TryGetComponent(out effect);
        }

        private void OnEnable()
        {
            EnableKey();
            color = effect.graphic.material.GetColor(ColorKey);
            rotate = effect.graphic.material.GetFloat(RotateKey);
            speed = effect.graphic.material.GetFloat(SpeedKey);
            direction = effect.graphic.material.GetVector(DirectionKey);
            shinyTexture = effect.graphic.material.GetTexture(MainTexKey);
            var ts = effect.graphic.material.GetVector(STKey);
            tilling = new Vector2(ts.x,ts.y);
            duration = effect.graphic.material.GetFloat(DurationKey);
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
        }

        private void OnValidate()
        {
            if(effect == null)
                return;
            effect.graphic.material.SetColor(ColorKey, color);
            effect.graphic.material.SetFloat(RotateKey, rotate);
            effect.graphic.material.SetFloat(SpeedKey, speed);
            effect.graphic.material.SetVector(DirectionKey, direction);
            effect.graphic.material.SetTexture(MainTexKey, shinyTexture);
            effect.graphic.material.SetVector(STKey, new Vector4(tilling.x,tilling.y,0,0));
            effect.graphic.material.SetFloat(DurationKey, duration);
        }
        
    }
}