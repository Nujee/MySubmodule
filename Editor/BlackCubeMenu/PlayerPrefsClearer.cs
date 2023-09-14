#if UNITY_EDITOR
using Code.BlackCubeSubmodule.DebugTools.BlackCubeLogger;
using Code.BlackCubeSubmodule.Utility.Constants;
using UnityEditor;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Editor.BlackCubeMenu
{
    public static class PlayerPrefsClearer
    {
        [MenuItem("BlackCube/Clear All Player Prefs", priority = 0)]
        public static void Clear()
        {
            PlayerPrefs.DeleteAll();
            
            $"{Names.Submodule}: Player prefs have been cleared.".Log();
        }
    }
}
#endif
