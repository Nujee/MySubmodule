#if UNITY_EDITOR
using System.Linq;
using Code.BlackCubeSubmodule.CodeGeneration;
using UnityEditor;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Editor.Tools
{
    public static class LayersClassBuilderMenuItem
    {
        private const string LayerTypeName = "Layers";
        private const string TagsTypeName = "Tags";
        private const string NamespaceName = "Code.Game.Constants.GeneratedCode";
        private const string Path = "Assets/Code/Game/Constants/GeneratedCode/";
        
        [MenuItem("Tools/Update Layers and Tags Enums")]
        public static void UpdateLayersAndTagsEnums()
        {
            UpdateLayersClass();
            UpdateTagsClass();
        }

        public static void UpdateLayersClass()
        {
            var layers = Enumerable.Range(0, 31)
                .Select(LayerMask.LayerToName)
                .Where(str => !string.IsNullOrEmpty(str))
                .ToDictionary(LayerMask.NameToLayer);
            
            EnumBuilder.Build(LayerTypeName, NamespaceName, Path, layers);
            AssetDatabase.Refresh();
        }
        
        private static void UpdateTagsClass()
        {
            var index = 0;
            var tags = UnityEditorInternal.InternalEditorUtility.tags.ToDictionary(_ => index++);
            
            EnumBuilder.Build(TagsTypeName, NamespaceName, Path, tags);
            AssetDatabase.Refresh();
        }
    }
}
#endif