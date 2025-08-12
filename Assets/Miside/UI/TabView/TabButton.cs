using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TFramework.Component.UI
{
    public class TabButton : MonoBehaviour,IPointerClickHandler
    {
        public int tabIndex;
        public TabGroup group;

        private List<ITabButtonGraphic> _graphics = new();
        
        public virtual void Init()
        {
            _graphics.Clear();
            var gs = GetComponents<ITabButtonGraphic>();
            _graphics.AddRange(gs);
            gs = transform.GetComponentsInChildren<ITabButtonGraphic>();
            _graphics.AddRange(gs);
            foreach (var graphic in _graphics)
            {
                graphic.OnInit();
            }
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            group?.Select(tabIndex);
        }

        public virtual void Show()
        {
            foreach (var graphic in _graphics)
            {
                graphic.OnShow();
            }
        }

        public virtual void Hide()
        {
            foreach (var graphic in _graphics)
            {
                graphic.OnHide();
            }
        }
    }

    public interface ITabButtonGraphic
    {
        public void OnInit();
        public void OnShow();
        public void OnHide();
    }
}