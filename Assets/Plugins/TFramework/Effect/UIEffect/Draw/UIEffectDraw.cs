#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace TFramework.UIEffect
{
    [CustomEditor(typeof(UIEffect))]
    public class UIEffectDraw : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var effect = target as UIEffect;
            if(effect == null)
                return;
            if (effect.graphic == null || effect.graphic.material == null)
            {
                return;
            }

            var path = AssetDatabase.GetAssetPath(effect.graphic.material);
            if (string.IsNullOrEmpty(path))
            {
                EditorGUILayout.HelpBox("材质路径为空:" + path, MessageType.Warning);
                if (GUILayout.Button("保存材质"))
                {
                    var material = new Material(effect.graphic.material);
                    path = EditorUtility.SaveFilePanel("保存材质", EditorPrefs.GetString("LastUIEffectSaveMaterialPath",Application.dataPath),"NewMaterial.mat","mat");
                    if (!string.IsNullOrEmpty(path))
                    {
                        EditorPrefs.SetString("LastUIEffectSaveMaterialPath",path);
                        path = path.Replace(Application.dataPath,"Assets");
                        AssetDatabase.CreateAsset(material, path);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        effect.graphic.material = material;
                    }
                  
                }

            }
            else
            {
                EditorGUILayout.HelpBox("Material Path:" + path, MessageType.Info);
            }
        }
    }
}
#endif