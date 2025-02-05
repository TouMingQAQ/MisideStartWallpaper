using UnityEngine;
using VInspector;

namespace TFramework.Component.UIComponent.Other
{
    public class SyncScale : RebuildElementSync
    {
        [ReadOnly] 
        public float scale;
        [SerializeField]
        private Vector2 scaleWight = Vector2.one;
        [SerializeField]
        private Vector2 limitScale = new Vector2(0,10);
        [SerializeField]
        private ScaleMode scaleMode;
        protected override void OnRebuild()
        {
            Sync();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            Sync();
        }

        void Sync()
        {
            scale = GetScale();
            transform.localScale = new Vector3(scale * scaleWight.x, scale * scaleWight.y, 1);
        }

        float GetScale()
        {
            float scaleX = nowScreenSize.x / screenAnchorSize.x;
            float scaleY = nowScreenSize.y / screenAnchorSize.y;
            switch (scaleMode)
            {
                case ScaleMode.Max:
                    return Mathf.Clamp(Mathf.Max(scaleX, scaleY),limitScale.x,limitScale.y) ;
                case ScaleMode.Min:
                    return Mathf.Clamp(Mathf.Min(scaleX, scaleY),limitScale.x,limitScale.y);
                default:
                    return 1;
            }
        }
        public enum ScaleMode
        {
            Min,Max
        }
    
    }
}
