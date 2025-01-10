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
using System.Diagnostics;
using Type = System.Type;
using Delegate = System.Delegate;
using Action = System.Action;
using static VTabs.Libs.VUtils;
using static VTabs.Libs.VGUI;





namespace VTabs
{
    public static class VTabs
    {
        static void Update()
        {
            var lastEvent = typeof(Event).GetFieldValue<Event>("s_Current");


            void scrollInteractions()
            {
                if (isKeyPressed) { sidesscrollPosition = 0; return; }
                if (lastEvent.delta == Vector2.zero) return;
                if (lastEvent.type == EventType.MouseMove) { sidesscrollPosition = 0; return; }
                if (lastEvent.type == EventType.MouseDrag) { sidesscrollPosition = 0; return; }
                if (lastEvent.type != EventType.ScrollWheel && delayedMousePosition_screenSpace != EditorGUIUtility.GUIToScreenPoint(lastEvent.mousePosition)) return; // uncaptured mouse move/drag check
                if (lastEvent.type != EventType.ScrollWheel && Application.platform == RuntimePlatform.OSXEditor && lastEvent.delta.x == (int)lastEvent.delta.x) return; // osx uncaptured mouse move/drag in sceneview ang gameview workaround


                void switchTab(int dir)
                {
                    if (!VTabsMenu.switchTabsEnabled) return;
                    if (!(EditorWindow.mouseOverWindow is EditorWindow hoveredWindow)) return;
                    if (!hoveredWindow.docked) return;
                    if (hoveredWindow.maximized) return;


                    var tabs = GetTabList(hoveredWindow);

                    var i0 = tabs.IndexOf(hoveredWindow);
                    var i1 = Mathf.Clamp(i0 + dir, 0, tabs.Count - 1);

                    tabs[i1].Focus();

                    UpdateTitle(tabs[i1]);

                }
                void moveTab(int dir)
                {
                    if (!VTabsMenu.moveTabsEnabled) return;
                    if (!(EditorWindow.mouseOverWindow is EditorWindow hoveredWindow)) return;


                    var tabs = GetTabList(hoveredWindow);

                    var i0 = tabs.IndexOf(hoveredWindow);
                    var i1 = Mathf.Clamp(i0 + dir, 0, tabs.Count - 1);

                    var r = tabs[i0];
                    tabs[i0] = tabs[i1];
                    tabs[i1] = r;
                    tabs[i1].Focus();

                }

                void shiftscroll()
                {
                    if (!lastEvent.shift) return;


                    var scrollDelta = Application.platform == RuntimePlatform.OSXEditor ? lastEvent.delta.x // osx sends delta.y as delta.x when shift is pressed
                                                                                        : lastEvent.delta.x - lastEvent.delta.y; // some software on windows (eg logitech options) may do that too
                    if (VTabsMenu.reverseScrollDirectionEnabled)
                        scrollDelta *= -1;


                    if (scrollDelta != 0)
                        if (lastEvent.control || lastEvent.command)
                            moveTab(scrollDelta > 0 ? 1 : -1);
                        else
                            switchTab(scrollDelta > 0 ? 1 : -1);

                }
                void sidescroll()
                {
                    if (lastEvent.shift) return;
                    if (lastEvent.delta.x == 0) return;
                    if (lastEvent.delta.x.Abs() <= 0.06f) return;
                    if (lastEvent.delta.x.Abs() * 1.1f < lastEvent.delta.y.Abs()) { sidesscrollPosition = 0; return; }
                    if (!VTabsMenu.sidescrollEnabled) return;


                    var dampenK = 5; // the larger this k is - the smaller big deltas are, and the less is sidescroll's dependency on scroll speed
                    var a = lastEvent.delta.x.Abs() * dampenK;
                    var deltaDampened = (a < 1 ? a : Mathf.Log(a) + 1) / dampenK * lastEvent.delta.x.Sign();

                    var sensitivityK = .22f;
                    var sidescrollDelta = deltaDampened * VTabsMenu.sidescrollSensitivity * sensitivityK;

                    if (VTabsMenu.reverseScrollDirectionEnabled)
                        sidescrollDelta *= -1;

                    if (sidesscrollPosition.RoundToInt() != (sidesscrollPosition += sidescrollDelta).RoundToInt())
                        if (lastEvent.control || lastEvent.command)
                            moveTab(sidescrollDelta < 0 ? 1 : -1);
                        else
                            switchTab(sidescrollDelta < 0 ? 1 : -1);

                }


                shiftscroll();
                sidescroll();

            }
            void scrollAnimation()
            {
                if (!EditorWindow.focusedWindow) return;
                if (!EditorWindow.focusedWindow.docked) return;
                if (EditorWindow.focusedWindow.maximized) return;


                var dockArea = EditorWindow.focusedWindow.GetMemberValue("m_Parent");

                if (dockArea.GetType() != t_DockArea) return; // happens on 2021.1.28


                var curScroll = dockArea.GetFieldValue<float>("m_ScrollOffset");

                if (!curScroll.Approx(0))
                    curScroll -= nonZeroTabScrollOffset;

                if (curScroll == 0 && prevFocusedDockArea == dockArea)
                    curScroll = prevScroll;




                var targScroll = GetOptimalTabScrollerPosition(EditorWindow.focusedWindow);

                var animationSpeed = 7f;

                var deltaTime = (EditorApplication.timeSinceStartup - prevTime).ToFloat().Min(.03f);

                var newScroll = SmoothDamp(curScroll, targScroll, animationSpeed, ref scrollDeriv, deltaTime);

                if (newScroll < .5f)
                    newScroll = 0;




                prevScroll = newScroll;
                prevFocusedDockArea = dockArea;
                prevTime = EditorApplication.timeSinceStartup;




                if (newScroll.Approx(curScroll)) return;

                if (!newScroll.Approx(0))
                    newScroll += nonZeroTabScrollOffset;

                dockArea.SetFieldValue("m_ScrollOffset", newScroll);

                EditorWindow.focusedWindow.Repaint();

            }
            void createWindowDelayed()
            {
                if (createWindowDelayedAction == null) return;
                if ((System.DateTime.UtcNow - lastDragndropTime).TotalSeconds < .05f) return;

                createWindowDelayedAction.Invoke();
                createWindowDelayedAction = null;

            }
            void dragndrop()
            {
                var uncaughtDragPerform = lastKnownDraggedObject != null && !DragAndDrop.objectReferences.Any() && curEvent.isMouseEnterWindow && Application.platform == RuntimePlatform.LinuxEditor;

                if (!VTabsMenu.dragndropEnabled) return;
                if (lastEvent.type != EventType.DragUpdated && lastEvent.type != EventType.DragPerform && !uncaughtDragPerform) return;
                if (!(EditorWindow.mouseOverWindow is EditorWindow hoveredWindow)) return;
                if (!hoveredWindow.position.SetPos(0, 0).SetHeight(hoveredWindow.GetType() == t_SceneHierarchyWindow ? 5 : 40).Contains(lastEvent.mousePosition)) return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (DragAndDrop.objectReferences.Any()) // workaround for null objectReferences and mouseOverWindow on linux
                {
                    lastKnownDraggedObject = DragAndDrop.objectReferences.First();
                    lastKnownHoveredWindow = EditorWindow.mouseOverWindow;
                }


                if (lastEvent.type != EventType.DragPerform && !uncaughtDragPerform) return;
                if (!lastKnownDraggedObject) return;
                if (!lastKnownHoveredWindow) return;
                if (lastDragndropPosition == curEvent.mousePosition) return;

                DragAndDrop.AcceptDrag();

                var lockToObject = lastKnownDraggedObject;
                var dockArea = lastKnownHoveredWindow.GetMemberValue("m_Parent");

                createWindowDelayedAction = () => new TabInfo(lockToObject).CreateWindow(dockArea, false); // not creating window right away to avoid scroll animation stutter

                lastDragndropPosition = curEvent.mousePosition;
                lastDragndropTime = System.DateTime.UtcNow;

                lastKnownDraggedObject = null;
                lastKnownHoveredWindow = null;

            }


            scrollInteractions();
            scrollAnimation();
            createWindowDelayed();
            dragndrop();

            CheckIfFocusedWindowChanged();
            CheckIfWindowWasUnmaximized();

        }

