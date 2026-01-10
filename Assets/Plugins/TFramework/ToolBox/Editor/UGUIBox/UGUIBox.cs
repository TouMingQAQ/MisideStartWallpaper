using System;
using System.Collections.Generic;
using System.Text;
using Codice.CM.Common;
using TFramework.ToolBox.UIToolKit;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using TwoPaneSplitView = TFramework.ToolBox.UIToolKit.TwoPaneSplitView;

namespace TFramework.ToolBox
{
    public class UGUIBox : VisualElement
    {
        Button moveLeftButton;
        Button moveRightButton;
        Button moveUpButton;
        Button moveDownButton;
        Button moveLeftUpButton;
        Button moveRightUpButton;
        Button moveLeftDownButton;
        Button moveRightDownButton;
        Button moveStateButton;
        
        Slider speedSlider;
        Label rectInfoLabel;
        Button adaptButton;
        Button adaptAnchoredButton;
        public static float moveSpeed = 0.01f;
        private bool controlLine = false;
        private bool controlLineUp = false;
        public UGUIBox(VisualElement root)
        {
            this.style.flexBasis = 1;
            this.style.flexGrow = 1;
            root.style.flexBasis = 1;
            root.style.flexGrow = 1;
            Add(root);
            Selection.selectionChanged += OnSelectionChanged;
            moveLeftButton = this.Q<Button>("MoveLeft");
            moveRightButton = this.Q<Button>("MoveRight");
            moveUpButton = this.Q<Button>("MoveUp");
            moveDownButton = this.Q<Button>("MoveDown");
            moveLeftUpButton = this.Q<Button>("MoveLeftUp");
            moveRightUpButton = this.Q<Button>("MoveRightUp");
            moveLeftDownButton = this.Q<Button>("MoveLeftDown");
            moveRightDownButton = this.Q<Button>("MoveRightDown");
            moveStateButton = this.Q<Button>("MoveState");
            rectInfoLabel = this.Q<Label>("RectInfo");
            adaptButton = this.Q<Button>("Adapt");
            speedSlider = this.Q<Slider>();
            adaptAnchoredButton = this.Q<Button>("AdaptAnchored");
            
            moveLeftButton.clicked += () => MovePixel(Vector2.left * moveSpeed);
            moveRightButton.clicked += () => MovePixel(Vector2.right * moveSpeed);
            moveUpButton.clicked += () => MovePixel(Vector2.up * moveSpeed);
            moveDownButton.clicked += () => MovePixel(Vector2.down * moveSpeed);
            moveLeftUpButton.clicked += () => MovePixel(new Vector2(-1,1) * moveSpeed);
            moveLeftDownButton.clicked += () => MovePixel(new Vector2(-1,-1) * moveSpeed);
            moveRightUpButton.clicked += () => MovePixel(new Vector2(1,1) * moveSpeed);
            moveRightDownButton.clicked += () => MovePixel(new Vector2(1,-1) * moveSpeed);
            speedSlider.RegisterCallback<ChangeEvent<float>>(x_=>ChangeSpeed(x_.newValue));
            adaptButton.clicked += Adapt;
            adaptAnchoredButton.clicked += AdaptAnchored;
            moveSpeed = EditorPrefs.GetFloat("UGUIMoveSpeed", moveSpeed);
            speedSlider.value = moveSpeed;
        }

        public RectTransform selectRect;
        
        

        private void OnSelectionChanged()
        {
            selectRect = null;
            rectInfoLabel.text = "Have no rect.";
            var objs = Selection.gameObjects;
            foreach (var o in objs)
            {
                if (o.TryGetComponent(out RectTransform rectTransform))
                {
                    selectRect = rectTransform;
                    rectInfoLabel.text = GetRectTransformInfo(rectTransform);
                    return;
                }
            }
        }

        public void SetControlLineUp()
        {
            // controlLineUp = !controlLineUp;
            RefreshMoveState();
        }
        public void SetControlLine()
        {
            // controlLine = !controlLine;
            RefreshMoveState();
        }

