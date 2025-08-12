using System;
using System.Collections.Generic;
using UnityEngine;

namespace TFramework.Component.UI
{
    public class TabGroup : MonoBehaviour
    {
        public bool openOnEnable = false;
        public int openIndexOnEnable = 0;
        public List<TabButton> tabButtons;
        public List<TabPage> tabPages;

        public event Action<int,TabButton> OnTabSelect;
        public event Action<int, TabPage> OnPageSelect;

        private void Awake()
        {
            for (int i = 0,count = tabButtons.Count; i < count; i++)
            {
                var tabButton = tabButtons[i];
                tabButton.tabIndex = i;
                tabButton.Init();
            }
        }

        private void OnEnable()
        {
            if (openOnEnable)
            {
                Select(openIndexOnEnable);
            }
        }

        public void Select(int index)
        {
            SelectTab(index);
            SelectPage(index);
        }
        /// <summary>
        /// 选择Page一般给TabButton用
        /// </summary>
        /// <param name="index"></param>
        public void SelectPage(int index)
        {
            if(index < 0 || index >= tabPages.Count)
                return;
            for (int i = 0,count = tabPages.Count; i < count; i++)
            {
                var page = tabPages[i];
                if(page == null)
                    continue;
                if (i == index)
                {
                    if(!page.IsShow())
                        page.Show();
                }
                else
                {
                    if(page.IsShow())
                        page.Hide();
                }
            }
            OnPageSelect?.Invoke(index,tabPages[index]);
        }

        /// <summary>
        /// 选择Tab，一般给TabButton用
        /// </summary>
        /// <param name="index"></param>
        public void SelectTab(int index)
        {
            if(index < 0 || index >= tabButtons.Count)
                return;
            for (int i = 0,count = tabButtons.Count; i < count; i++)
            {
                var button = tabButtons[i];
                if(button == null)
                    continue;
                if(i == index)
                    button.Show();
                else
                    button.Hide();
            }
            OnTabSelect?.Invoke(index,tabButtons[index]);
        }

        public void HideAll()
        {
            foreach (var tabButton in tabButtons)
            {
                tabButton.Hide();
            }

            foreach (var tabPage in tabPages)
            {
                tabPage.Hide();
            }
        }
    }
}
