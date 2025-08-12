using System;
using UnityEngine;
using UnityEngine.UI;

namespace TFramework.Component.UI
{
    public class TabButtonSprite : MonoBehaviour,ITabButtonGraphic
    {
        public Image targetImage;
        public bool useOverride = true;
        public Sprite showSprite;
        public Sprite hideSprite;
        
        void SetSprite(Sprite sprite)
        {
            if (useOverride)
                targetImage.overrideSprite = sprite;
            else
            {
                targetImage.overrideSprite = null;
                targetImage.sprite = showSprite;
            }
        }

        private void Reset()
        {
            TryGetComponent(out targetImage);
        }

        public void OnInit()
        {
            
        }

        public void OnShow()
        {
            SetSprite(showSprite);
        }

        public void OnHide()
        {
            SetSprite(hideSprite);
        }
    }
}