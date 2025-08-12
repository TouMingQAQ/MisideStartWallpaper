using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DDB.UI
{
    public class GraphicCollect : MonoBehaviour
    {
        public List<MaskableGraphic> graphics = new ();
        private Dictionary<MaskableGraphic,Color> normalColorMap = new ();
        
        private bool initialized = false;
        public void Init()
        {
            if(initialized)
                return;
            normalColorMap.Clear();
            foreach (var graphic in graphics)
            {
                normalColorMap.Add(graphic, graphic.color);
            }

            initialized = true;
        }

        private void Reset()
        {
            var cgs = GetComponentsInChildren<MaskableGraphic>();
            graphics.Clear();
            foreach (var g in cgs)
            {
                if (g is TMP_SubMeshUI)
                    continue;
                graphics.Add(g);
            }
        }

        public void CrossFadeAlpha(float alpha,float duration,bool ignoreTimeScale = false)
        {
            foreach (var graphic in graphics)
            {
                graphic.CrossFadeAlpha(alpha,duration,ignoreTimeScale);
            }
        }

        public void SetAlpha(float alpha)
        {
            foreach (var graphic in graphics)
            {
                var color = graphic.color;
                color.a = alpha;
                graphic.color = color;
            }
        }
        public void SetBrightness(float brightness)
        {
            if(!initialized)
                Init();
            foreach (var graphic in graphics)
            {
                var normalColor = normalColorMap[graphic];
                var alpha = normalColor.a;
                normalColor *= brightness;
                normalColor.a = alpha;
                graphic.color = normalColor;
            }
        }
        
    }
}