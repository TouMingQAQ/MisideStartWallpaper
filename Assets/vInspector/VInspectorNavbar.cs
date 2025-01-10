#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using Type = System.Type;
using static VInspector.Libs.VUtils;
using static VInspector.Libs.VGUI;
using static VInspector.VInspectorData;
using static VInspector.VInspector;


namespace VInspector
{
    public class VInspectorNavbar
    {

        public void OnGUI(Rect navbarRect)
        {
            this.navbarRect = navbarRect;

            void shadow()
            {
                if (!curEvent.isRepaint) return;

                var shadowLength = 30;
                var shadowPos = 21;
                var shadowGreyscale = isDarkTheme ? .08f : .28f;
                var shadowAlpha = .35f;

                var minScrollPos = 10;
                var maxScrollPos = 20;


                var scrollPos = window.GetMemberValue<ScrollView>("m_ScrollView").scrollOffset.y;

                var opacity = ((scrollPos - minScrollPos) / (maxScrollPos - minScrollPos)).Clamp01();


                navbarRect.MoveY(shadowPos).SetHeight(shadowLength).DrawCurtainDown(Greyscale(shadowGreyscale, shadowAlpha * opacity));

            }
            void background()
            {
                if (!curEvent.isRepaint) return;

                var backgroundColor = Greyscale(isDarkTheme ? .235f : .8f);
                var lineColor = Greyscale(isDarkTheme ? .1f : .53f);

                navbarRect.Draw(backgroundColor);

                navbarRect.SetHeightFromBottom(1).MoveY(1).Draw(lineColor);

            }

            void name()
            {
                if (!curEvent.isRepaint) return;

                var nameRect = navbarRect.MoveX(30 * 2 + 1);

                var minScrollPos = 10;
                var maxScrollPos = 20;


                var scrollPos = window.GetMemberValue<ScrollView>("m_ScrollView").scrollOffset.y;

                var opacity = (scrollPos - minScrollPos) / (maxScrollPos - minScrollPos);



                SetGUIColor(Greyscale(.95f, opacity));
                SetLabelBold();

                GUI.Label(nameRect, Selection.activeObject?.name ?? "No selection");

                ResetLabelStyle();
                ResetGUIColor();


            }
            void nameCurtain()
            {
                if (!curEvent.isRepaint) return;
                if (!data) return;
                if (lastItemX == default) return;

                var backgroundColor = Greyscale(isDarkTheme ? .235f : .8f);

                var curtainRect = navbarRect.AddHeightFromMid(-4).SetXMax(lastItemX).SetWidthFromRight(20);
                var maskRect = navbarRect.AddHeightFromMid(-4).SetX(lastItemX).SetWidth(123);

                curtainRect.DrawCurtainLeft(backgroundColor);
                maskRect.Draw(backgroundColor);


            }

            void selectPrev()
            {
                var buttonRect = navbarRect.SetWidth(30).MoveX(4);

                var iconName = "Chevron Left";
                var iconSize = 14;
                var colorNormal = Greyscale(isDarkTheme ? .75f : .2f);
                var colorHovered = Greyscale(isDarkTheme ? 1f : .2f);
                var colorPressed = Greyscale(isDarkTheme ? .75f : .5f);
                var colorDisabled = Greyscale(isDarkTheme ? .53f : .55f);


                var disabled = !VInspectorSelectionHistory.instance.prevStates.Any();

                if (disabled) { IconButton(buttonRect, iconName, iconSize, colorDisabled, colorDisabled, colorDisabled); return; }


                if (!IconButton(buttonRect, iconName, iconSize, colorNormal, colorHovered, colorPressed)) return;

                VInspectorSelectionHistory.instance.MoveBack();

            }
            void selectNext()
            {
                var buttonRect = navbarRect.SetWidth(30).MoveX(30).MoveX(1).AddWidthFromMid(-6);

                var iconName = "Chevron Right";
                var iconSize = 14;
                var colorNormal = Greyscale(isDarkTheme ? .75f : .2f);
                var colorHovered = Greyscale(isDarkTheme ? 1f : .2f);
                var colorPressed = Greyscale(isDarkTheme ? .75f : .5f);
                var colorDisabled = Greyscale(isDarkTheme ? .53f : .55f);


                var disabled = !VInspectorSelectionHistory.instance.nextStates.Any();

                if (disabled) { IconButton(buttonRect, iconName, iconSize, colorDisabled, colorDisabled, colorDisabled); return; }


                if (!IconButton(buttonRect, iconName, iconSize, colorNormal, colorHovered, colorPressed)) return;

                VInspectorSelectionHistory.instance.MoveForward();

            }

            void bookmarks()
            {
                void createData()
                {
                    if (data) return;
                    if (!navbarRect.IsHovered()) return;
                    if (!DragAndDrop.objectReferences.Any()) return;

                    data = ScriptableObject.CreateInstance<VInspectorData>();

                    AssetDatabase.CreateAsset(data, GetScriptPath("VInspector").GetParentPath().CombinePath("vInspector Data.asset"));

                }
                void repaintOnUndoRedo()
                {
                    if (!data) return;
                    if (curEvent.commandName != "UndoRedoPerformed") return;
                    if (!repaintNeededAfterUndoRedo) return;

                    window.Repaint();

                    repaintNeededAfterUndoRedo = false;

                }
                void gui()
                {
                    if (!data) return;

                    bookmarksRect = navbarRect.AddWidth(-5).AddWidthFromRight(-60);

                    BookmarksOnGUI();

                }

                createData();
                repaintOnUndoRedo();
                gui();

            }


            shadow();
            background();

            name();
            nameCurtain();

            selectPrev();
            selectNext();

            bookmarks();

        }

