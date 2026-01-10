using System;
using System.Text;
using UnityEngine;

namespace TFramework.UIEffect
{
    [RequireComponent(typeof(UIEffect))]
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public sealed class UVFlowEffect : MonoBehaviour
    {
       
        private static readonly int MoveSpeed = Shader.PropertyToID("_MoveSpeed");
        private static readonly int MoveDirection = Shader.PropertyToID("_MoveDirection");
        private static readonly int UVFlow = Shader.PropertyToID("_IsUVFlow");
        public const string EffectKey = "UVFLOW_ON";
        public UIEffect effect;
        public float moveSpeed = 1;
        public Vector2 moveDirection = new Vector2(1,0);

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
            effect.graphic.material.SetFloat(UVFlow,1);
            effect.graphic.material.EnableKeyword(EffectKey);
            moveSpeed = effect.graphic.material.GetFloat(MoveSpeed);
            moveDirection = effect.graphic.material.GetVector(MoveDirection);
        }
        [ContextMenu("DisableKey")]
        public void DisableKey()
        {
            effect.graphic.material.SetFloat(UVFlow,0);
            effect.graphic.material.DisableKeyword(EffectKey);
        }
        

        private void OnValidate()
        {
            if(effect == null)
                return;
            effect.graphic.material.SetFloat(MoveSpeed, moveSpeed);
            effect.graphic.material.SetVector(MoveDirection, moveDirection);
        }
    }
}