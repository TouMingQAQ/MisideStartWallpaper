#if UNITY_EDITOR
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Reflection;
using UnityEditor;
using Type = System.Type;
using static VInspector.VInspectorState;
using static VInspector.Libs.VUtils;
using static VInspector.Libs.VGUI;



namespace VInspector
{
    static class VInspectorResettableVariables
    {

        public static void ResetButtonGUI(Rect fieldRect, SerializedProperty property, FieldInfo fieldInfo, IEnumerable<object> targets)
        {
            // if (!fieldRect.IsHovered()) return;


            object targetWithDefaultValues = GetTargetWithDefaulValues(targets.First().GetType());

            bool isResetted(object target)
            {
                if (property.isInstantiatedPrefab) return !property.prefabOverride;

                if (targetWithDefaultValues as object == null) return true;


                var currentValue = fieldInfo.GetValue(target);
                var defaultValue = fieldInfo.GetValue(targetWithDefaultValues);

                var isResetted = object.Equals(currentValue, defaultValue);


                if (typeof(Object).IsAssignableFrom(fieldInfo.FieldType))
                    isResetted |= (defaultValue == null) && !(bool)(Object)currentValue;

                if (fieldInfo.FieldType == typeof(string))
                    isResetted |= fieldInfo.FieldType == typeof(string) && (object.Equals(currentValue, "") && object.Equals(defaultValue, null));


                return isResetted;

            }

            if (targets.All(r => isResetted(r))) return;



            var iconSize = 12;
            var colorNormal = Greyscale(.41f);
            var colorHovered = Greyscale(isDarkTheme ? .9f : .0f);
            var colorPressed = Greyscale(isDarkTheme ? .65f : .6f);

            var buttonRect = fieldRect.SetWidthFromRight(20).MoveX(typeof(Object).IsAssignableFrom(fieldInfo.FieldType) ? -18 : 1);

            EditorGUIUtility.AddCursorRect(buttonRect, MouseCursor.CustomCursor);



            if (!IconButton(buttonRect, "CrossIcon", iconSize, colorNormal, colorHovered, colorPressed)) return;

            if (property.isInstantiatedPrefab)
            {
                foreach (var target in property.serializedObject.targetObjects)
                    target.RecordUndo();

                PrefabUtility.RevertPropertyOverride(property, InteractionMode.AutomatedAction);

            }
            else
                property.SetBoxedValue(fieldInfo.GetValue(targetWithDefaultValues));

            GUI.changed = true;

            GUI.FocusControl(null);

        }


        static object GetTargetWithDefaulValues(Type targetType)
        {
            if (targetWithDefaulValues_byType.TryGetValue(targetType, out var cachedResult)) return cachedResult;

            object targetWithDefaultValues = null;

            void script_2023plus()
            {
#if UNITY_2023_2_OR_NEWER

                if (!typeof(MonoBehaviour).IsAssignableFrom(targetType)) return;

                if (Application.isPlaying || TypeCache.GetTypesWithAttribute<ExecuteInEditMode>().Contains(targetType) || TypeCache.GetTypesWithAttribute<ExecuteAlways>().Contains(targetType))
                    if ((targetType.GetMethodInfo("Awake") ?? targetType.GetMethodInfo("OnEnable") ?? targetType.GetMethodInfo("OnDisable") ?? targetType.GetMethodInfo("OnDestroy")) != null) return; // to avoid executing user code


                var tempGo = EditorUtility.CreateGameObjectWithHideFlags("Dummy object for fetching default variable values for vInspector's resettable variables feature", HideFlags.HideAndDontSave, targetType);

                try { targetWithDefaultValues = tempGo.GetComponent(targetType); }

                finally { Object.DestroyImmediate(tempGo); }

#endif
            }
            void script_olderVersions()
            {
#if !UNITY_2023_2_OR_NEWER

                if (!typeof(MonoBehaviour).IsAssignableFrom(targetType)) return;

                targetWithDefaultValues = ScriptableObject.CreateInstance(targetType);

#endif
            }
            void scriptableObject()
            {
                if (!typeof(ScriptableObject).IsAssignableFrom(targetType)) return;

                targetWithDefaultValues = ScriptableObject.CreateInstance(targetType);

            }
            void customClass()
            {
                if (typeof(MonoBehaviour).IsAssignableFrom(targetType)) return;
                if (typeof(ScriptableObject).IsAssignableFrom(targetType)) return;
                if (targetType.GetConstructor(System.Type.EmptyTypes) == null) return;

                targetWithDefaultValues = System.Activator.CreateInstance(targetType);

            }

            script_2023plus();
            script_olderVersions();
            scriptableObject();
            customClass();

            return targetWithDefaulValues_byType[targetType] = targetWithDefaultValues;

        }

        static Dictionary<Type, object> targetWithDefaulValues_byType = new();




        public static bool IsResettable(FieldInfo fieldInfo)
        {
            if (!VInspectorMenu.resettableVariablesEnabled) return false;
            if (Application.isPlaying) return false;

            if (System.Attribute.IsDefined(fieldInfo, typeof(VariantsAttribute))) return false;

            if (typeof(Object).IsAssignableFrom(fieldInfo.FieldType)) return true;
            if (fieldInfo.FieldType == typeof(int)) return true;
            if (fieldInfo.FieldType == typeof(float)) return true;
            if (fieldInfo.FieldType == typeof(double)) return true;
            if (fieldInfo.FieldType == typeof(string)) return true;

            return false;

        }

    }
}
#endif