        Rect navbarRect;
        Rect bookmarksRect;







        public void BookmarksOnGUI()
        {
            this.bookmarksRect = bookmarksRect;

            void item(Vector2 centerPosition, Item item)
            {
                if (item == null) return;
                if (curEvent.isLayout) return;


                var itemRect = Rect.zero.SetSize(item.width, bookmarksRect.height).SetMidPos(centerPosition);


                void shadow()
                {
                    if (!draggingItem) return;
                    if (draggedItem != item) return;

                    itemRect.SetSizeFromMid(item.width - 4, expandedItemWidth - 4).DrawBlurred(Greyscale(0, .3f), 15);

                }
                void background()
                {
                    if (!itemRect.IsHovered()) return;
                    if (draggingItem && draggedItem != item) return;

                    var backgroundColor = Greyscale(isDarkTheme ? .35f : .7f);

                    var backgroundRect = itemRect.SetSizeFromMid(itemRect.width - 2, expandedItemWidth - 2);

                    backgroundRect.DrawRounded(backgroundColor, 4);


                }
                void icon()
                {
                    var iconRect = itemRect.SetSizeFromMid(16);

                    Texture iconTexture = null;
                    float opacity = 1f;

                    void getTexture_material()
                    {
                        if (item.type != typeof(Material)) return;

                        iconTexture = item.isLoadable ? AssetPreview.GetAssetPreview(item.obj) ?? AssetPreview.GetMiniThumbnail(item.obj) : AssetPreview.GetMiniTypeThumbnail(item.type);

                    }
                    void getTexture_otherAsset()
                    {
                        if (item.type == typeof(Material)) return;
                        if (!item.isAsset) return;

                        iconTexture = item.isLoadable ? AssetPreview.GetMiniThumbnail(item.obj) : AssetPreview.GetMiniTypeThumbnail(item.type);

                    }
                    void getTexture_sceneGameObject()
                    {
                        if (!item.isSceneGameObject) return;

                        void getIconNameFromAssetPreview()
                        {
                            if (!item.isLoadable) return;

                            item.sceneGameObjectIconName = AssetPreview.GetMiniThumbnail(item.obj).name;

                        }
                        void getIconNameFromVHierarchy()
                        {
                            if (!item.isLoadable) return;
                            if (item.obj is not GameObject gameObject) return;
                            if (mi_VHierarchy_GetIconName == null) return;

                            var iconNameFromVHierarchy = (string)mi_VHierarchy_GetIconName.Invoke(null, new object[] { gameObject });

                            if (!iconNameFromVHierarchy.IsNullOrEmpty())
                                item.sceneGameObjectIconName = iconNameFromVHierarchy;

                        }

                        getIconNameFromAssetPreview();
                        getIconNameFromVHierarchy();

                        iconTexture = EditorGUIUtility.IconContent(item.sceneGameObjectIconName.IsNullOrEmpty() ? "GameObject icon" : item.sceneGameObjectIconName).image;

                    }
                    void set_opacity()
                    {
                        var opacityNormal = .9f;
                        var opacityHovered = 1f;
                        var opacityPressed = .75f;
                        var opacityDragged = .75f;
                        var opacityDisabled = .4f;

                        var isDisabled = !item.isLoadable;


                        opacity = opacityNormal;

                        if (draggingItem)
                            opacity = item == draggedItem ? opacityDragged : opacityNormal;

                        else if (item == pressedItem)
                            opacity = opacityPressed;

                        else if (itemRect.IsHovered())
                            opacity = opacityHovered;

                        if (isDisabled)
                            opacity = opacityDisabled;

                    }
                    void drawTexture_material()
                    {
                        if (item.type != typeof(Material)) return;
                        if (!iconTexture) return;


                        if (!assetPreviewMaterial)
                            assetPreviewMaterial = new Material(Shader.Find("Hidden/VInspectorAssetPreview"));


                        SetGUIColor(Greyscale(1, opacity));

                        assetPreviewMaterial.SetColor("_Color", GUI.color);

                        EditorGUI.DrawPreviewTexture(iconRect, iconTexture, assetPreviewMaterial);

                        ResetGUIColor();

                    }
                    void drawTexture_other()
                    {
                        if (item.type == typeof(Material)) return;
                        if (!iconTexture) return;


                        SetGUIColor(Greyscale(1, opacity));

                        GUI.DrawTexture(iconRect, iconTexture);

                        ResetGUIColor();

                    }

                    getTexture_material();
                    getTexture_otherAsset();
                    getTexture_sceneGameObject();
                    set_opacity();
                    drawTexture_material();
                    drawTexture_other();

                }
                void selectedIndicator()
                {
                    if (Selection.activeObject != item.obj) return;
                    if (Selection.activeObject == null) return;
                    if (draggingItem && draggedItem == item && centerPosition.y != bookmarksRect.center.y) return;

                    var indicatorColor = Greyscale(isDarkTheme ? .75f : 1f);

                    var rect = itemRect.SetHeightFromBottom(3).MoveY(2).SetWidthFromMid(3);

                    rect.DrawRounded(indicatorColor, 1);

                }
                void tooltip()
                {
                    if (item != (draggingItem ? (draggedItem) : (lastHoveredItem))) return;
                    if (tooltipOpacity == 0) return;

                    var fontSize = 11; // ,maybe 12
                    var tooltipText = item.isDeleted ? "Deleted" : item.name;

                    Rect tooltipRect;

                    void set_tooltipRect()
                    {
                        var width = tooltipText.GetLabelWidth(fontSize) + 6;
                        var height = 16 + (fontSize - 12) * 2;

                        var yOffset = 28;
                        var rightMargin = -1;


                        tooltipRect = Rect.zero.SetMidPos(centerPosition.x, centerPosition.y + yOffset).SetSizeFromMid(width, height);


                        var maxXMax = bookmarksRect.xMax - rightMargin;

                        if (tooltipRect.xMax > maxXMax)
                            tooltipRect = tooltipRect.MoveX(maxXMax - tooltipRect.xMax);

                    }
                    void shadow()
                    {
                        var shadowAmount = .33f;
                        var shadowRadius = 10;

                        tooltipRect.DrawBlurred(Greyscale(0, shadowAmount).MultiplyAlpha(tooltipOpacity), shadowRadius);

                    }
                    void background()
                    {
                        var cornerRadius = 5;

                        var backgroundColor = Greyscale(isDarkTheme ? .13f : .9f);
                        var outerEdgeColor = Greyscale(isDarkTheme ? .25f : .6f);
                        var innerEdgeColor = Greyscale(isDarkTheme ? .0f : .95f);

                        tooltipRect.Resize(-1).DrawRounded(outerEdgeColor.SetAlpha(tooltipOpacity.Pow(2)), cornerRadius + 1);
                        tooltipRect.Resize(0).DrawRounded(innerEdgeColor.SetAlpha(tooltipOpacity.Pow(2)), cornerRadius + 0);
                        tooltipRect.Resize(1).DrawRounded(backgroundColor.SetAlpha(tooltipOpacity), cornerRadius - 1);

                    }
                    void text()
                    {
                        var textRect = tooltipRect.MoveY(-.5f);

                        var textColor = Greyscale(1f);

                        SetLabelAlignmentCenter();
                        SetLabelFontSize(fontSize);
                        SetGUIColor(textColor.SetAlpha(tooltipOpacity));

                        GUI.Label(textRect, tooltipText);

                        ResetLabelStyle();
                        ResetGUIColor();

                    }

                    set_tooltipRect();
                    shadow();
                    background();
                    text();

                }
                void click()
                {
                    if (!itemRect.IsHovered()) return;
                    if (!curEvent.isMouseUp) return;
                    if (doubleclickUnhandled) return;

                    curEvent.Use();


                    if (draggingItem) return;
                    if ((curEvent.mousePosition - mouseDownPosiion).magnitude > 2) return;
                    if (!item.isLoadable) return;

                    Selection.activeObject = item.obj;

                    lastClickedItem = item;

                    hideTooltip = true;

                }
                void doubleclick()
                {
                    if (!itemRect.IsHovered()) return;
                    if (!curEvent.isMouseUp) return;
                    if (!doubleclickUnhandled) return;

                    void frameSceneGO()
                    {
                        if (!item.isLoadable) return;
                        if (!item.isSceneGameObject) return;
                        if (item.obj is not GameObject go) return;


                        var sv = SceneView.lastActiveSceneView;

                        if (!sv || !sv.hasFocus)
                            sv = SceneView.sceneViews.ToArray().FirstOrDefault(r => (r as SceneView).hasFocus) as SceneView;

                        if (!sv)
                            (sv = SceneView.lastActiveSceneView ?? SceneView.sceneViews[0] as SceneView).Focus();

                        sv.Frame(go.GetBounds(), false);

                    }
                    void loadSceneAndSelect()
                    {
                        if (!item.isSceneGameObject) return;
                        if (item.isLoadable) return;
                        if (item.isDeleted) return;
                        if (Application.isPlaying) return;

                        EditorSceneManager.SaveOpenScenes();
                        EditorSceneManager.OpenScene(item.assetPath);

                        Selection.activeObject = item.obj;

                    }
                    void openPrefab()
                    {
                        if (!item.isLoadable) return;
                        if (item.obj is not GameObject gameObject) return;
                        if (!AssetDatabase.Contains(gameObject)) return;

                        AssetDatabase.OpenAsset(gameObject);

                    }

                    frameSceneGO();
                    loadSceneAndSelect();
                    openPrefab();

                    doubleclickUnhandled = false;

                }


                itemRect.MarkInteractive();

                shadow();
                background();
                icon();
                selectedIndicator();
                tooltip();
                click();
                doubleclick();

            }

            void normalItem(int i)
            {
                if (data.items[i] == droppedItem && animatingDroppedItem) return;

                var centerX = GetItemCenterX(i);
                var centerY = bookmarksRect.height / 2;


                var minX = centerX - data.items[i].width / 2;

                if (minX < bookmarksRect.x) return;

                lastItemX = minX;


                item(new Vector2(centerX, centerY), data.items[i]);

            }
            void draggedItem_()
            {
                if (!draggingItem) return;

                var centerX = curEvent.mousePosition.x + draggedItemHoldOffset.x;
                var centerY = bookmarksRect.IsHovered() ? bookmarksRect.height / 2 : curEvent.mousePosition.y;

                item(new Vector2(centerX, centerY), draggedItem);

            }
            void droppedItem_()
            {
                if (!animatingDroppedItem) return;

                var centerX = droppedItemX;
                var centerY = bookmarksRect.height / 2;

                item(new Vector2(centerX, centerY), droppedItem);

            }


            UpdateMouseState();
            UpdateAnimations();
            UpdateDragging();

            for (int i = 0; i < data.items.Count; i++)
                normalItem(i);

            draggedItem_();
            droppedItem_();


            if (draggingItem || animatingDroppedItem || animatingGaps || animatingTooltip)
                window.Repaint();

        }

