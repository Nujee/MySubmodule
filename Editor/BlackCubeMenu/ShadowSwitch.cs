#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Code.BlackCubeSubmodule.Editor.BlackCubeMenu
{
    public static class ShadowSwitch
    {
        [MenuItem("BlackCube/Shadow off", priority = 1)]
        public static void ShadowsOff()
        {
            var meshRenderers = Object.FindObjectsOfType<MeshRenderer>();

            var counter = 0;
            foreach (var meshRenderer in meshRenderers)
            {
                if (meshRenderer.shadowCastingMode != ShadowCastingMode.Off) counter++;
                meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
                meshRenderer.receiveShadows = false;
            }
            
            Debug.Log($"BlackCube: Disabling shadows. Disabled on {counter} renderers.");
        }

        [MenuItem("BlackCube/Shadow on", priority = 2)]
        public static void Undo()
        {
            var meshRenderers = Object.FindObjectsOfType<MeshRenderer>();

            var counter = 0;
            foreach (var meshRenderer in meshRenderers)
            {
                counter++;
                meshRenderer.shadowCastingMode = ShadowCastingMode.On;
                meshRenderer.receiveShadows = true;
            }
            
            Debug.Log($"BlackCube: Enabling shadows. Enabled on {counter} renderers.");
        }
    }
}
#endif


