using System;
using UnityEngine;

namespace TFramework.ToolBox
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ToolButtonAttribute : System.Attribute
    {
        public string Name;

        public ToolButtonAttribute()
        {
            Name = string.Empty;
        }
        public ToolButtonAttribute(string name)
        {
            Name = name;
        }
    }
    
    public class SceneAttribute : PropertyAttribute {}
    
}