        float GetItemCenterX(int i)
        {
            return bookmarksRect.xMax
                 - data.items[i].width / 2
                 - data.items.Take(i).Sum(r => r.width)
                 - gaps.Take(i + 1).Sum();

        }
        int GetItemIndex(float mouseX)
        {
            var curItemWidthSum = 0f;

            for (int i = 0; i < data.items.Count; i++)
            {
                curItemWidthSum += data.items[i].width;

                if (bookmarksRect.xMax - curItemWidthSum < mouseX + .5f)
                    return i;
            }

            return data.items.Count;

        }

        public static float expandedItemWidth = 24;

        public float lastItemX;

        Material assetPreviewMaterial;

        public static bool repaintNeededAfterUndoRedo;





        void UpdateMouseState()
        {
            void down()
            {
                if (!curEvent.isMouseDown) return;

                mousePressed = true;

                mouseDownPosiion = curEvent.mousePosition;

                var pressedItemIndex = GetItemIndex(mouseDownPosiion.x);

                if (pressedItemIndex.IsInRangeOf(data.items))
                    pressedItem = data.items[pressedItemIndex];

                doubleclickUnhandled = curEvent.clickCount == 2;

                curEvent.Use();

            }
            void up()
            {
                if (!curEvent.isMouseUp) return;

                mousePressed = false;
                pressedItem = null;

            }
            void hover()
            {
                var hoveredItemIndex = GetItemIndex(curEvent.mousePosition.x);

                mouseHoversItem = bookmarksRect.IsHovered() && hoveredItemIndex.IsInRangeOf(data.items);

                if (mouseHoversItem)
                    lastHoveredItem = data.items[hoveredItemIndex];


            }

            down();
            up();
            hover();

        }

