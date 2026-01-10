using System;
using System.Collections.Generic;
using Unity.Properties;

#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif
using UnityEngine;
using UnityEngine.UIElements;

namespace TFramework.ToolBox.UIToolKit
{
    [UxmlElement("日志")]
    public partial class LogView : VisualElement
    {
        private bool _debugLog = true;
        [UxmlAttribute]
        public bool DebugLog { get => _debugLog; set=>_debugLog = value;}
    
        private bool _debugWarningLog = false;

        [UxmlAttribute]
        public bool DebugWarningLog { get => _debugWarningLog; set=>_debugWarningLog = value;}
        
        private bool _debugErrorLog = false;
        [UxmlAttribute]
        public bool DebugErrorLog { get => _debugErrorLog; set=>_debugErrorLog = value;}
        
        private bool _registerLog = true;

        [UxmlAttribute]
        public bool RegisterLog { get => _registerLog; set=>_registerLog = value;}

        private Color _logColor = Color.white;
        [UxmlAttribute]
        public Color LogColor { get => _logColor; set=>_logColor = value;}
        
        private Color _warningColor = Color.yellow;
        [UxmlAttribute]
        public Color WarningColor { get => _warningColor; set=>_warningColor = value;}
        
        private Color _errorColor = Color.red;
        [UxmlAttribute]
        public Color ErrorColor { get => _errorColor; set=>_errorColor = value;}
        
        private float _logFontSize = 15;
        [UxmlAttribute]
        public float LogFontSize { get => _logFontSize;
            set
            {
                _logFontSize = value;
                foreach (var item in _scrollView.Children())
                {
                    item.Q<Label>("MessName").style.fontSize = value;
                    item.Q<Label>("MessValue").style.fontSize = value;
                }
            }}

        private ScrollView _scrollView;

        private List<LogValue> _logList = new();
        public LogView()
        {
            _scrollView = new ScrollView
            {
                style =
                {
                    flexGrow = 1,
                    paddingLeft = 10,
                    paddingTop = 5,
                    paddingRight = 10,
                    paddingBottom = 5,
                }
            };
            style.flexGrow = 1;
            style.backgroundColor = new StyleColor(new Color(0, 0, 0, 0.8f));
            var element = _scrollView.Q<VisualElement>("unity-content-viewport");
            element.style.flexDirection = FlexDirection.ColumnReverse;
#if UNITY_EDITOR
            var toolbar = new Toolbar();
            var clearButton = new ToolbarButton(ClearLog)
            {
                text = "Clear"
            };
            var topButton = new ToolbarButton(Top)
            {
                text = "Top"
            };
            var bottomButton = new ToolbarButton(Bottom)
            {
                text = "Bottom"
            };
            var slider = new Slider(10,20,SliderDirection.Horizontal)
            {
                value = LogFontSize,
                label = "Font Size"
            };
            slider.Q<VisualElement>(null, "unity-base-field__input").style.minWidth = 80;
            slider.RegisterValueChangedCallback((evt =>
            {
                SetFontSize(evt.newValue);
            }));
            toolbar.Add(clearButton);
            toolbar.Add(topButton);
            toolbar.Add(bottomButton);
            toolbar.Add(slider);
            Add(toolbar);
#endif
            Add(_scrollView);
            Application.logMessageReceived += OnLog;
        }

        public void Top()
        {
            _scrollView.scrollOffset = new Vector2(0,0);
        }

        public void Bottom()
        {
            _scrollView.scrollOffset = new Vector2(0, _scrollView.contentContainer.layout.height);
        }
        public void ClearLog()
        {
            _scrollView.Clear();
        }
        public void SetFontSize(float size)
        {
            LogFontSize = size;
        }
        private void OnLog(string condition, string stacktrace, LogType type)
        {
            if(!RegisterLog)
                return;
            if(!IsLog(type))
                return;
            string name = GetMessWithColor(type.ToString()+">", type);
            string mess = GetMessWithColor(condition, type);
            AddItem(new LogValue()
            {
                Name = name,
                Message = mess
            });
            bool IsLog(LogType type)
            {
                return type switch
                {
                    LogType.Error => DebugErrorLog,
                    LogType.Assert => DebugErrorLog,
                    LogType.Warning => DebugWarningLog,
                    LogType.Log => DebugLog,
                    LogType.Exception => DebugErrorLog,
                    _ => false
                };
            }
            
            Color GetLogColor(LogType type)
            {
                var color = type switch
                {
                    LogType.Error => ErrorColor,
                    LogType.Assert => ErrorColor,
                    LogType.Warning => WarningColor,
                    LogType.Log => LogColor,
                    LogType.Exception => ErrorColor,
                    _ => LogColor
                };
                return color;
            }
            string GetMessWithColor(string mess,LogType type)
            {
                return $"<color=#{ColorUtility.ToHtmlStringRGB(GetLogColor(type))}>{mess}</color>";
            }
        }

        public void AddItem(LogValue value)
        {
            if(_scrollView == null)
                return;
            _logList.Add(value);
            var itemRoot = new VisualElement
            {
                name = "MessItem",
                style =
                {
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    alignItems = new StyleEnum<Align>(Align.FlexStart),
                    justifyContent = new StyleEnum<Justify>(Justify.FlexStart),
                    marginTop = 5,
                    marginBottom = 5,
                    fontSize = 15
                }
            };
            var itemName = new Label(value.Name)
            {
                name = "MessName",
                style =
                {
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    alignItems = new StyleEnum<Align>(Align.FlexStart),
                    justifyContent = new StyleEnum<Justify>(Justify.FlexStart),
                    fontSize = 15
                },
                selection =
                {
                    isSelectable = true
                }
            };
            var itemValue = new Label(value.Message)
            {
                name = "MessValue",
                style =
                {
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    alignItems = new StyleEnum<Align>(Align.FlexStart),
                    justifyContent = new StyleEnum<Justify>(Justify.FlexStart),
                    fontSize = 15
                },
                selection =
                {
                    isSelectable = true
                }
            };
            itemName.style.whiteSpace = new StyleEnum<WhiteSpace>(WhiteSpace.PreWrap);
            itemValue.style.whiteSpace = new StyleEnum<WhiteSpace>(WhiteSpace.PreWrap);
            itemRoot.Add(itemName);
            itemRoot.Add(itemValue);
            _scrollView.Add(itemRoot);
            // 滚动到底部
            // 延迟一帧执行
            schedule.Execute(() =>
            {
                _scrollView.scrollOffset = new Vector2(0, _scrollView.contentContainer.layout.height);
            }).ExecuteLater(1);
        }
        [Serializable]
        public struct LogValue
        {
            public string Name;
            public string Message;
        }
    }

}
