using System;

namespace TFrameworkKit.Console.Command
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandAttribute : Attribute
    {
        public string Name;
        public bool IsDebug;
        public CommandAttribute(){}
        public CommandAttribute(string name) : base()
        {
            Name = name;
        }
        public CommandAttribute(bool isDebug):base()
        {
            IsDebug = isDebug;
        }
        public CommandAttribute(string name,bool isDebug):base()
        {
            Name = name;
            IsDebug = isDebug;
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class CommandMethodAttribute : Attribute
    {
        public string Name;
        public bool IsDebug;
        public string NoteStr;
        public CommandMethodAttribute(){}
  
        public CommandMethodAttribute(string name = "",string note = "", bool isDebug = false) : base()
        {
            Name = name;
            IsDebug = isDebug;
            NoteStr = note;
        }
    }
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CommandParameterAttribute : Attribute
    {
        public string Name;
        public string GetTipListFunc;
        public bool UseDefaultTip = true;
        public CommandParameterAttribute(){}

        public CommandParameterAttribute(string name)
        {
            Name = name;
        }
        public CommandParameterAttribute(string name, string getTips) 
        {
            Name = name;
            GetTipListFunc = getTips;
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method,AllowMultiple = true)]
    public class StringToValueAttribute : Attribute
    {
        public Type ValueType;

        public StringToValueAttribute(Type valueType)
        {
            ValueType = valueType;
        }
    }
    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method,AllowMultiple = true)]
    public class ValueTipAttribute : Attribute
    {
        public Type ValueType;

        public ValueTipAttribute(Type valueType)
        {
            ValueType = valueType;
        }
    }
}