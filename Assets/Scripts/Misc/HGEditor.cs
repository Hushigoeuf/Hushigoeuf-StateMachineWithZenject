using UnityEngine;

namespace Hushigoeuf
{
    public static class HGEditor
    {
        public const string COMPONENT_MENU_PATH = nameof(Hushigoeuf) + "/";
        public const string CREATE_ASSET_MENU_PATH = nameof(Hushigoeuf) + "/";

        public static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }

        public static void LogWarning(object message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
        }

        public static void LogError(object message)
        {
#if UNITY_EDITOR
            Debug.LogError(message);
#endif
        }
    }
}