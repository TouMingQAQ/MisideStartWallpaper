using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TFramework.ToolBox
{
#if UNITY_EDITOR
    public class ConsoleWindow : EditorWindow
    {
        public VisualTreeAsset TreeAsset;
        [MenuItem("TFramework/ToolBox/Console _F1")]
        public static void OpenConsoleWindow()
        {
            EditorWindow wnd = EditorWindow.GetWindow<ConsoleWindow>();
            wnd.titleContent = new GUIContent("控制台");
        }

        private void CreateGUI()
        {
            rootVisualElement.Add(new ConsoleBox(TreeAsset.CloneTree()));
        }
    }
    
#endif
}