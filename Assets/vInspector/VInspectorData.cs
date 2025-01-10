#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using System.Reflection;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using Type = System.Type;
using static VInspector.VInspectorState;
using static VInspector.Libs.VUtils;
using static VInspector.Libs.VGUI;



namespace VInspector
{
    public class VInspectorData : ScriptableObject, ISerializationCallbackReceiver
    {

        public List<Item> items = new();

        [System.Serializable]
        public class Item
        {
            public GlobalID globalId;


            public Type type => Type.GetType(_typeString) ?? typeof(DefaultAsset);
            public string _typeString;

            public Object obj
            {
                get
                {
                    if (_obj == null && !isSceneGameObject)
                        _obj = globalId.GetObject();

                    return _obj;

                    // updating scene objects here using globalId.GetObject() could cause performance issues on large scenes
                    // so instead they are batch updated in VInspector.UpdateBookmarkedObjectsForScene()

                }
            }
            public Object _obj;


            public bool isSceneGameObject;
            public bool isAsset;


            public bool isLoadable => obj != null;

            public bool isDeleted
            {
                get
                {
                    if (!isSceneGameObject)
                        return !isLoadable;

                    if (isLoadable)
                        return false;

                    if (!AssetDatabase.LoadAssetAtPath<SceneAsset>(globalId.guid.ToPath()))
                        return true;

                    for (int i = 0; i < EditorSceneManager.sceneCount; i++)
                        if (EditorSceneManager.GetSceneAt(i).path == globalId.guid.ToPath())
                            return true;

                    return false;

                }
            }

            public string assetPath => globalId.guid.ToPath();


            public Item(Object o)
            {
                globalId = o.GetGlobalID();

                id = Random.value.GetHashCode();

                isSceneGameObject = o is GameObject go && go.scene.rootCount != 0;
                isAsset = !isSceneGameObject;

                _typeString = o.GetType().AssemblyQualifiedName;

                _name = o.name;

                _obj = o;

            }



            public float width => VInspectorNavbar.expandedItemWidth;




            public string name
            {
                get
                {
                    if (!isLoadable) return _name;

                    if (assetPath.GetExtension() == ".cs")
                        _name = obj.name.Decamelcase();
                    else
                        _name = obj.name;

                    return _name;

                }
            }
            public string _name { get => state._name; set => state._name = value; }

            public string sceneGameObjectIconName { get => state.sceneGameObjectIconName; set => state.sceneGameObjectIconName = value; }



            public ItemState state
            {
                get
                {
                    if (!VInspectorState.instance.itemStates_byItemId.ContainsKey(id))
                        VInspectorState.instance.itemStates_byItemId[id] = new ItemState();

                    return VInspectorState.instance.itemStates_byItemId[id];

                }
            }

            public int id;

        }



        public void OnAfterDeserialize() => VInspectorNavbar.repaintNeededAfterUndoRedo = true;
        public void OnBeforeSerialize() { }









        [CustomEditor(typeof(VInspectorData))]
        class Editor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {
                var style = EditorStyles.label;
                style.wordWrap = true;


                SetGUIEnabled(false);
                BeginIndent(0);

                Space(10);
                EditorGUILayout.LabelField("This file stores bookmarks from vInspector's navigation bar", style);

                EndIndent(10);
                ResetGUIEnabled();

                // Space(15);
                // base.OnInspectorGUI();

            }
        }


    }
}
#endif