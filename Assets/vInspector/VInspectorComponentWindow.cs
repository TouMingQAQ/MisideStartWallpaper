#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using System.Reflection;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using Type = System.Type;
using static VInspector.Libs.VUtils;
using static VInspector.Libs.VGUI;



namespace VInspector
{
    public class VInspectorComponentWindow : EditorWindow
    {

        void OnGUI()
        {
            if (!component || !editor) { Close(); return; } // todo script components break on playmode


            void background()
            {
                position.SetPos(0, 0).Draw(GUIColors.windowBackground);
            }
            void outline()
            {
                if (Application.platform == RuntimePlatform.OSXEditor) return;

                position.SetPos(0, 0).DrawOutline(Greyscale(.1f));

            }
            void header()
            {
                var headerRect = ExpandWidthLabelRect(18).Resize(-1).AddWidthFromMid(6);
                var closeButtonRect = headerRect.SetWidthFromRight(16).SetHeightFromMid(16).Move(-4, 0);

                var backgroundColor = isDarkTheme ? Greyscale(.25f) : GUIColors.windowBackground;

                void startDragging()
                {
                    if (isResizing) return;
                    if (isDragged) return;
                    if (!curEvent.isMouseDrag) return;
                    if (!headerRect.IsHovered()) return;


                    draggedInstance = this;

                    dragStartMousePos = EditorGUIUtility.GUIToScreenPoint(curEvent.mousePosition);
                    dragStartWindowPos = position.position;

                }
                void updateDragging()
                {
                    if (!isDragged) return;


                    var draggedPosition = dragStartWindowPos + EditorGUIUtility.GUIToScreenPoint(curEvent.mousePosition) - dragStartMousePos;

                    if (!curEvent.isRepaint)
                        position = position.SetPos(draggedPosition);


                    EditorGUIUtility.hotControl = EditorGUIUtility.GetControlID(FocusType.Passive);

                }
                void stopDragging()
                {
                    if (!isDragged) return;
                    if (!curEvent.isMouseMove && !curEvent.isMouseUp) return;


                    draggedInstance = null;

                    EditorGUIUtility.hotControl = 0;

                }

                void background()
                {
                    headerRect.Draw(backgroundColor);

                    headerRect.SetHeightFromBottom(1).Draw(isDarkTheme ? Greyscale(.2f) : Greyscale(.7f));

                }
                void icon()
                {
                    var iconRect = headerRect.SetWidth(20).MoveX(14).MoveY(-1);

                    if (!componentIcons_byType.ContainsKey(component.GetType()))
                        componentIcons_byType[component.GetType()] = EditorGUIUtility.ObjectContent(component, component.GetType()).image;

                    GUI.Label(iconRect, componentIcons_byType[component.GetType()]);

                }
                void toggle()
                {
                    var toggleRect = headerRect.MoveX(36).SetSize(20, 20);


                    var pi_enabled = component.GetType().GetProperty("enabled") ??
                                     component.GetType().BaseType?.GetProperty("enabled") ??
                                     component.GetType().BaseType?.BaseType?.GetProperty("enabled") ??
                                     component.GetType().BaseType?.BaseType?.BaseType?.GetProperty("enabled");


                    if (pi_enabled == null) return;

                    var enabled = (bool)pi_enabled.GetValue(component);


                    if (GUI.Toggle(toggleRect, enabled, "") == enabled) return;

                    component.RecordUndo();
                    pi_enabled.SetValue(component, !enabled);

                }
                void name()
                {
                    var nameRect = headerRect.MoveX(54).MoveY(-1);


                    var s = new GUIContent(EditorGUIUtility.ObjectContent(component, component.GetType())).text;
                    s = s.Substring(s.LastIndexOf('(') + 1);
                    s = s.Substring(0, s.Length - 1);

                    if (instances.Any(r => r.component.GetType() == component.GetType() && r.component != component))
                        s += " - " + component.gameObject.name;


                    SetLabelBold();

                    GUI.Label(nameRect, s);

                    ResetLabelStyle();

                }
                void nameCurtain()
                {
                    var flatColorRect = headerRect.SetX(closeButtonRect.x + 3).SetXMax(headerRect.xMax);
                    var gradientRect = headerRect.SetXMax(flatColorRect.x).SetWidthFromRight(30);

                    flatColorRect.Draw(backgroundColor);
                    gradientRect.DrawCurtainLeft(backgroundColor);

                }
                void closeButton()
                {
                    var iconName = "CrossIcon";
                    var iconSize = 14;
                    var color = isDarkTheme ? Greyscale(.65f) : Greyscale(.35f);
                    var colorHovered = isDarkTheme ? Greyscale(.9f) : color;
                    var colorPressed = color;


                    if (!IconButton(closeButtonRect, iconName, iconSize, color, colorHovered, colorPressed)) return;

                    Close();

                    EditorGUIUtility.ExitGUI();

                }
                void escHint()
                {
                    if (!closeButtonRect.IsHovered()) return;
                    if (EditorWindow.focusedWindow != this) return;

                    var textRect = headerRect.SetWidthFromRight(42).MoveY(-.5f);
                    var fontSize = 11;
                    var color = Greyscale(.65f);


                    SetLabelFontSize(fontSize);
                    SetGUIColor(color);

                    GUI.Label(textRect, "Esc");

                    ResetGUIColor();
                    ResetLabelStyle();

                }

                startDragging();
                updateDragging();
                stopDragging();

                background();
                icon();
                toggle();
                name();
                nameCurtain();
                closeButton();
                escHint();

            }
            void body()
            {
                EditorGUIUtility.labelWidth = (this.position.width * .4f).Max(120);

                BeginIndent(17);

                editor?.OnInspectorGUI();

                EndIndent(1);

                EditorGUIUtility.labelWidth = 0;

            }

            void updateHeight()
            {
                var r = ExpandWidthLabelRect();

                if (curEvent.isRepaint)
                    position = position.SetHeight(lastRect.y);

            }
            void closeOnEscape()
            {
                if (!curEvent.isKeyDown) return;
                if (curEvent.keyCode != KeyCode.Escape) return;

                Close();

                EditorGUIUtility.ExitGUI();

            }

            void horizontalResize()
            {
                var resizeArea = this.position.SetPos(0, 0).SetWidthFromRight(5).AddHeightFromBottom(-20);

                void startResize()
                {
                    if (isDragged) return;
                    if (isResizing) return;
                    if (!curEvent.isMouseDown && !curEvent.isMouseDrag) return;
                    if (!resizeArea.IsHovered()) return;

                    isResizing = true;

                    resizeStartMousePos = EditorGUIUtility.GUIToScreenPoint(curEvent.mousePosition);
                    resizeStartWindowSize = this.position.size;

                }
                void updateResize()
                {
                    if (!isResizing) return;


                    var resizedWidth = resizeStartWindowSize.x + EditorGUIUtility.GUIToScreenPoint(curEvent.mousePosition).x - resizeStartMousePos.x;

                    var width = resizedWidth.Max(minWidth);

                    if (!curEvent.isRepaint)
                        position = position.SetWidth(width);


                    EditorGUIUtility.hotControl = EditorGUIUtility.GetControlID(FocusType.Passive);
                    // GUI.focused

                }
                void stopResize()
                {
                    if (!isResizing) return;
                    if (!curEvent.isMouseUp) return;

                    isResizing = false;

                    EditorGUIUtility.hotControl = 0;

                }


                EditorGUIUtility.AddCursorRect(resizeArea, MouseCursor.ResizeHorizontal);

                startResize();
                updateResize();
                stopResize();

            }



            background();
            header();
            outline();

            Space(3);
            body();

            Space(7);


            updateHeight();
            closeOnEscape();

            horizontalResize();

            if (isDragged)
                Repaint();

        }

