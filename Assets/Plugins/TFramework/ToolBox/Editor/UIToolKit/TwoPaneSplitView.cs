using UnityEngine.UIElements;

namespace TFramework.ToolBox.UIToolKit
{
    [UxmlElement]
    public partial class TwoPaneSplitView : VisualElement
    {
        public VisualElement leftPanel;
        public VisualElement rightPanel;
        public TwoPaneSplitView()
        {
            var view = new UnityEngine.UIElements.TwoPaneSplitView();
            leftPanel = new VisualElement()
            {
                name = "LeftPanel",
            };
            rightPanel = new VisualElement()
            {
                name = "RightPanel",
            };
            view.Add(leftPanel);
            view.Add(rightPanel);
            Add(view);
            style.flexGrow = 1;
        }
    }
}