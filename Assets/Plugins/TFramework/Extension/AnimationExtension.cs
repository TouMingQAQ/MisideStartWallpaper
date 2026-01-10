using System.Collections.Generic;

#if  UNITY_EDITOR
using UnityEditor;

#endif
using UnityEngine;

namespace TFramework.Extension
{
    public static class AnimationExtension
    {
#if  UNITY_EDITOR

        public static void Inverted(this AnimationClip clip)
        {
            var bindings = AnimationUtility.GetCurveBindings(clip);
            var totalDuration = clip.length;
            List<Keyframe> keyframes = new();
            foreach (var binding in bindings)
            {
                keyframes.Clear();
                var editorCurve = AnimationUtility.GetEditorCurve(clip, binding);
                if(editorCurve == null)
                    continue;
                keyframes.AddRange(editorCurve.keys);
                editorCurve.ClearKeys();
                for (int i = keyframes.Count-1; i >= 0; i--)
                {
                    var key = keyframes[i];
                    key.time = totalDuration - key.time;
                    (key.inWeight, key.outWeight) = (key.outWeight, key.inWeight);
                    (key.inTangent, key.outTangent) = (-key.outTangent, -key.inTangent);
                    editorCurve.AddKey(key);
                }
                AnimationUtility.SetEditorCurve(clip,binding,editorCurve);
            }
        }
        [UnityEditor.MenuItem("CONTEXT/AnimationClip/Inverted")]
        public static void Inverted()
        {
            var clip = Selection.activeObject as AnimationClip;
            if (clip == null)
                return;
            clip.Inverted();
            EditorUtility.SetDirty(clip);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
    }
}