        void RefreshMoveState()
        {
            SetControlLineColor(moveLeftButton);
            SetControlLineColor(moveRightButton);
            SetControlLineColor(moveUpButton);
            SetControlLineColor(moveDownButton);
            SetControlLineColor(moveLeftUpButton);
            SetControlLineColor(moveLeftDownButton);
            SetControlLineColor(moveRightUpButton);
            SetControlLineColor(moveRightDownButton);
            moveStateButton.text = "Move";
            if(controlLine)
                moveStateButton.text = "Up";
            if (controlLine && controlLineUp)
                moveStateButton.text = "Down";
            
        }
        void SetControlLineColor(VisualElement visualElement)
        {
            Color enabledColor = new Color(0.2f, 0.6f, 0.6f);
            Color upColor = new Color(0.9f, 0.2f, 0.4f);
            Color disabledColor = Color.gray;
            visualElement.style.backgroundColor = controlLine ? enabledColor : disabledColor;
            if(controlLine && controlLineUp)
                visualElement.style.backgroundColor = upColor;
        }
        void ChangeSpeed(float speed)
        {
            EditorPrefs.SetFloat("EditorPrefs",speed);
            moveSpeed = speed;
        }
        // void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.LeftArrow))
        //     {
        //         // 获取当前的左边距
        //         float currentInset = rectTransform.offsetMin.x;
        //
        //         // 设置新的左边距，向左移动
        //         rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, currentInset - moveDistance, rectTransform.rect.width);
        //     }
        // }
        public void MovePixel(Vector2 pixel)
        {
            if(selectRect == null)
                return;
            Undo.RecordObject(selectRect,"");
            if (controlLine)
            {
                float currentInset = 0;
                if (pixel.x < 0)
                {
                    currentInset = selectRect.offsetMin.x;
                    selectRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left,currentInset - pixel.x,selectRect.rect.width);

                }
                else if (pixel.x > 0)
                {
                    currentInset = selectRect.offsetMax.x;
                    selectRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right,currentInset + pixel.x,selectRect.rect.width);
                }
                if (pixel.y < 0)
                {
                    currentInset = selectRect.offsetMin.y;
                    selectRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom,currentInset - pixel.y,selectRect.rect.height);

                }
                else if (pixel.y > 0)
                {
                    currentInset = selectRect.offsetMax.y;
                    selectRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top,currentInset + pixel.y,selectRect.rect.height);
                }

            }
            else
            {
                selectRect.anchoredPosition += pixel;
            }
        }
        
        
        
        public string GetRectTransformInfo(RectTransform rectTransform)
        {
            if ((object)rectTransform == null)
            {
                return "Select rectTransform is null";
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name:{rectTransform.gameObject.name}");
            sb.AppendLine($"Position:{rectTransform.position}");
            sb.AppendLine($"LocalPosition:{rectTransform.localPosition}");
            sb.AppendLine($"Pivot:{rectTransform.pivot}");
            sb.AppendLine($"AnchoredPosition:{rectTransform.anchoredPosition}");
            sb.AppendLine($"AnchoredPosition3D:{rectTransform.anchoredPosition3D}");
            sb.AppendLine($"AnchorMax:{rectTransform.anchorMax}");
            sb.AppendLine($"AnchorMin:{rectTransform.anchorMin}");
            sb.AppendLine($"SizeDelta:{rectTransform.sizeDelta}");
            sb.AppendLine($"OffsetMax:{rectTransform.offsetMax}");
            sb.AppendLine($"OffsetMin:{rectTransform.offsetMin}");
            return sb.ToString();
        }
        
        private  void AdaptAnchored()
        {
            if(selectRect == null)
                return;
            Undo.RecordObject(selectRect,"");
            selectRect.anchoredPosition = Vector2.zero;
        }
        private void Adapt()
        {
            if(selectRect == null)
                return;
            Undo.RecordObject(selectRect,"");
            //位置信息
            Vector3 partentPos = selectRect.transform.parent.position;
            Vector3 localPos = selectRect.transform.position;
            //------获取rectTransform----
            RectTransform partentRect = selectRect.transform.parent.GetComponent<RectTransform>();
            // RectTransform go = go.GetComponent<RectTransform>();
            float partentWidth = partentRect.rect.width;
            float partentHeight = partentRect.rect.height;
            float localWidth = selectRect.rect.width * 0.5f;
            float localHeight = selectRect.rect.height * 0.5f;
            //---------位移差------
            float offX = localPos.x - partentPos.x;
            float offY = localPos.y - partentPos.y;
  
            float rateW = offX / partentWidth;
            float rateH = offY / partentHeight;
            selectRect.anchorMax = selectRect.anchorMin = new Vector2(0.5f + rateW, 0.5f + rateH);
            selectRect.anchoredPosition = Vector2.zero;
  
            partentHeight = partentHeight * 0.5f;
            partentWidth = partentWidth * 0.5f;
            float rateX = (localWidth / partentWidth) * 0.5f;
            float rateY = (localHeight / partentHeight) * 0.5f;
            selectRect.anchorMax = new Vector2(selectRect.anchorMax.x + rateX, selectRect.anchorMax.y + rateY);
            selectRect.anchorMin = new Vector2(selectRect.anchorMin.x - rateX, selectRect.anchorMin.y - rateY);
            selectRect.offsetMax = selectRect.offsetMin = Vector2.zero;
        }

    }
}