        public bool isDragged => draggedInstance == this;
        public Vector2 dragStartMousePos;
        public Vector2 dragStartWindowPos;

        public bool isResizing;
        public Vector2 resizeStartMousePos;
        public Vector2 resizeStartWindowSize;

        static Dictionary<System.Type, Texture> componentIcons_byType = new();














        public void Init(Component component)
        {
            if (editor)
                editor.DestroyImmediate();

            this.component = component;
            this.editor = Editor.CreateEditor(component);

            if (!instances.Contains(this))
                instances.Add(this);

        }

        void OnDestroy()
        {
            editor?.DestroyImmediate();

            if (instances.Contains(this))
                instances.Remove(this);

        }

        public Component component;
        public Editor editor;

        public static List<VInspectorComponentWindow> instances = new();





        public static void CreateDraggedInstance(Component component, Vector2 windowPosition, float windowWidth)
        {
            draggedInstance = ScriptableObject.CreateInstance<VInspectorComponentWindow>();

            draggedInstance.ShowPopup();
            draggedInstance.Init(component);
            draggedInstance.Focus();

            draggedInstance.wantsMouseMove = true;


            draggedInstance.position = Rect.zero.SetPos(windowPosition).SetWidth(windowWidth).SetHeight(200);

            draggedInstance.dragStartMousePos = curEvent.mousePosition_screenSpace;
            draggedInstance.dragStartWindowPos = windowPosition;

        }

        public static VInspectorComponentWindow draggedInstance;

        public static float minWidth => 300;

    }
}
#endif