        bool mouseHoversItem;
        bool mousePressed;
        bool doubleclickUnhandled;
        Vector2 mouseDownPosiion;

        Item pressedItem;
        Item lastHoveredItem;






        void UpdateAnimations()
        {
            void set_deltaTime()
            {
                if (!curEvent.isLayout) return;

                deltaTime = (float)(EditorApplication.timeSinceStartup - lastLayoutTime);

                if (deltaTime > .05f)
                    deltaTime = .0166f;

                lastLayoutTime = EditorApplication.timeSinceStartup;



            }
            void gaps_()
            {
                if (!curEvent.isLayout) return;


                var makeSpaceForDraggedItem = draggingItem && bookmarksRect.IsHovered();

                var lerpSpeed = 12;

                for (int i = 0; i < gaps.Count; i++)
                    if (makeSpaceForDraggedItem && i == insertDraggedItemAtIndex)
                        gaps[i] = Lerp(gaps[i], draggedItem.width, lerpSpeed, deltaTime);
                    else
                        gaps[i] = Lerp(gaps[i], 0, lerpSpeed, deltaTime);




                for (int i = 0; i < gaps.Count; i++)
                    if (gaps[i].Approx(0))
                        gaps[i] = 0;
                // else if (gaps[i].Approx(draggedItem.width))
                // gaps[i] = draggedItem.width;



                animatingGaps = gaps.Any(r => r > .5f);


            }
            void droppedItem_()
            {
                if (!curEvent.isLayout) return;
                if (!animatingDroppedItem) return;

                var lerpSpeed = 8;

                var targX = GetItemCenterX(data.items.IndexOf(droppedItem));

                SmoothDamp(ref droppedItemX, targX, lerpSpeed, ref droppedItemXDerivative, deltaTime);

                if ((droppedItemX - targX).Abs() < .5f)
                    animatingDroppedItem = false;

            }
            void tooltip()
            {
                if (!curEvent.isLayout) return;


                if (!mouseHoversItem || lastHoveredItem != lastClickedItem)
                    hideTooltip = false;


                var lerpSpeed = UnityEditorInternal.InternalEditorUtility.isApplicationActive ? 15 : 12321;

                if (mouseHoversItem && !draggingItem && !hideTooltip)
                    SmoothDamp(ref tooltipOpacity, 1, lerpSpeed, ref tooltipOpacityDerivative, deltaTime);
                else
                    SmoothDamp(ref tooltipOpacity, 0, lerpSpeed, ref tooltipOpacityDerivative, deltaTime);


                if (tooltipOpacity > .99f)
                    tooltipOpacity = 1;

                if (tooltipOpacity < .01f)
                    tooltipOpacity = 0;


                animatingTooltip = tooltipOpacity != 0 && tooltipOpacity != 1;

            }

            set_deltaTime();
            gaps_();
            droppedItem_();
            tooltip();

        }

