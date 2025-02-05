using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VInspector;

namespace TFramework.Component.UIComponent.Other
{
    [RequireComponent(typeof(RectTransform))]
    [ExecuteAlways]
    public abstract class RebuildElementSync : UIBehaviour,ILayoutGroup
    {
        [ReadOnly]
        public Vector2 nowScreenSize;
        public Vector2 screenAnchorSize = new Vector2(1920, 1080);

        private Vector2 _screenSizeBuffer;
        public RectTransform rectTransform;
        [ContextMenu("ReLoad")]
        void ReLoad()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        protected override void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            base.Awake();
        }
#if UNITY_EDITOR
        protected override void OnValidate()
        {
            OnRebuild();
            base.OnValidate();
        }

#endif

        protected virtual void LateUpdate()
        {
#if UNITY_ANDROID
            nowScreenSize = new Vector2(Screen.safeArea.width, Screen.safeArea.height);
#else
            nowScreenSize = new Vector2(Screen.width, Screen.height);
#endif
        }


        protected abstract void OnRebuild();

        public void SetLayoutHorizontal()
        {
            OnRebuild();
        }

        public void SetLayoutVertical()
        {
            OnRebuild();
        }
    }

}
