using System.Collections.Generic;
using UnityEngine;

namespace TFrameworkKit.Console.Command
{
    public class DefaultValue
    {
        [ValueTip(typeof(bool))]
        public static IEnumerable<string> Boolean => new[]
        {
            "true", 
            "false"
        };
        [ValueTip(typeof(Color))]
        public static IEnumerable<string> Color => new[]
        {
            "Color.red",
            "Color.yellow",
            "Color.blue",
            "Color.green",
            "Color.white",
            "Color.black" ,
            "Color.gray",
            "Color.cyan",
            "Color.magenta",
            "Color.clear",
        };
        [ValueTip(typeof(Vector2))]
        public static IEnumerable<string> Vector2 => new[]
        {
            "Vector2.up",
            "Vector2.down",
            "Vector2.left",
            "Vector2.right",
            "Vector2.one",
            "Vector2.zero",
        };
        [ValueTip(typeof(Vector3))]
        public static IEnumerable<string> Vector3 => new[]
        {
            "Vector3.up",
            "Vector3.down",
            "Vector3.left",
            "Vector3.right",
            "Vector3.forward",
            "Vector3.back",
            "Vector3.one",
            "Vector3.zero",
        };
        [ValueTip(typeof(Vector4))]
        public static IEnumerable<string> Vector4 => new[]
        {
            "Vector4.zero",
            "Vector4.one",
        };

    }
}