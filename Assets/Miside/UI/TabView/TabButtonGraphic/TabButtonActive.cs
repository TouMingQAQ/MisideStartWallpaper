using System;
using UnityEngine;

namespace TFramework.Component.UI
{
    public class TabButtonActive : MonoBehaviour,ITabButtonGraphic
    {
        public GameObject target;
        public bool active = true;
        public void OnInit()
        {
            
        }

        public void OnShow()
        {
            target.SetActive(active);
        }

        public void OnHide()
        {
            target.SetActive(!active);
        }

        
    }
}