using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TFrameworkKit.Console.Command
{
    public static class CommandUtility
    {
        
    }

    public struct CommandTip
    {
        public string ShowStr;
        public CommandContainer Command;
        public string InputStr;
    }
    [Serializable]
    public class CommandContainer
    {
        public string CommandName = string.Empty;
        public string MethodName = string.Empty;
        public string CommandNote = string.Empty;
        public List<CommandParameter> Parameters = new();
        public MethodInfo MethodInfo;
        
    }
    [Serializable]
    public class CommandParameter
    {
        public string Name = string.Empty;
        public bool IsDySize = false;
        public Type ValueType = null;
        public Func<IEnumerable<string>> GetList = null;
    }
    
}