using System;
using UnityEngine;

namespace TFrameworkKit.Console.Command
{
    public class SystemValue
    {
        [StringToValue(typeof(String))]
        public static object ParseString(string value) => value;

        public static object ParseGameObject(string value)
        {
           return GameObject.Find(value);
        }
        
        [StringToValue(typeof(Boolean))]
        public static object ParseBool(string value)
        {
            if (bool.TryParse(value, out var result))
            {
                return result;
            }
            if (int.TryParse(value,out int intRes))
            {
                return intRes != 0;
            }

            return null;
        }
        [StringToValue(typeof(Int16))]
        public static object ParseInt16(string value)
        {
            if (Int16.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }

        [StringToValue(typeof(UInt16))]
        public static object ParseUInt16(string value)
        {
            if (UInt16.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }

        [StringToValue(typeof(Int32))]
        public static object ParseInt32(string value)
        {
            if (Int32.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }

        [StringToValue(typeof(UInt32))]
        public static object ParseUInt32(string value)
        {
            if (UInt32.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }

        [StringToValue(typeof(Int64))]
        public static object ParseInt64(string value)
        {
            if (Int64.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }

        [StringToValue(typeof(UInt64))]
        public static object ParseUInt64(string value)
        {
            if (UInt64.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }

        [StringToValue(typeof(Single))]
        public static object ParseSingle(string value)
        {
            if (Single.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }

        [StringToValue(typeof(Double))]
        public static object ParseDouble(string value)
        {
            if (Double.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }

        [StringToValue(typeof(Decimal))]
        public static object ParseDecimal(string value)
        {
            if (Decimal.TryParse(value, out var result))
            {
                return result;
            }
            return null;
        }
    }
    public class UnityValue
    {
        public static Vector3 GetVector3(string vName)
        {
            return vName switch
            {
                "Vector3.up" => Vector3.up,
                "Vector3.down" => Vector3.down,
                "Vector3.left" => Vector3.left,
                "Vector3.right" => Vector3.right,
                "Vector3.forward" => Vector3.forward,
                "Vector3.back" => Vector3.back,
                "Vector3.one" => Vector3.one,
                "Vector3.zero" => Vector3.zero,
                _ => Vector3.zero // 默认返回 Vector3.zero
            };
        }

        public static Vector2 GetVector2(string vName)
        {
            return vName switch
            {
                "Vector2.up" => Vector2.up,
                "Vector2.down" => Vector2.down,
                "Vector2.left" => Vector2.left,
                "Vector2.right" => Vector2.right,
                "Vector2.one" => Vector2.one,
                "Vector2.zero" => Vector2.zero,
                _ => Vector2.zero // 默认返回 Vector2.zero
            };
        }
        public static Vector4 GetVector4(string vName)
        {
            return vName switch
            {
                "Vector4.zero" => Vector4.zero,
                "Vector4.one" => Vector4.one,
                _ => Vector4.one
            };
        }
        [StringToValue(typeof(Vector2))]
        public static object ParseVector2(string value)
        {
            Vector2 v = default;
            var numStr = value.Split(",");
            if (numStr.Length <= 1)
                return GetVector2(value);
            for (var i = 0; i < numStr.Length; i++)
            {
                if (!float.TryParse(numStr[i], out float fv))
                    return default;
                switch (i)
                {
                    case 0:
                        v.x = fv;
                        break;
                    case 1:
                        v.y = fv;
                        break;
                }
            }
            return v;
        }
        [StringToValue(typeof(Vector3))]
        public static object ParseVector3(string value)
        {
            Vector3 v = default;
            var numStr = value.Split(",");
            if (numStr.Length <= 1)
                return GetVector3(value);
            for (var i = 0; i < numStr.Length; i++)
            {
                if (!float.TryParse(numStr[i], out float fv))
                    return default;
                switch (i)
                {
                    case 0:
                        v.x = fv;
                        break;
                    case 1:
                        v.y = fv;
                        break;
                    case 2:
                        v.z = fv;
                        break;
                }
            }
            return v;
        }
        [StringToValue(typeof(Vector4))]
        public static object ParseVector4(string value)
        {
            Vector4 v = default;
            var numStr = value.Split(",");
            if (numStr.Length <= 1)
                return GetVector4(value);
            for (var i = 0; i < numStr.Length; i++)
            {
                if (!float.TryParse(numStr[i], out float fv))
                    return v;
                switch (i)
                {
                    case 0:
                        v.x = fv;
                        break;
                    case 1:
                        v.y = fv;
                        break;
                    case 2:
                        v.z = fv;
                        break;
                    case 3:
                        v.w = fv;
                        break;
                }
            }
            return v;
        }

        public static Color GetColor(string colorName)
        {
            return colorName switch
            {
                "Color.red" => Color.red,
                "Color.yellow" => Color.yellow,
                "Color.blue" => Color.blue,
                "Color.green" => Color.green,
                "Color.white" => Color.white,
                "Color.black" => Color.black,
                "Color.gray" => Color.gray,
                "Color.cyan" => Color.cyan,
                "Color.magenta" => Color.magenta,
                "Color.clear"=> Color.clear,
                _ => Color.white
            };
        }
        [StringToValue(typeof(Color))]
        [StringToValue(typeof(Color32))]
        public static object ParseColor(string value)
        {
            Color color = Color.white;
            var numStr = value.Split(",");
            if (value.StartsWith("#")) //#66CCFF
            {
                ColorUtility.TryParseHtmlString(value, out color);
            }
            else if(numStr.Length > 1) // 1,1,1,1
            {
                for (var i = 0; i < numStr.Length; i++)
                {
                    if (!float.TryParse(numStr[i], out float fv))
                        return color;
                    switch (i)
                    {
                        case 0:
                            color.r = fv;
                            break;
                        case 1:
                            color.g = fv;
                            break;
                        case 2:
                            color.b = fv;
                            break;
                        case 3:
                            color.a = fv;
                            break;
                    }
                }
            }
            else
            {
                color = GetColor(value);
            }

            return color;
        }
    }
}