        static float sidesscrollPosition;

        static float scrollDeriv;
        static float prevScroll;
        static object prevFocusedDockArea;

        static double deltaTime;
        static double prevTime = 0;

        static Vector2 lastDragndropPosition;
        static System.DateTime lastDragndropTime;
        static Action createWindowDelayedAction;

        static Object lastKnownDraggedObject;
        static EditorWindow lastKnownHoveredWindow;







        static void CheckShortcuts() // globalEventHandler
        {
            void set_isKeyPressed()
            {
                if (curEvent.keyCode == KeyCode.LeftShift) return;
                if (curEvent.keyCode == KeyCode.LeftControl) return;
                if (curEvent.keyCode == KeyCode.LeftCommand) return;
                if (curEvent.keyCode == KeyCode.RightShift) return;
                if (curEvent.keyCode == KeyCode.RightControl) return;
                if (curEvent.keyCode == KeyCode.RightCommand) return;

                if (Event.current.type == EventType.KeyDown)
                    isKeyPressed = true;

                if (Event.current.type == EventType.KeyUp)
                    isKeyPressed = false;

            }

            void addTab()
            {
                if (!curEvent.isKeyDown) return;
                if (!curEvent.holdingCmdOnly && !curEvent.holdingCtrlOnly) return;
                if (curEvent.keyCode != KeyCode.T) return;

                if (!EditorWindow.mouseOverWindow) return;
                if (!VTabsMenu.addTabEnabled) return;


                curEvent.Use();

                addTabMenu_openedOverWindow = EditorWindow.mouseOverWindow;



                List<TabInfo> defaultTabList;
                List<TabInfo> customTabList;

                var customTabListKey = "vTabs-CustomTabList-" + GetProjectId();

                void loadDefaultTabList()
                {
                    defaultTabList = new List<TabInfo>();

                    defaultTabList.Add(new TabInfo("SceneView", "Scene"));
                    defaultTabList.Add(new TabInfo("GameView", "Game"));
                    defaultTabList.Add(new TabInfo("ProjectBrowser", "Project"));
                    defaultTabList.Add(new TabInfo("InspectorWindow", "Inspector"));
                    defaultTabList.Add(new TabInfo("ConsoleWindow", "Console"));
                    defaultTabList.Add(new TabInfo("ProfilerWindow", "Profiler"));
                    // defaultTabList.Add(new TabInfo("LightingWindow", "Lighting"));
                    // defaultTabList.Add(new TabInfo("ProjectSettingsWindow", "Project Settings"));

                }
                void loadSavedTabList()
                {
                    var json = EditorPrefs.GetString(customTabListKey);

                    customTabList = JsonUtility.FromJson<TabInfoList>(json)?.list ?? new List<TabInfo>();

                }
                void saveSavedTabsList()
                {
                    var json = JsonUtility.ToJson(new TabInfoList { list = customTabList });

                    EditorPrefs.SetString(customTabListKey, json);

                }

                loadDefaultTabList();
                loadSavedTabList();




                GenericMenu menu = new GenericMenu();
                Vector2 menuPosition;

                void rememberClickPosition()
                {
                    addTabMenu_lastClickPosition_screenSpace = curEvent.mousePosition_screenSpace;
                }
                void set_menuPosition()
                {
                    if (curEvent.mousePosition_screenSpace.DistanceTo(addTabMenu_lastClickPosition_screenSpace) < 2)
                        menuPosition = EditorGUIUtility.ScreenToGUIPoint(addTabMenu_lastOpenPosition_screenSpace);
                    else
                        menuPosition = curEvent.mousePosition - Vector2.up * 9;

                    addTabMenu_lastOpenPosition_screenSpace = EditorGUIUtility.GUIToScreenPoint(menuPosition);

#if !UNITY_2021_2_OR_NEWER
                    if (EditorWindow.focusedWindow)
                        menuPosition += EditorWindow.focusedWindow.position.position;
#endif
                }

                void defaultTabs()
                {
                    menu.AddDisabledItem(new GUIContent("Default tabs"));

                    foreach (var tabInfo in defaultTabList)
                        menu.AddItem(new GUIContent(tabInfo.menuItemName), false, () =>
                        {
                            tabInfo.CreateWindow(GetDockArea(addTabMenu_openedOverWindow), false);
                            EditorApplication.delayCall += rememberClickPosition;

                        });

                }
                void savedTabs()
                {
                    if (!customTabList.Any()) return;

                    menu.AddSeparator("");

                    menu.AddDisabledItem(new GUIContent("Saved tabs"));


                    foreach (var tabInfo in customTabList)
                        if (tabInfo.isPropertyEditor && !tabInfo.globalId.GetObject())
                            menu.AddDisabledItem(new GUIContent(tabInfo.menuItemName));
                        else
                            menu.AddItem(new GUIContent(tabInfo.menuItemName), false, () =>
                            {
                                tabInfo.CreateWindow(GetDockArea(addTabMenu_openedOverWindow), false);
                                EditorApplication.delayCall += rememberClickPosition;

                            });

                }
                void remove()
                {
                    if (!customTabList.Any()) return;


                    foreach (var tabInfo in customTabList)
                        menu.AddItem(new GUIContent("Remove/" + tabInfo.menuItemName), false, () =>
                        {
                            customTabList.Remove(tabInfo);
                            saveSavedTabsList();
                            EditorApplication.delayCall += rememberClickPosition;

                        });


                    menu.AddSeparator("Remove/");

                    menu.AddItem(new GUIContent("Remove/Remove all"), false, () =>
                    {
                        customTabList.Clear();
                        saveSavedTabsList();
                        EditorApplication.delayCall += rememberClickPosition;

                    });

                }
                void saveCurrentTab()
                {
                    var menuItemName = addTabMenu_openedOverWindow.titleContent.text.Replace("/", " \u2215 ").Trim(' ');

                    if (defaultTabList.Any(r => r.menuItemName == menuItemName)) return;
                    if (customTabList.Any(r => r.menuItemName == menuItemName)) return;

                    menu.AddSeparator("");

                    menu.AddItem(new GUIContent("Save current tab"), false, () =>
                    {
                        customTabList.Add(new TabInfo(addTabMenu_openedOverWindow));
                        saveSavedTabsList();
                        EditorApplication.delayCall += rememberClickPosition;

                    });

                }


                set_menuPosition();

                defaultTabs();
                savedTabs();
                remove();
                saveCurrentTab();

                menu.DropDown(Rect.zero.SetPos(menuPosition));

            }
            void closeTab()
            {
                if (!curEvent.isKeyDown) return;
                if (!curEvent.holdingCmdOnly && !curEvent.holdingCtrlOnly) return;
                if (curEvent.keyCode != KeyCode.W) return;

                if (!VTabsMenu.closeTabEnabled) return;
                if (!EditorWindow.mouseOverWindow) return;
                if (!EditorWindow.mouseOverWindow.docked) return;
                if (GetTabList(EditorWindow.mouseOverWindow).Count <= 1) return;


                Event.current.Use();

                tabInfosForReopening.Push(new TabInfo(EditorWindow.mouseOverWindow));

                EditorWindow.mouseOverWindow.Close();

            }
            void reopenTab()
            {
                if (!curEvent.isKeyDown) return;

                if (curEvent.modifiers != (EventModifiers.Command | EventModifiers.Shift)
                 && curEvent.modifiers != (EventModifiers.Control | EventModifiers.Shift)) return;

                if (curEvent.keyCode != KeyCode.T) return;

                if (!EditorWindow.mouseOverWindow) return;
                if (!VTabsMenu.reopenTabEnabled) return;
                if (!tabInfosForReopening.Any()) return;


                Event.current.Use();

                var window = tabInfosForReopening.Pop().CreateWindow();

                UpdateTitle(window);

            }


            set_isKeyPressed();

            addTab();
            closeTab();
            reopenTab();

        }

