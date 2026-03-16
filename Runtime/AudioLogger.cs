using UnityEngine;

namespace CuteEngine.Audio
{
    public static class AudioLogger
    {
        public static void Log(string message)
        {
            Debug.Log($"<color=orange>[AudioSystem]</color> {message}");
        }

        public static void LogWarning(string message)
        {
            Debug.LogWarning($"<color=orange>[AudioSystem]</color> {message}");
        }

        public static void LogError(string message)
        {
            Debug.LogError($"<color=orange>[AudioSystem]</color> {message}");
        }
    }
}
