using System;
using System.Collections.Generic;
using Code.BlackCubeSubmodule.CodeGeneration;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Code.BlackCubeSubmodule.GameConfigs.AdressablesConfigs
{
    [CreateAssetMenu(fileName = "NamesConfig", menuName = "Game Configs/Names Setup", order = 1)]
    public sealed class NamesConfig : ScriptableObject
    {
        private const string CameraEnumName = "CameraName";
        private const string EffectEnumName = "EffectName";
        private const string NamespaceName = "Code.Game.Constants.GeneratedCode";
        private const string Path = "Assets/Code/Game/Constants/GeneratedCode/";

        [ListDrawerSettings(DefaultExpandedState = true, DraggableItems = false, HideRemoveButton = false)] 
        [SerializeField] private string[] _camerasNames = Array.Empty<string>();
        [ListDrawerSettings(DefaultExpandedState = true, DraggableItems = false, HideRemoveButton = false)]  
        [SerializeField] private string[] _effectsNames = Array.Empty<string>();

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void OnAwake()
        {
            var guid = AssetDatabase.FindAssets($"{nameof(NamesConfig)} t:{nameof(NamesConfig)}");
            var namesSetup = AssetDatabase.LoadAssetAtPath<NamesConfig>(AssetDatabase.GUIDToAssetPath(guid[0]));
            namesSetup.ApplyChanges();
        }

        [Button]
        private void ApplyChanges()
        {
            var camerasDictionary = new Dictionary<int, string>(_camerasNames.Length);
            for (int i = 0; i < _camerasNames.Length; i++)
            {
                camerasDictionary.Add(i, _camerasNames[i]);
            }
            
            var effectsDictionary = new Dictionary<int, string>(_effectsNames.Length);
            for (int i = 0; i < _effectsNames.Length; i++)
            {
                effectsDictionary.Add(i, _effectsNames[i]);
            }
            
            EnumBuilder.Build(CameraEnumName, NamespaceName, Path, camerasDictionary);
            EnumBuilder.Build(EffectEnumName, NamespaceName, Path, effectsDictionary);
            
            AssetDatabase.Refresh();
        }
#endif
    }
}