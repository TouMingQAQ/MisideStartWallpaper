using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFramework.Component.UI
{
    public class TabButtonColor : MonoBehaviour,ITabButtonGraphic
    {
        public Graphic target;
        public Color hideColor;
        public Color showColor;

        public void OnInit()
        {
            
        }

        public void OnShow()
        {
            target.color = showColor;
        }

        public void OnHide()
        {
            target.color = hideColor;
        }
    }
}