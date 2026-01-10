using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace TFramework.UIEffect
{
    [DisallowMultipleComponent]
    public sealed class UIEffect : MonoBehaviour
    {
        public Graphic graphic;
#if UNITY_EDITOR
        private void Reset()
        {
            TryGetComponent(out graphic);
            if(graphic == null)
                return;
            if(graphic.material != graphic.defaultMaterial)
                return;
            var shader = Shader.Find("TFramework/UIEffect");
            if(shader == null)
                return;
            var material = new Material(shader);
            graphic.material = new Material(material);
        }
#endif
    }
}
