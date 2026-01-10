#if  UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TFramework.ToolBox
{
    public class UGUIWindow : EditorWindow
    {
        public VisualTreeAsset TreeAsset;
        UGUIBox _uGUIBox;
        [MenuItem("TFramework/ToolBox/UGUI _F2")]
        public static void OpenConsoleWindow()
        {
            EditorWindow wnd = EditorWindow.GetWindow<UGUIWindow>();
            wnd.titleContent = new GUIContent("UGUI");
            wnd.minSize = new Vector2(300, 600);
            wnd.maxSize = new Vector2(300, 600);
            wnd.Show();
        }

        private void CreateGUI()
        {
            _uGUIBox = new UGUIBox(TreeAsset.CloneTree());
            rootVisualElement.Add(_uGUIBox);
        }

        private void OnGUI()
        {
            if(_uGUIBox == null)
                return;
            if (Event.current.isKey && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.LeftControl)
                _uGUIBox.SetControlLine();
            if(Event.current.isKey && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.LeftAlt)
                _uGUIBox.SetControlLineUp();
                
        }
    }
}
#endif
