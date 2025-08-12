using UnityEngine;

namespace TFramework.Component.UI
{
    public class TabPage : MonoBehaviour
    {
        public TabGroup group;

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual bool IsShow()
        {
            return gameObject.activeSelf;
        }
    }
}
