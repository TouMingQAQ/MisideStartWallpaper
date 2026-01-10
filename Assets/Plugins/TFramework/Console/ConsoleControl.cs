using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using TFrameworkKit.Console.Command;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace TFramework.Console
{


    public static class ConsoleControl
    {
        public static List<CommandContainer> cmdList = new();
        public static Dictionary<string, CommandContainer> cmdMap = new();
        public static Dictionary<Type, MethodInfo> parseMap = new();
        public static Dictionary<Type, MethodInfo> valueTipMap = new();

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#else
        [RuntimeInitializeOnLoadMethod]
#endif
        public static void Init()
        {
            BakeStringToValue();
            BakeCommand();
        }
        /// <summary>
        /// 烘焙转换函数
        /// </summary>
        static void BakeStringToValue()
        {
            parseMap.Clear();
            Debug.Log("BakeStringToValue");
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            
            foreach (var type in types)
            {
                var methods = type.GetMethods();
                var methodInfos = methods.Where(info => Attribute.IsDefined(info, typeof(StringToValueAttribute)));
                foreach (var methodInfo in methodInfos)
                {
                    var ats = methodInfo.GetCustomAttributes<StringToValueAttribute>();
                    foreach (var attribute in ats)
                    {
                        var valueType = attribute.ValueType;
                        parseMap[valueType] = methodInfo;
                    }
                }
                var properties = type.GetProperties();
                var propertyInfos = properties.Where(info => Attribute.IsDefined(info, typeof(StringToValueAttribute)));
                foreach (var propertyInfo in propertyInfos)
                {
                    var ats = propertyInfo.GetCustomAttributes<StringToValueAttribute>();
                    foreach (var attribute in ats)
                    {
                        var valueType = attribute.ValueType;
                        parseMap[valueType] = propertyInfo.GetMethod;
                    }
                }
            }
        }
        /// <summary>
        /// 烘焙指令集
        /// </summary>
        static void BakeCommand()
        {
            cmdList.Clear();
            cmdMap.Clear();
            Debug.Log("BakeCommand");
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new();
            foreach (var assembly in assemblies)
            {
                types.AddRange(assembly.GetTypes());
            }
            var commandList = types
                .Where(t => Attribute.IsDefined(t, typeof(CommandAttribute)));
            foreach (var type in commandList)
            {
                var commandAttribute = type.GetCustomAttribute<CommandAttribute>();
                if(commandAttribute is null)
                    continue;
                if(!Debug.isDebugBuild && commandAttribute.IsDebug)
                    continue;
                var methods = type.GetMethods();
                foreach (var method in methods)
                {
                    var commandMethodAttribute = method.GetCustomAttribute<CommandMethodAttribute>();
                    if(commandMethodAttribute is null)
                        continue;
                    if(!Debug.isDebugBuild && commandMethodAttribute.IsDebug)
                        continue;
                    CommandContainer container = new CommandContainer();
                    var cmdName = string.IsNullOrEmpty(commandAttribute.Name) ? type.Name : commandAttribute.Name;
                    var methodName = string.IsNullOrEmpty(commandMethodAttribute.Name) ? method.Name : commandMethodAttribute.Name;
                    container.CommandName = cmdName;
                    container.MethodName = methodName;
                    container.CommandNote = commandMethodAttribute.NoteStr;
                    container.MethodInfo = method;
                    var parameters = method.GetParameters();
                    foreach (var parameterInfo in parameters)
                    {
                        CommandParameter parameter = new CommandParameter();
                        var parameterAttribute = parameterInfo.GetCustomAttribute<CommandParameterAttribute>();
                        if (parameterAttribute is not null)
                        {
                            parameter.Name = parameterAttribute.Name;
                            var parameterType = parameterInfo.ParameterType;
                            var getTipFunc = parameterAttribute.GetTipListFunc;
                            if (!string.IsNullOrEmpty(getTipFunc))
                            {
                                var getListMethod =
                                    methods.FirstOrDefault(_ => _.Name == parameterAttribute.GetTipListFunc);
                                if(getListMethod != default)
                                    parameter.GetList = Delegate.CreateDelegate(typeof(Func<IEnumerable<string>>),getListMethod) as Func<IEnumerable<string>>;
                            }
                            else if(parameterAttribute.UseDefaultTip)
                            {
                                if(valueTipMap.TryGetValue(parameterType,out var methodInfo))
                                    parameter.GetList = Delegate.CreateDelegate(typeof(Func<IEnumerable<string>>),methodInfo) as Func<IEnumerable<string>>;
                            }
                           
                        }

                        parameter.ValueType = parameterInfo.ParameterType;
                        parameter.Name ??= parameterInfo.Name;
                        if (parameterInfo.IsDefined(typeof(ParamArrayAttribute), false))
                        {
                            parameter.ValueType = parameterInfo.ParameterType.GetElementType();
                            parameter.IsDySize = true;
                        }
                        container.Parameters.Add(parameter);
                    }
                    cmdMap[$"/{container.CommandName} /{container.MethodName}"] = container;
                }
            }
            cmdList.AddRange(cmdMap.Values.ToList());
        }
        public static void CommandTipList(string command,in HashSet<CommandTip> tipList)
        {
            tipList.Clear();
            if (!command.StartsWith("/"))
                return;

            #region OldMethod

            // // 定义正则表达式，匹配 "/类名" 格式的指令
            // string patternClass = @"^/(?<class>\w+)$";
            // // 定义正则表达式，匹配 "/类名 /方法名" 格式的指令
            // string patternMethod = @"^/(?<class>\w+)(\s+/(?<method>\w+))$";            
            // // // 定义正则表达式，匹配 "/类名 /方法名 #参数名" 格式的指令
            // // string pattern3 = @"^/(?<class>\w+)(\s+/(?<method>\w+))(\s+#(?<paramName>\w+))$";
            // // // 定义正则表达式，匹配 "/类名 /方法名 #参数名 参数值" 格式的指令
            // // string pattern4 = @"^/(?<class>\w+)(\s+/(?<method>\w+))(\s+#(?<paramName>\w+)\s+(?<paramValue>[\S\s]+?))?$";
            // // 收集四种匹配的结果
            // Match matchClass = Regex.Match(command, patternClass);
            // Match matchMethod = Regex.Match(command, patternMethod);
            // if(matchMethod.Success)
            // {
            //     var className = matchMethod.Groups["class"].Value;
            //     var methodName = matchMethod.Groups["method"].Value;
            //     var containerList = cmdList.FindAll((c => c.CommandName.StartsWith(className) && c.MethodName.Contains(methodName,StringComparison.OrdinalIgnoreCase)));
            //     if(!containerList.Any())
            //         return;
            //     foreach (var container in containerList)
            //     {
            //         tipList.Add($"/{container.CommandName} /{container.MethodName}");
            //     }
            // }
            // else if (matchClass.Success)
            // {
            //     var className = matchClass.Groups["class"].Value;
            //     var containerList = cmdList.FindAll((c => c.CommandName.Contains(className,StringComparison.OrdinalIgnoreCase)));
            //     if(!containerList.Any())
            //         return;
            //     foreach (var container in containerList)
            //     {
            //         tipList.Add($"/{container.CommandName}");
            //     }
            // }

            #endregion

            #region New

            tipList.Clear();
            foreach (var (cmdKey,cmdValue) in cmdMap)
            {
                if (cmdKey.Contains(command))
                {
                    StringBuilder str = new StringBuilder();
                    str.Append(cmdKey);
                    CommandTip tip = new();
                    tip.InputStr = str.ToString();
                    str = str.Replace(command, $"<color=green>{command}</color>");

                    foreach (var cmdValueParameter in cmdValue.Parameters)
                    {
                        str.Append($" #{cmdValueParameter.Name}({cmdValueParameter.ValueType})");
                    }
                    
                    if (!string.IsNullOrEmpty(cmdValue.CommandNote))
                        str.Append($" <color=#66ccff>[{cmdValue.CommandNote}]</color>");
                    tip.ShowStr = str.ToString();
                    tip.Command = cmdValue;
                    tipList.Add(tip);
                }
            }

            #endregion
           
    

            
        }

        private static List<object> parameters = new();
        private static Dictionary<string, string> parameterMap = new();
        /// <summary>
        /// 执行指令
        /// sample: /ClassName /MethodName #paramName paramValue
        /// </summary>
        /// <param name="command"></param>
        public static void ExecuteCommand(string command)
        {
            if (!command.StartsWith("/"))
            {
                Debug.LogError("指令格式错误");
                return;
            }
            // 定义正则表达式，匹配类名、方法名和参数名（# 开头的参数）及其单个值
            Debug.Log($"ExecuteCommand:<color=green>{command}</color>");
            string pattern =  @"^/(?<class>\w+)\s+/(?<method>\w+)(\s+#(?<paramName>\w+)\s+(?<paramValue>[\S\s]+?))*$";
            string paramPattern = @"#([A-Za-z]+)\s+(\S+)";
            Match match = Regex.Match(command, pattern);
            MatchCollection paramMatches = Regex.Matches(command, paramPattern);
            if (!match.Success)
            {
                Debug.LogError("无法解析指令");
                return;
            }
            // 提取类名和方法名
            string cmdName = match.Groups["class"].Value;
            string cmdMethod = match.Groups["method"].Value;
            if (string.IsNullOrEmpty(cmdName) || string.IsNullOrEmpty(cmdMethod))
            {
                Debug.LogError("指令为空，解析失败");
                return;
            }

            string title = $"/{cmdName} /{cmdMethod}";

            if (!cmdMap.TryGetValue(title, out var container))
            {
                Debug.LogWarning($"[{title}]指令集为空");
                return;
            }

            if (container.Parameters.Count <= 0)
            {
                //无参函数，直接执行
                container.MethodInfo.Invoke(null, null);
                return;
            }
            // 提取参数
            parameterMap.Clear();
            foreach (Match paramMatch in paramMatches)
            {
                string paramName = paramMatch.Groups[1].Value;
                string paramValue = paramMatch.Groups[2].Value;

                // 将参数名和值存入字典
                parameterMap[paramName] = paramValue;
            }
            if (parameterMap.Count != container.Parameters.Count)
            {
                Debug.LogError($"[{title}]参数异常");
                return;
            }
            parameters.Clear();
            foreach (var commandParameter in container.Parameters)
            {
                if(!parameterMap.TryGetValue(commandParameter.Name,out var pValue))
                    return;//有不对应的参数和名称
               
                if (commandParameter.IsDySize)
                {
                    var strList = pValue.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    Array extra = Array.CreateInstance(commandParameter.ValueType,strList.Length);
                    for (int i = 0,length = strList.Length; i < length; i++)
                    {
                        var str = strList[i];
                        if(!TryCreatObject(str, commandParameter.ValueType,out object obj))
                            return;//转换失败
                        extra.SetValue(obj,i);
                    }
                    parameters.Add(extra);
                }
                else
                {
                    if(!TryCreatObject(pValue, commandParameter.ValueType,out object obj))
                        return;//转换失败
                    parameters.Add(obj);
                }
            }
            try
            {
                container.MethodInfo.Invoke(null, parameters.ToArray());
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[{title}]指令执行失败");
            }
        }
        static bool TryCreatObject(string value, Type valueType,out object obj,bool isDySize = false)
        {
            obj = null;
            if (!parseMap.TryGetValue(valueType, out var methodInfo))
                return false;
            try
            {
                obj = methodInfo.Invoke(null, new object[]{value});
            }
            catch (Exception e)
            {
                return false;
            }
                
            return true;
        }
    }
}