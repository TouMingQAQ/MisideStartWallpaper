using UnityEngine.UIElements;

namespace TFramework.ToolBox
{
    public static class UIToolkitExtension
    {
        public static VisualElement GetRealRoot(this VisualTreeAsset asset,string rootName)
        {
            var rootTree = asset.CloneTree();
            var rootStyle = rootTree.styleSheets;
            VisualElement root =rootTree.Q<VisualElement>(rootName);
            if (root == null)
                return null;
            root.styleSheets.Clear();
            for (int i = 0; i < rootStyle.count; i++)
            {
                root.styleSheets.Add(rootStyle[i]);
            }
            return root;
        }
        public static void CopySheet(this VisualElement element,VisualElement source)
        {
            element.styleSheets.Clear();
            for (int i = 0; i < source.styleSheets.count; i++)
            {
                element.styleSheets.Add(source.styleSheets[i]);
            }
        }
    }
}