        static Vector2 addTabMenu_lastClickPosition_screenSpace;
        static Vector2 addTabMenu_lastOpenPosition_screenSpace;
        static EditorWindow addTabMenu_openedOverWindow;

        static bool isKeyPressed;

        static Stack<TabInfo> tabInfosForReopening = new Stack<TabInfo>();






        [System.Serializable]
        class TabInfo
        {
            public EditorWindow CreateWindow(object dockArea = null, bool atOriginalTabIndex = true)
            {
                if (dockArea == null) dockArea = originalDockArea;
                if (dockArea == null) return null;
                if (dockArea.GetType() != t_DockArea) return null; // happens in 2023.2, no idea why

                var lastInteractedBrowser = t_ProjectBrowser.GetFieldValue("s_LastInteractedProjectBrowser"); // changes on new browser creation // tomove to seutup

                var window = (EditorWindow)ScriptableObject.CreateInstance(typeName);

                void notifyVFavorites()
                {
                    mi_VFavorites_BeforeWindowCreated?.Invoke(null, new object[] { dockArea });
                }
                void addToDockArea()
                {
                    if (atOriginalTabIndex)
                        dockArea.InvokeMethod("AddTab", originalTabIndex, window, true);
                    else
                        dockArea.InvokeMethod("AddTab", window, true);

                }

                void setupBrowser()
                {
                    if (!isBrowser) return;


                    void setSavedGridSize()
                    {
                        if (!isGridSizeSaved) return;

                        window.GetFieldValue("m_ListArea")?.SetMemberValue("gridSize", savedGridSize);

                    }
                    void setLastUsedGridSize()
                    {
                        if (isGridSizeSaved) return;
                        if (lastInteractedBrowser == null) return;

                        var listAreaSource = lastInteractedBrowser.GetFieldValue("m_ListArea");
                        var listAreaDest = window.GetFieldValue("m_ListArea");

                        if (listAreaSource != null && listAreaDest != null)
                            listAreaDest.SetPropertyValue("gridSize", listAreaSource.GetPropertyValue("gridSize"));

                    }

                    void setSavedLayout()
                    {
                        if (!isLayoutSaved) return;

                        var layoutEnum = System.Enum.ToObject(t_ProjectBrowser.GetField("m_ViewMode", maxBindingFlags).FieldType, savedLayout);

                        window.InvokeMethod("SetViewMode", layoutEnum);

                    }
                    void setLastUsedLayout()
                    {
                        if (isLayoutSaved) return;
                        if (lastInteractedBrowser == null) return;

                        window.InvokeMethod("SetViewMode", lastInteractedBrowser.GetMemberValue("m_ViewMode"));

                    }

                    void setLastUsedListWidth()
                    {
                        if (lastInteractedBrowser == null) return;

                        window.SetFieldValue("m_DirectoriesAreaWidth", lastInteractedBrowser.GetFieldValue("m_DirectoriesAreaWidth"));

                    }

                    void lockToFolder_twoColumns()
                    {
                        if (!isLocked) return;
                        if (window.GetMemberValue<int>("m_ViewMode") != 1) return;
                        if (folderGuid.IsNullOrEmpty()) return;


                        var iid = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(folderGuid)).GetInstanceID();

                        window.GetFieldValue("m_ListAreaState").SetFieldValue("m_SelectedInstanceIDs", new List<int> { iid });

                        t_ProjectBrowser.InvokeMethod("OpenSelectedFolders");


                        window.SetPropertyValue("isLocked", true);

                    }
                    void lockToFolder_oneColumn()
                    {
                        if (!isLocked) return;
                        if (window.GetMemberValue<int>("m_ViewMode") != 0) return;
                        if (folderGuid.IsNullOrEmpty()) return;

                        if (!(window.GetMemberValue("m_AssetTree") is object m_AssetTree)) return;
                        if (!(m_AssetTree.GetMemberValue("data") is object data)) return;


                        var folderPath = folderGuid.ToPath();
                        var folderIid = AssetDatabase.LoadAssetAtPath<Object>(folderPath).GetInstanceID();

                        data.SetMemberValue("m_rootInstanceID", folderIid);

                        m_AssetTree.InvokeMethod("ReloadData");

                        window.GetMemberValue("m_SearchFilter")?.SetMemberValue("m_Folders", new[] { folderPath });


                        window.SetPropertyValue("isLocked", true);

                    }


                    window.InvokeMethod("Init");

                    setSavedGridSize();
                    setLastUsedGridSize();

                    setSavedLayout();
                    setLastUsedLayout();

                    setLastUsedListWidth();

                    lockToFolder_twoColumns();
                    lockToFolder_oneColumn();

                    UpdateBrowserTitle(window);

                }
                void setupPropertyEditor()
                {
                    if (!isPropertyEditor) return;
                    if (globalId.isNull) return;


                    var lockTo = globalId.GetObject();

                    if (lockedPrefabAssetObject)
                        lockTo = lockedPrefabAssetObject; // globalId api doesn't work for prefab asset objects, so we use direct object reference in such cases 

                    if (!lockTo) return;


                    window.GetMemberValue("tracker").InvokeMethod("SetObjectsLockedByThisTracker", (new List<Object> { lockTo }));

                    window.SetMemberValue("m_GlobalObjectId", globalId.ToString());
                    window.SetMemberValue("m_InspectedObject", lockTo);

                    UpdatePropertyEditorTitle(window);

                }

