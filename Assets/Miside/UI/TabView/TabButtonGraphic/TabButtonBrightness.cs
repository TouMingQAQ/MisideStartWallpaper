using System;
using DDB.UI;
using UnityEngine;

namespace TFramework.Component.UI
{
    [RequireComponent(typeof(GraphicCollect))]
    public class TabButtonBrightness : MonoBehaviour,ITabButtonGraphic
    {

        public GraphicCollect collect;
        public float showBrightness = 1;
        public float hideBrightness = 0.8f;

        public void OnInit()
        {
            collect.Init();
        }

        public void OnShow()
        {
            collect.SetBrightness(showBrightness);
        }

        public void OnHide()
        {
            collect.SetBrightness(hideBrightness);
        }

        private void Reset()
        {
            TryGetComponent(out collect);
        }
    }
}