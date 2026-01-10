using System;
using UnityEngine;

namespace TFramework.UIEffect
{
    [RequireComponent(typeof(UIEffect))]
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public class NearAlphaEffect : MonoBehaviour
    {
        public static string EffectKey = "NEARALPHA_ON";
        public static string ToggleKey = "_IsNearAlpha";
        public static string NearOutAreaKey = "_NearOutArea";
        public static string NearInAreaKey = "_NearInArea";
        public static string NearAlphaKey = "_NearAlpha";
        public UIEffect effect;
        [Range(0,1)]
        public float alpha;
        public RectTransform outArea;
        public RectTransform inArea;
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
            effect.graphic.material.SetVector(NearOutAreaKey, new Vector4(0,0,0,0));
            effect.graphic.material.SetVector(NearInAreaKey, new Vector4(0,0,0,0));
        }

        private void LateUpdate()
        {
            UpdateArea();
        }

        public void UpdateArea()
        {
            Vector4 areaValue = new Vector4(0,0,0,0);
            var thisRect = currentRect.rect;
            if (outArea != null)
            {
                var areaRect = outArea.rect;
                var width = areaRect.width/thisRect.width/2;
                var height = areaRect.height/thisRect.height/2;
                var anchoredPos = ConvertAnchoredPosition(outArea, currentRect);
                var offsetWidth = anchoredPos.x/thisRect.width;
                var offsetHeight = anchoredPos.y/thisRect.height;
                offsetWidth+=0.5f;
                // offsetHeight+=0.5f;
                var x = -width+offsetWidth;
                var y = -height+offsetHeight;
                var z = +width+offsetWidth;
                var w = +height+offsetHeight;
                areaValue = new Vector4(x, y, z, w);
            }
            effect.graphic.material.SetVector(NearOutAreaKey, areaValue);
            areaValue = new Vector4(0,0,0,0);
            if (inArea != null)
            {
                var areaRect = inArea.rect;
                var width = areaRect.width/thisRect.width/2;
                var height = areaRect.height/thisRect.height/2;
                var anchoredPos = ConvertAnchoredPosition(inArea, currentRect);
                var offsetWidth = anchoredPos.x/thisRect.width;
                var offsetHeight = anchoredPos.y/thisRect.height;
                offsetWidth+=0.5f;
                // offsetHeight+=0.5f;
                var x = -width+offsetWidth;
                var y = -height+offsetHeight;
                var z = +width+offsetWidth;
                var w = +height+offsetHeight;
                areaValue = new Vector4(x, y, z, w);
            }
            effect.graphic.material.SetVector(NearInAreaKey, areaValue);
            effect.graphic.material.SetFloat(NearAlphaKey, alpha);
        }
        public Vector2 ConvertAnchoredPosition(RectTransform source, RectTransform targetParent)
        {
            // 获取source在画布空间中的位置
            Vector2 sourceScreenPosition = RectTransformUtility.WorldToScreenPoint(null, source.position);
    
            // 转换为targetParent的局部坐标
            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetParent, 
                sourceScreenPosition, 
                null, 
                out localPosition);
    
            // 计算anchoredPosition（考虑targetParent的锚点和轴心点）
            Vector2 sizeDelta = targetParent.sizeDelta;
            Vector2 anchorMin = targetParent.anchorMin;
            Vector2 anchorMax = targetParent.anchorMax;
            Vector2 pivot = targetParent.pivot;
    
            // 计算anchoredPosition
            Vector2 anchoredPosition = localPosition + new Vector2(
                sizeDelta.x * (pivot.x - anchorMin.x),
                sizeDelta.y * (pivot.y - anchorMin.y));
    
            return anchoredPosition;
        }
    }
}
