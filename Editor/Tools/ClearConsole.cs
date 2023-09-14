#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;

namespace Code.BlackCubeSubmodule.Editor.Tools
{
    public static class ClearConsole
    {
        // no key modifiers = _n = _anyLetter
        // CTRL = %,
        // SHIFT = #
        // ALT = &
        // %#n = CTRL + SHIFT + n
        [MenuItem("Tools/Clear ClearConsole _n")]
        public static void Clear()
        {
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method?.Invoke(new object(), null);
        }
    }
}
#endif