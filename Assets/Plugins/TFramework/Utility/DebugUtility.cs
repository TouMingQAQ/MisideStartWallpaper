using UnityEngine;

namespace TFramework.Utility
{
    public static class DebugUtility
    {
        public static int LogLevel = 0;
        
        public static void LogI(string tag, string message,int logLevel = 0)
        {
            if(!CanLog(logLevel))
                return;
            Debug.Log($"[{tag}]  {message}");
        }
        public static void LogW(string tag, string message,int logLevel = 0)
        {
            if(!CanLog(logLevel))
                return;
            Debug.LogWarning($"[{tag}]  {message}");
        }
        public static void LogE(string tag, string message,int logLevel = 0)
        {
            if(!CanLog(logLevel))
                return;
            Debug.LogError($"[{tag}]  {message}");
        }

        static bool CanLog(int logLevel) => logLevel >= LogLevel;
    }
}