        List<float> gaps
        {
            get
            {
                while (_gaps.Count < data.items.Count + 1) _gaps.Add(0);
                while (_gaps.Count > data.items.Count + 1) _gaps.RemoveLast();

                return _gaps;

            }
        }
        List<float> _gaps = new();

        float deltaTime;
        double lastLayoutTime;

        float droppedItemX;
        float droppedItemXDerivative;

        float tooltipOpacity;
        float tooltipOpacityDerivative;

        bool animatingDroppedItem;
        bool animatingGaps;
        bool animatingTooltip;

        Item lastClickedItem;
        bool hideTooltip;






        void UpdateDragging()
        {
            void initFromOutside()
            {
                if (draggingItem) return;
                if (!bookmarksRect.IsHovered()) return;
                if (!curEvent.isDragUpdate) return;
                if (DragAndDrop.objectReferences.FirstOrDefault() is not Object draggedObject) return;

                if (draggedObject is DefaultAsset) return;
                if (draggedObject is Component) return;
                if (draggedObject is GameObject go && StageUtility.GetCurrentStage() is PrefabStage && !AssetDatabase.Contains(go)) return;

                animatingDroppedItem = false;

                draggingItem = true;
                draggingItemFromInside = false;

                draggedItem = new Item(draggedObject);
                draggedItemHoldOffset = Vector2.zero;

            }
            void initFromInside()
            {
                if (draggingItem) return;
                if (!mousePressed) return;
                if ((curEvent.mousePosition - mouseDownPosiion).magnitude <= 2) return;
                if (pressedItem == null) return;

                var i = GetItemIndex(mouseDownPosiion.x);

                if (i >= data.items.Count) return;
                if (i < 0) return;


                animatingDroppedItem = false;

                draggingItem = true;
                draggingItemFromInside = true;

                draggedItem = data.items[i];
                draggedItemHoldOffset = new Vector2(GetItemCenterX(i) - mouseDownPosiion.x, bookmarksRect.center.y - mouseDownPosiion.y);

                gaps[i] = draggedItem.width;


                data.RecordUndo();

                data.items.Remove(draggedItem);

            }

            void acceptFromOutside()
            {
                if (!draggingItem) return;
                if (!curEvent.isDragPerform) return;
                if (!bookmarksRect.IsHovered()) return;

                DragAndDrop.AcceptDrag();
                curEvent.Use();

                data.RecordUndo();

                accept();

                data.Dirty();
                data.Save();

            }
            void acceptFromInside()
            {
                if (!draggingItem) return;
                if (!curEvent.isMouseUp) return;
                if (!bookmarksRect.IsHovered()) return;

                curEvent.Use();
                EditorGUIUtility.hotControl = 0;

                DragAndDrop.PrepareStartDrag(); // fixes phantom dragged component indicator after reordering bookmarks

                data.RecordUndo();
                data.Dirty();

                accept();

            }
            void accept()
            {
                draggingItem = false;
                draggingItemFromInside = false;
                mousePressed = false;

                data.items.AddAt(draggedItem, insertDraggedItemAtIndex);

                gaps[insertDraggedItemAtIndex] -= draggedItem.width;
                gaps.AddAt(0, insertDraggedItemAtIndex);

                droppedItem = draggedItem;

                droppedItemX = curEvent.mousePosition.x + draggedItemHoldOffset.x;
                droppedItemXDerivative = 0;
                animatingDroppedItem = true;

                draggedItem = null;

                EditorGUIUtility.hotControl = 0;

            }

            void cancelFromOutside()
            {
                if (!draggingItem) return;
                if (draggingItemFromInside) return;
                if (bookmarksRect.IsHovered()) return;

                draggingItem = false;
                mousePressed = false;

            }
            void cancelFromInsideAndDelete()
            {
                if (!draggingItem) return;
                if (!curEvent.isMouseUp) return;
                if (bookmarksRect.IsHovered()) return;

                draggingItem = false;

                DragAndDrop.PrepareStartDrag(); // fixes phantom dragged component indicator after reordering bookmarks

                data.Dirty();

            }

            void update()
            {
                if (!draggingItem) return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Generic;

                EditorGUIUtility.hotControl = EditorGUIUtility.GetControlID(FocusType.Passive);



                insertDraggedItemAtIndex = GetItemIndex(curEvent.mousePosition.x + draggedItemHoldOffset.x);

            }


            initFromOutside();
            initFromInside();

            acceptFromOutside();
            acceptFromInside();

            cancelFromOutside();
            cancelFromInsideAndDelete();

            update();


        }

        bool draggingItem;
        bool draggingItemFromInside;

        Item draggedItem;
        Item droppedItem;

        Vector2 draggedItemHoldOffset;

        int insertDraggedItemAtIndex;





        public VInspectorNavbar(EditorWindow window) => this.window = window;

        public EditorWindow window;

    }
}
#endif