                void setCustomEditorWindowTitle()
                {
                    if (window.titleContent.text != window.GetType().FullName) return;
                    if (originalTitle.IsNullOrEmpty()) return;

                    window.titleContent.text = originalTitle;

                    // custom EditorWindows often have their titles set in EditorWindow.GetWindow
                    // and when such windows are created via ScriptableObject.CreateInstance, their titles default to window type name
                    // so we have to set original window title in such cases

                }


                notifyVFavorites();
                addToDockArea();

                setupBrowser();
                setupPropertyEditor();

                setCustomEditorWindowTitle();


                window.Focus();



                return window;
            }



            public TabInfo(EditorWindow window)
            {
                typeName = window.GetType().Name;
                originalDockArea = GetDockArea(window);
                originalTabIndex = GetTabList(window).IndexOf(window);
                originalTitle = window.titleContent.text;
                menuItemName = window.titleContent.text.Replace("/", " \u2215 ").Trim(' ');

                if (isBrowser)
                {
                    isLocked = window.GetPropertyValue<bool>("isLocked");


                    savedGridSize = window.GetFieldValue<int>("m_StartGridSize");

                    isGridSizeSaved = true;


                    savedLayout = window.GetMemberValue<int>("m_ViewMode");

                    isLayoutSaved = true;


                    var folderPath = savedLayout == 0 ? window.GetMemberValue("m_SearchFilter")?.GetMemberValue<string[]>("m_Folders")?.FirstOrDefault() ?? "Assets" // one column
                                                      : window.InvokeMethod<string>("GetActiveFolderPath");                                                          // two columns
                    folderGuid = folderPath.ToGuid();

                }

                if (isPropertyEditor)
                    globalId = new GlobalID(window.GetMemberValue<string>("m_GlobalObjectId"));

            }
            public TabInfo(Object lockTo)
            {
                isLocked = true;
                typeName = lockTo is DefaultAsset ? t_ProjectBrowser.Name : t_PropertyEditor.Name;


                if (isBrowser)
                    folderGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(lockTo));

                if (isPropertyEditor)
                    globalId = lockTo.GetGlobalID();

#if UNITY_2021_2_OR_NEWER
                if (isPropertyEditor)
                    if (StageUtility.GetCurrentStage() is PrefabStage && globalId.ToString().Contains("00000000000000000000000000000000"))
                        lockedPrefabAssetObject = lockTo;
#endif

            }
            public TabInfo(string typeName, string menuItemName) { this.typeName = typeName; this.menuItemName = menuItemName; }

            public string typeName;
            public string menuItemName;
            public object originalDockArea;
            public int originalTabIndex;
            public string originalTitle;

            public bool isBrowser => typeName == t_ProjectBrowser.Name;
            public bool isLocked;
            public string folderGuid = "";
            public int savedGridSize;
            public int savedLayout;
            public bool isGridSizeSaved = false;
            public bool isLayoutSaved = false;

