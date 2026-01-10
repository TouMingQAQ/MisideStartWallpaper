using System;
using UnityEngine;

namespace TFramework.ToolBox
{
    [Serializable]
    public class ReferenceValue<T> where T : struct
    {
        private T _value = default;
        public Action<T, T> onValueChange;
        public T Value
        {
            get => _value;
            set
            {
                onValueChange?.Invoke(_value,value);
                _value = value;
            }
        }

        public ReferenceValue() { }
        public ReferenceValue(T value) : base()
        {
            _value = value;
        }
        public ReferenceValue(Action<T, T> onValueChange) : base()
        {
            this.onValueChange = onValueChange;
        }
        public ReferenceValue(T value, Action<T, T> onValueChange) : base()
        {
            _value = value;
            this.onValueChange = onValueChange;
        }

        public void Copy(in ReferenceValue<T> target)
        {
            Value = target.Value;
        }

        public static bool operator ==(ReferenceValue<T> a, ReferenceValue<T> b)
        {
            if (a == null || b == null)
                return false;
            return a.Value.Equals(b.Value);
        }

        public static bool operator !=(ReferenceValue<T> a, ReferenceValue<T> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is not ReferenceValue<T> value)
                return false;
            return _value.Equals(value._value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}