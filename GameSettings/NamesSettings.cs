using System;
using System.Collections.Generic;
using Code.MySubmodule.CodeGeneration;
using TriInspector;
using UnityEditor;
using UnityEngine;

namespace Code.MySubmodule.GameSettings
{
    [CreateAssetMenu(fileName = "NamesSettings", menuName = "Game Settings/Names Setup", order = 1)]
    public sealed class NamesSettings : ScriptableObject
    {
        private const string CameraEnumName = "CameraName";
        private const string EffectEnumName = "EffectName";
        private const string NamespaceName = "Code.Game.Constants.GeneratedCode";
        private const string Path = "Assets/Code/Game/Constants/GeneratedCode/";

        [ListDrawerSettings(Draggable = false, HideAddButton = false, HideRemoveButton = false, AlwaysExpanded = true)] 
        [SerializeField] private string[] _camerasNames = Array.Empty<string>();
        [ListDrawerSettings(Draggable = false, HideAddButton = false, HideRemoveButton = false, AlwaysExpanded = true)] 
        [SerializeField] private string[] _effectsNames = Array.Empty<string>();

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void OnAwake()
        {
            var guid = AssetDatabase.FindAssets($"{nameof(NamesSettings)} t:{nameof(NamesSettings)}");
            var namesSetup = AssetDatabase.LoadAssetAtPath<NamesSettings>(AssetDatabase.GUIDToAssetPath(guid[0]));
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