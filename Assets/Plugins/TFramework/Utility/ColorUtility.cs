

using UnityEngine;

namespace TFramework.Utility
{
    public static class ColorUtility
    {
        public static string Color(this string message, Color color)
        {
            return $"<color={UnityEngine.ColorUtility.ToHtmlStringRGB(color)}>{message}</color>";
        }
    }
}