            public bool isPropertyEditor => typeName == t_PropertyEditor.Name;
            public GlobalID globalId;
            public Object lockedPrefabAssetObject;

        }

        [System.Serializable]
        class TabInfoList { public List<TabInfo> list = new List<TabInfo>(); }











        static void UpdateTitle(EditorWindow window)
        {
            if (window == null) return;

            var isPropertyEditor = window.GetType() == t_PropertyEditor;
            var isBrowser = window.GetType() == t_ProjectBrowser;

            if (!isPropertyEditor && !isBrowser) return;


            if (isPropertyEditor)
                UpdatePropertyEditorTitle(window);

            if (isBrowser)
                if (window.GetPropertyValue<bool>("isLocked"))
                    UpdateBrowserTitle(window);

        }

        static void UpdateBrowserTitle(EditorWindow browser)
        {
            if (mi_VFavorites_CanBrowserBeWrapped != null && mi_VFavorites_CanBrowserBeWrapped.Invoke(null, new[] { browser }).Equals(false)) return;

            var isLocked = browser.GetMemberValue<bool>("isLocked");
            var isTitleDefault = browser.titleContent.text == "Project";

            void setLockedTitle()
            {
                if (!isLocked) return;

                var isOneColumn = browser.GetMemberValue<int>("m_ViewMode") == 0;

                var path = isOneColumn ? browser.GetMemberValue("m_SearchFilter")?.GetMemberValue<string[]>("m_Folders")?.FirstOrDefault() ?? "Assets" : browser.InvokeMethod<string>("GetActiveFolderPath");
                var guid = path.ToGuid();

                var name = path.GetFilename();
                var icon = EditorGUIUtility.FindTexture("Project");


                void getIconFromVFolders()
                {
                    if (mi_VFolders_GetIcon == null) return;

                    if (mi_VFolders_GetIcon.Invoke(null, new[] { guid }) is Texture2D iconFromVFolders)
                        icon = iconFromVFolders;

                }

                getIconFromVFolders();


                browser.titleContent = new GUIContent(name, icon);

                t_DockArea.GetFieldValue<IDictionary>("s_GUIContents").Clear();

            }
            void setDefaultTitle()
            {
                if (isLocked) return;
                if (isTitleDefault) return;

                var name = "Project";
                var icon = EditorGUIUtility.FindTexture("Project@2x");

                browser.titleContent = new GUIContent(name, icon);

                t_DockArea.GetFieldValue<IDictionary>("s_GUIContents").Clear();

            }

            setLockedTitle();
            setDefaultTitle();

        }
        static void UpdateBrowserTitles() => allBrowsers.ToList().ForEach(r => UpdateBrowserTitle(r));

        static void UpdatePropertyEditorTitle(EditorWindow propertyEditor)
        {
            var obj = propertyEditor.GetMemberValue<Object>("m_InspectedObject");

            if (!obj) return;


            var name = obj is Component component ? GetComponentName(component) : obj.name;
            var sourceIcon = AssetPreview.GetMiniThumbnail(obj);
            var adjustedIcon = sourceIcon;


            void getSourceIconFromVHierarchy()
            {
                if (mi_VHierarchy_GetIcon == null) return;
                if (!(obj is GameObject gameObject)) return;

                if (mi_VHierarchy_GetIcon.Invoke(null, new[] { gameObject }) is Texture2D iconFromVHierarchy)
                    sourceIcon = iconFromVHierarchy;

            }
            void getAdjustedIcon()
            {
                if (adjustedObjectIconsBySourceIid.TryGetValue(sourceIcon.GetInstanceID(), out adjustedIcon)) return;


                adjustedIcon = new Texture2D(sourceIcon.width, sourceIcon.height, sourceIcon.format, sourceIcon.mipmapCount, false);
                adjustedIcon.hideFlags = HideFlags.DontSave;
                adjustedIcon.SetPropertyValue("pixelsPerPoint", (sourceIcon.width / 16f).RoundToInt());

                Graphics.CopyTexture(sourceIcon, adjustedIcon);


                adjustedObjectIconsBySourceIid[sourceIcon.GetInstanceID()] = adjustedIcon;

            }


            getSourceIconFromVHierarchy();
            getAdjustedIcon();

            propertyEditor.titleContent = new GUIContent(name, adjustedIcon);

            propertyEditor.SetMemberValue("m_InspectedObject", null); // prevents further title updates from both internal code and vTabs

            t_DockArea.GetFieldValue<IDictionary>("s_GUIContents").Clear();

        }
        static void UpdatePropertyEditorTitles() => allPropertyEditors.ForEach(r => UpdatePropertyEditorTitle(r));

        static Dictionary<int, Texture2D> adjustedObjectIconsBySourceIid = new Dictionary<int, Texture2D>();




        static void UpdateGUIWrappingForBrowser(EditorWindow browser)
        {
            if (!browser.hasFocus) return;
            if (mi_VFavorites_CanBrowserBeWrapped != null && mi_VFavorites_CanBrowserBeWrapped.Invoke(null, new[] { browser }).Equals(false)) return;

            var isLocked = browser.GetMemberValue<bool>("isLocked");
            var isWrapped = browser.GetMemberValue("m_Parent").GetMemberValue<Delegate>("m_OnGUI").Method.Name == nameof(WrappedBrowserOnGUI);

            void wrap()
            {
                if (!isLocked) return;
                if (isWrapped) return;

                var hostView = browser.GetMemberValue("m_Parent");

                var newDelegate = typeof(VTabs).GetMethod(nameof(WrappedBrowserOnGUI), maxBindingFlags).CreateDelegate(t_EditorWindowDelegate, browser);

                hostView.SetMemberValue("m_OnGUI", newDelegate);

                browser.Repaint();


                browser.SetMemberValue("useTreeViewSelectionInsteadOfMainSelection", false);

            }
            void unwrap()
            {
                if (isLocked) return;
                if (!isWrapped) return;

                var hostView = browser.GetMemberValue("m_Parent");

                var originalDelegate = hostView.InvokeMethod("CreateDelegate", "OnGUI");

                hostView.SetMemberValue("m_OnGUI", originalDelegate);

                browser.Repaint();

            }

            wrap();
            unwrap();

        }
        static void UpdateGUIWrappingForAllBrowsers() => allBrowsers.ForEach(r => UpdateGUIWrappingForBrowser(r));

        static void WrappedBrowserOnGUI(EditorWindow browser)
        {
            var headerHeight = 26;
            var footerHeight = 21;
            var breadcrubsYOffset = .5f;

            var headerRect = browser.position.SetPos(0, 0).SetHeight(headerHeight);
            var footerRect = browser.position.SetPos(0, 0).SetHeightFromBottom(footerHeight);
            var listAreaRect = browser.position.SetPos(0, 0).AddHeight(-footerHeight).AddHeightFromBottom(-headerHeight);

            var breadcrumbsRect = headerRect.AddHeightFromBottom(-breadcrubsYOffset * 2);
            var topGapRect = headerRect.SetHeight(breadcrubsYOffset * 2);

            var breadcrumbsTint = isDarkTheme ? Greyscale(0, .05f) : Greyscale(0, .02f);
            var topGapColor = isDarkTheme ? Greyscale(.24f, 1) : Greyscale(.8f, 1);

            var isOneColumn = browser.GetMemberValue<int>("m_ViewMode") == 0;


            void setRootForOneColumn()
            {
                if (!isOneColumn) return;
                if (curEvent.isRepaint) return;
                if (!(browser.GetMemberValue("m_AssetTree") is object m_AssetTree)) return;
                if (!(m_AssetTree.GetMemberValue("data") is object data)) return;

                var m_rootInstanceID = data.GetMemberValue<int>("m_rootInstanceID");

                void setInitial()
                {
                    if (m_rootInstanceID != 0) return;

                    var folderPath = browser.GetMemberValue("m_SearchFilter")?.GetMemberValue<string[]>("m_Folders")?.FirstOrDefault() ?? "Assets";
                    var folderIid = AssetDatabase.LoadAssetAtPath<Object>(folderPath).GetInstanceID();

                    data.SetMemberValue("m_rootInstanceID", folderIid);

                    m_AssetTree.InvokeMethod("ReloadData");

                }
                void update()
                {
                    if (m_rootInstanceID == 0) return;

                    var folderIid = m_rootInstanceID;
                    var folderPath = EditorUtility.InstanceIDToObject(folderIid).GetPath();

                    browser.GetMemberValue("m_SearchFilter")?.SetMemberValue("m_Folders", new[] { folderPath });

                }
                void reset()
                {
                    if (browser.GetMemberValue<bool>("isLocked")) return;

                    data.SetMemberValue("m_rootInstanceID", 0);
                    browser.GetMemberValue("m_SearchFilter")?.SetMemberValue("m_Folders", new[] { "Assets" });

                    m_AssetTree.InvokeMethod("ReloadData");

                    // returns the browser to normal state on unlock

                }

                setInitial();
                update();
                reset();

            }
            void handleFolderChange()
            {
                if (isOneColumn) return;

                void onBreadcrumbsClick()
                {
                    if (!curEvent.isMouseUp) return;
                    if (!breadcrumbsRect.IsHovered()) return;

                    browser.RecordUndo();

                    toCallInGUI += () => UpdateBrowserTitle(browser);
                    toCallInGUI += () => browser.Repaint();

                }
                void onDoubleclick()
                {
                    if (!curEvent.isMouseDown) return;
                    if (curEvent.clickCount != 2) return;

                    browser.RecordUndo();

                    EditorApplication.delayCall += () => UpdateBrowserTitle(browser);
                    EditorApplication.delayCall += () => browser.Repaint();

                }
                void onUndoRedo()
                {
                    if (!curEvent.isKeyDown) return;
                    if (!curEvent.holdingCmdOrCtrl) return;
                    if (curEvent.keyCode != KeyCode.Z) return;

                    var curFolderGuid = browser.InvokeMethod<string>("GetActiveFolderPath").ToGuid();

                    EditorApplication.delayCall += () =>
                    {
                        var delayedFolderGuid = browser.InvokeMethod<string>("GetActiveFolderPath").ToGuid();

                        if (delayedFolderGuid == curFolderGuid) return;


                        var folderIid = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(delayedFolderGuid)).GetInstanceID();

                        browser.InvokeMethod("SetFolderSelection", new[] { folderIid }, false);

                        UpdateBrowserTitle(browser);

                    };

                }

                onBreadcrumbsClick();
                onDoubleclick();
                onUndoRedo();

            }

            void oneColumn()
            {
                if (!isOneColumn) return;




                if (!browser.InvokeMethod<bool>("Initialized"))
                    browser.InvokeMethod("Init");



                var m_TreeViewKeyboardControlID = GUIUtility.GetControlID(FocusType.Keyboard);







                browser.InvokeMethod("OnEvent");

                if (curEvent.isMouseDown && browser.position.SetPos(0, 0).IsHovered())
                    t_ProjectBrowser.SetFieldValue("s_LastInteractedProjectBrowser", browser);




                // header
                browser.SetFieldValue("m_ListHeaderRect", breadcrumbsRect);

                if (curEvent.isRepaint)
                    browser.InvokeMethod("BreadCrumbBar");

                breadcrumbsRect.Draw(breadcrumbsTint);
                topGapRect.Draw(topGapColor);




                // footer
                browser.SetFieldValue("m_BottomBarRect", footerRect);
                browser.InvokeMethod("BottomBar");




                // tree
                browser.GetMemberValue("m_AssetTree")?.InvokeMethod("OnGUI", listAreaRect, m_TreeViewKeyboardControlID);












                browser.InvokeMethod("HandleCommandEvents");



            }
            void twoColumns()
            {
                if (isOneColumn) return;



                if (!browser.InvokeMethod<bool>("Initialized"))
                    browser.InvokeMethod("Init");



                var m_ListKeyboardControlID = GUIUtility.GetControlID(FocusType.Keyboard);

                var startGridSize = browser.GetFieldValue("m_ListArea")?.GetMemberValue("gridSize");





                browser.InvokeMethod("OnEvent");

                if (curEvent.isMouseDown && browser.position.SetPos(0, 0).IsHovered())
                    t_ProjectBrowser.SetFieldValue("s_LastInteractedProjectBrowser", browser);




                // header
                browser.SetFieldValue("m_ListHeaderRect", breadcrumbsRect);


                browser.InvokeMethod("BreadCrumbBar");

                breadcrumbsRect.Draw(breadcrumbsTint);
                topGapRect.Draw(topGapColor);




                // footer
                browser.SetFieldValue("m_BottomBarRect", footerRect);
                browser.InvokeMethod("BottomBar");




                // list area
                browser.GetFieldValue("m_ListArea").InvokeMethod("OnGUI", listAreaRect, m_ListKeyboardControlID);

                // block grid size changes when ctrl-shift-scrolling
                if (curEvent.holdingCmdOrCtrl)
                    browser.GetFieldValue("m_ListArea").SetMemberValue("gridSize", startGridSize);





                browser.SetFieldValue("m_StartGridSize", browser.GetFieldValue("m_ListArea").GetMemberValue("gridSize"));

                browser.InvokeMethod("HandleContextClickInListArea", listAreaRect);
                browser.InvokeMethod("HandleCommandEvents");



            }


            setRootForOneColumn();
            handleFolderChange();

            oneColumn();
            twoColumns();

        }






        static void ReplaceTabScrollerButtonsWithGradients()
        {
            void getStyles()
            {
                if (leftScrollerStyle != null && rightScrollerStyle != null) return;

                if (!guiStylesInitialized) TryInitializeGuiStyles();
                if (!guiStylesInitialized) return;

                if (typeof(GUISkin).GetFieldValue("current")?.GetFieldValue<Dictionary<string, GUIStyle>>("m_Styles")?.ContainsKey("dragtab scroller prev") != true) return;
                if (typeof(GUISkin).GetFieldValue("current")?.GetFieldValue<Dictionary<string, GUIStyle>>("m_Styles")?.ContainsKey("dragtab scroller next") != true) return;


                var t_Styles = typeof(Editor).Assembly.GetType("UnityEditor.DockArea+Styles");

                leftScrollerStyle = t_Styles.GetFieldValue<GUIStyle>("tabScrollerPrevButton");
                rightScrollerStyle = t_Styles.GetFieldValue<GUIStyle>("tabScrollerNextButton");

            }
            void createTextures()
            {
                if (leftScrollerGradient != null && rightScrollerGradient != null && clearTexture != null) return;


                clearTexture = new Texture2D(1, 1);
                clearTexture.hideFlags = HideFlags.DontSave;
                clearTexture.SetPixel(0, 0, Color.clear);
                clearTexture.Apply();


                var res = 16;
                var greyscale = EditorGUIUtility.isProSkin ? .16f : .65f;

                leftScrollerGradient = new Texture2D(res, 1);
                leftScrollerGradient.hideFlags = HideFlags.DontSave;
                leftScrollerGradient.SetPixels(Enumerable.Range(0, res).Select(r => Greyscale(greyscale, r / (res - 1f))).Reverse().ToArray(), 0);
                leftScrollerGradient.Apply();

                rightScrollerGradient = new Texture2D(res, 1);
                rightScrollerGradient.hideFlags = HideFlags.DontSave;
                rightScrollerGradient.SetPixels(Enumerable.Range(0, res).Select(r => Greyscale(greyscale, r / (res - 1f))).ToArray(), 0);
                rightScrollerGradient.Apply();

            }
            void assignTextures()
            {
                if (leftScrollerStyle == null) return;
                if (rightScrollerStyle == null) return;

                // var hideLeftGradient = EditorWindow.focusedWindow && EditorWindow.focusedWindow.docked && GetTabList(EditorWindow.focusedWindow).First() == EditorWindow.focusedWindow;
                // var hideRightGradient = EditorWindow.focusedWindow && EditorWindow.focusedWindow.docked && GetTabList(EditorWindow.focusedWindow).Last() == EditorWindow.focusedWindow;
                var hideLeftGradient = false;
                var hideRightGradient = false;

                leftScrollerStyle.normal.background = hideLeftGradient ? clearTexture : leftScrollerGradient;
                rightScrollerStyle.normal.background = hideRightGradient ? clearTexture : rightScrollerGradient;
            }

            getStyles();
            createTextures();
            assignTextures();

        }

        static GUIStyle leftScrollerStyle;
        static GUIStyle rightScrollerStyle;

        static Texture2D leftScrollerGradient;
        static Texture2D rightScrollerGradient;
        static Texture2D clearTexture;






        static void ClosePropertyEditorsWithNonLoadableObjects()
        {
            foreach (var propertyEditor in allPropertyEditors)
                if (propertyEditor.GetMemberValue<Object>("m_InspectedObject") == null)
                    propertyEditor.Close();

        }

        static void LoadPropertyEditorInspectedObjects()
        {
            foreach (var propertyEditor in allPropertyEditors)
                propertyEditor.InvokeMethod("LoadPersistedObject");

        }




        static void EnsureTabVisibleOnScroller(EditorWindow window)
        {
            var pos = GetOptimalTabScrollerPosition(window);

            if (!pos.Approx(0))
                pos += nonZeroTabScrollOffset;

            GetDockArea(window).SetFieldValue("m_ScrollOffset", pos);

        }
        static void EnsureActiveTabsVisibleOnScroller() => allEditorWindows.Where(r => r.hasFocus && !r.maximized && r.docked).ForEach(r => EnsureTabVisibleOnScroller(r));

        static float GetOptimalTabScrollerPosition(EditorWindow activeTab)
        {

            var dockArea = activeTab.GetMemberValue("m_Parent");
            var tabAreaWidth = dockArea.GetFieldValue<Rect>("m_TabAreaRect").width;

            if (tabAreaWidth == 0)
                tabAreaWidth = activeTab.position.width - 38;

            if (tabStyle == null)
                if (guiStylesInitialized)
                    tabStyle = new GUIStyle("dragtab");
                else return 0;




            var activeTabXMin = 0f;
            var activeTabXMax = 0f;

            var tabWidthSum = 0f;

            var activeTabReached = false;

            foreach (var tab in GetTabList(activeTab))
            {
                var tabWidth = dockArea.InvokeMethod<float>("GetTabWidth", tabStyle, tab);

                tabWidthSum += tabWidth;


                if (activeTabReached) continue;

                activeTabXMin = activeTabXMax;
                activeTabXMax += tabWidth;

                if (tab == activeTab)
                    activeTabReached = true;

            }




            var optimalScrollPos = 0f;

            var visibleAreaPadding = 65f;

            var visibleAreaXMin = activeTabXMin - visibleAreaPadding;
            var visibleAreaXMax = activeTabXMax + visibleAreaPadding;

            optimalScrollPos = Mathf.Max(optimalScrollPos, visibleAreaXMax - tabAreaWidth);
            optimalScrollPos = Mathf.Min(optimalScrollPos, tabWidthSum - tabAreaWidth + 4);

            optimalScrollPos = Mathf.Min(optimalScrollPos, visibleAreaXMin);
            optimalScrollPos = Mathf.Max(optimalScrollPos, 0);




            return optimalScrollPos;

        }

        static GUIStyle tabStyle;

        static float nonZeroTabScrollOffset = 3f;








        [UnityEditor.Callbacks.PostProcessBuild]
        static void OnBuild(BuildTarget _, string __)
        {
            EditorApplication.delayCall += LoadPropertyEditorInspectedObjects;
            EditorApplication.delayCall += UpdatePropertyEditorTitles;
        }

        static void OnDomainReloaded()
        {
            toCallInGUI += UpdateGUIWrappingForAllBrowsers;
            toCallInGUI += UpdateBrowserTitles;

        }

        static void OnSceneOpened(Scene _, OpenSceneMode __)
        {
            LoadPropertyEditorInspectedObjects();
            ClosePropertyEditorsWithNonLoadableObjects();
            UpdatePropertyEditorTitles();

        }

        static void OnProjectLoaded()
        {
            toCallInGUI += EnsureActiveTabsVisibleOnScroller;

            UpdatePropertyEditorTitles();

        }

        static void OnFocusedWindowChanged()
        {
            if (EditorWindow.focusedWindow?.GetType() == t_ProjectBrowser)
                UpdateGUIWrappingForBrowser(EditorWindow.focusedWindow);

        }

        static void OnWindowUnmaximized()
        {
            UpdatePropertyEditorTitles();
            UpdateBrowserTitles();

            UpdateGUIWrappingForAllBrowsers();


            EnsureActiveTabsVisibleOnScroller();

        }







        static void CheckIfFocusedWindowChanged()
        {
            if (prevFocusedWindow != EditorWindow.focusedWindow)
                OnFocusedWindowChanged();

            prevFocusedWindow = EditorWindow.focusedWindow;
        }

        static EditorWindow prevFocusedWindow;



        static void CheckIfWindowWasUnmaximized()
        {
            var isMaximized = EditorWindow.focusedWindow?.maximized == true;

            if (!isMaximized && wasMaximized)
                OnWindowUnmaximized();

            wasMaximized = isMaximized;

        }

        static bool wasMaximized;




        static void OnSomeGUI()
        {
            toCallInGUI?.Invoke();
            toCallInGUI = null;

            CheckIfFocusedWindowChanged();

        }

        static void ProjectWindowItemOnGUI(string _, Rect __) => OnSomeGUI();
        static void HierarchyWindowItemOnGUI(int _, Rect __) => OnSomeGUI();

        static Action toCallInGUI;






        static void DelayCallLoop()
        {
            UpdateBrowserTitles();
            UpdateGUIWrappingForAllBrowsers();
            UpdateDelayedMousePosition();
            ReplaceTabScrollerButtonsWithGradients();

            EditorApplication.delayCall -= DelayCallLoop;
            EditorApplication.delayCall += DelayCallLoop;

        }




        static void UpdateDelayedMousePosition()
        {
            var lastEvent = typeof(Event).GetFieldValue<Event>("s_Current");

            delayedMousePosition_screenSpace = EditorGUIUtility.GUIToScreenPoint(lastEvent.mousePosition);

        }

        static Vector2 delayedMousePosition_screenSpace;















        static void ComponentTabHeaderGUI(Editor editor)
        {
            if (!(editor.target is Component component)) return;

            var headerRect = ExpandWidthLabelRect(height: 0).MoveY(-48).SetHeight(50).AddWidthFromMid(8);
            var nameRect = headerRect.MoveX(43).MoveY(5).SetHeight(20).SetXMax(headerRect.xMax - 50);
            var subtextRect = headerRect.MoveX(43).MoveY(22).SetHeight(20);


            void hideName()
            {
                var maskRect = headerRect.AddWidthFromRight(-45).AddWidth(-50);

                var maskColor = Greyscale(isDarkTheme ? .24f : .8f);

                maskRect.Draw(maskColor);

            }
            void name()
            {
                SetLabelFontSize(13);

                GUI.Label(nameRect, GetComponentName(component));

                ResetLabelStyle();

            }
            void componentOf()
            {
                SetGUIEnabled(false);

                GUI.Label(subtextRect, "Component of");

                ResetGUIEnabled();

            }
            void goName()
            {
                var goNameRect = subtextRect.MoveX("Component of ".GetLabelWidth() - 3).SetWidth(component.gameObject.name.GetLabelWidth(isBold: true));

                goNameRect.MarkInteractive();

                SetGUIEnabled(goNameRect.IsHovered() && !mousePressedOnGoName);
                SetLabelBold();

                GUI.Label(goNameRect, component.gameObject.name);

                ResetGUIEnabled();
                ResetLabelStyle();



                if (curEvent.isMouseDown && goNameRect.IsHovered())
                {
                    mousePressedOnGoName = true;
                    curEvent.Use();
                }

                if (curEvent.isMouseUp)
                {
                    if (mousePressedOnGoName)
                        EditorGUIUtility.PingObject(component.gameObject);

                    mousePressedOnGoName = false;
                    curEvent.Use();
                }

                if (curEvent.isMouseLeaveWindow || (!curEvent.isLayout && !goNameRect.Resize(1).IsHovered()))
                    mousePressedOnGoName = false;

            }



            hideName();
            name();
            componentOf();
            goName();

            Space(-4);

        }

        static bool mousePressedOnGoName;

        static string GetComponentName(Component component)
        {
            if (!component) return "";

            var name = new GUIContent(EditorGUIUtility.ObjectContent(component, component.GetType())).text;

            name = name.Substring(name.LastIndexOf('(') + 1);
            name = name.Substring(0, name.Length - 1);

            return name;

        }








        static void PreventGameViewZoomOnShiftScroll() // called from Update
        {
            if (!curEvent.holdingShift) return;
            if (Application.isPlaying) return; // zoom by scrolling is disabled in playmode anyway
            if (!(EditorWindow.mouseOverWindow is EditorWindow hoveredWindow)) return;
            if (hoveredWindow.GetType() != t_GameView) return;
            if (!(hoveredWindow.GetMemberValue("m_ZoomArea", false) is object zoomArea)) return;

            var isScroll = !curEvent.isMouseMove && curEvent.mouseDelta != Vector2.zero;

            if (isScroll)
            {
                zoomArea.SetMemberValue("m_Scale", lastGameViewScale);
                zoomArea.SetMemberValue("m_Translation", lastGameViewTranslation);
            }
            else
            {
                lastGameViewScale = zoomArea.GetMemberValue<Vector2>("m_Scale");
                lastGameViewTranslation = zoomArea.GetMemberValue<Vector2>("m_Translation");
            }
        }

        static Vector2 lastGameViewScale = Vector2.one;
        static Vector2 lastGameViewTranslation = Vector2.zero;








        static void TryInitializeGuiStyles() => EditorWindow.focusedWindow?.SendEvent(EditorGUIUtility.CommandEvent(""));

        static bool guiStylesInitialized => typeof(GUI).GetFieldValue("s_Skin") != null;





        static object GetDockArea(EditorWindow window) => window.GetFieldValue("m_Parent");

        static List<EditorWindow> GetTabList(EditorWindow window) => GetDockArea(window).GetFieldValue<List<EditorWindow>>("m_Panes");









        [InitializeOnLoadMethod]
        static void Init()
        {
            if (VTabsMenu.pluginDisabled) return;



            // dragndrop and scrolling
            EditorApplication.delayCall += () => EditorApplication.update -= Update;
            EditorApplication.delayCall += () => EditorApplication.update += Update;

            EditorApplication.delayCall -= UpdateDelayedMousePosition;
            EditorApplication.delayCall += UpdateDelayedMousePosition;

            EditorApplication.update -= PreventGameViewZoomOnShiftScroll;
            EditorApplication.update += PreventGameViewZoomOnShiftScroll;



            // shortcuts
            var globalEventHandler = typeof(EditorApplication).GetFieldValue<EditorApplication.CallbackFunction>("globalEventHandler");
            typeof(EditorApplication).SetFieldValue("globalEventHandler", (globalEventHandler - CheckShortcuts) + CheckShortcuts);


            // component tabs
            Editor.finishedDefaultHeaderGUI -= ComponentTabHeaderGUI;
            Editor.finishedDefaultHeaderGUI += ComponentTabHeaderGUI;




            // state change detectors
            var projectWasLoaded = typeof(EditorApplication).GetFieldValue<UnityEngine.Events.UnityAction>("projectWasLoaded");
            typeof(EditorApplication).SetFieldValue("projectWasLoaded", (projectWasLoaded - OnProjectLoaded) + OnProjectLoaded);

            UnityEditor.SceneManagement.EditorSceneManager.sceneOpened -= OnSceneOpened;
            UnityEditor.SceneManagement.EditorSceneManager.sceneOpened += OnSceneOpened;

            EditorApplication.projectWindowItemOnGUI -= ProjectWindowItemOnGUI;
            EditorApplication.projectWindowItemOnGUI += ProjectWindowItemOnGUI;

            EditorApplication.hierarchyWindowItemOnGUI -= HierarchyWindowItemOnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;

            EditorApplication.delayCall -= DelayCallLoop;
            EditorApplication.delayCall += DelayCallLoop;




            OnDomainReloaded();

        }








        static IEnumerable<EditorWindow> allBrowsers => _allBrowsers ??= t_ProjectBrowser.GetFieldValue<IList>("s_ProjectBrowsers").Cast<EditorWindow>();
        static IEnumerable<EditorWindow> _allBrowsers;

        static IEnumerable<EditorWindow> allPropertyEditors => Resources.FindObjectsOfTypeAll(t_PropertyEditor).Where(r => r.GetType().BaseType == typeof(EditorWindow)).Cast<EditorWindow>();

        static List<EditorWindow> allEditorWindows => Resources.FindObjectsOfTypeAll<EditorWindow>().ToList();




        static Type t_EditorWindow = typeof(EditorWindow);
        static Type t_DockArea = typeof(Editor).Assembly.GetType("UnityEditor.DockArea");
        static Type t_PropertyEditor = typeof(Editor).Assembly.GetType("UnityEditor.PropertyEditor");
        static Type t_ProjectBrowser = typeof(Editor).Assembly.GetType("UnityEditor.ProjectBrowser");
        static Type t_GameView = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        static Type t_SceneHierarchyWindow = typeof(Editor).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
        static Type t_HostView = typeof(Editor).Assembly.GetType("UnityEditor.HostView");
        static Type t_EditorWindowDelegate = t_HostView.GetNestedType("EditorWindowDelegate", maxBindingFlags);


        static Type t_VHierarchy = Type.GetType("VHierarchy.VHierarchy") ?? Type.GetType("VHierarchy.VHierarchy, VHierarchy, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        static Type t_VFolders = Type.GetType("VFolders.VFolders") ?? Type.GetType("VFolders.VFolders, VFolders, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        static Type t_VFavorites = Type.GetType("VFavorites.VFavorites") ?? Type.GetType("VFavorites.VFavorites, VFavorites, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");

        static MethodInfo mi_VFolders_GetIcon = t_VFolders?.GetMethod("GetSmallFolderIcon_forVTabs", maxBindingFlags);
        static MethodInfo mi_VHierarchy_GetIcon = t_VHierarchy?.GetMethod("GetIcon_forVTabs", maxBindingFlags);
        static MethodInfo mi_VFavorites_BeforeWindowCreated = t_VFavorites?.GetMethod("BeforeWindowCreated_byVTabs", maxBindingFlags);
        static MethodInfo mi_VFavorites_CanBrowserBeWrapped = t_VFavorites?.GetMethod("CanBrowserBeWrapped_byVTabs", maxBindingFlags);





        const string version = "2.0.13";

    }
}
#endif
