using System;
using System.Collections.Generic;
using Code.BlackCubeSubmodule.ECS.Features;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.BlackCubeSubmodule.GameConfigs.AdressablesConfigs
{
    [CreateAssetMenu(fileName = "ECSConfig", menuName = "Game Configs/ECS Config", order = 3)]
    public sealed class ECSConfig : ScriptableObject
    {
        [ListDrawerSettings(ShowFoldout = false, ShowItemCount = false)]
        [SerializeReference]
        [TypeFilter("GetFilteredTypeList")]
        public IEcsInitSystem[] InitSystems = Array.Empty<IEcsInitSystem>();
        
        [PropertySpace(SpaceBefore = 20)]
        [ListDrawerSettings(ShowFoldout = false, ShowItemCount = false)]
        public FeatureGroupContainer[] FeatureGroups;
        
        private readonly List<Type> _types = new List<Type>();

        public IEnumerable<Type> GetFilteredTypeList()
        {
            _types.Clear();
            
            var derivedTypes = TypeCache.GetTypesDerivedFrom(typeof(IEcsInitSystem));
            for (var i = 0; i < derivedTypes.Count; i++)
            {
                if (derivedTypes[i].IsSubclassOf(typeof(Object))
                    || derivedTypes[i].IsAbstract
                    || derivedTypes[i].ContainsGenericParameters
                    || derivedTypes[i].IsClass && derivedTypes[i].GetConstructor(Type.EmptyTypes) == null
                    || derivedTypes[i].IsSubclassOf(typeof(Feature))
                    || typeof(IEcsRunSystem).IsAssignableFrom(derivedTypes[i]))
                {
                    continue;   
                }
                
                _types.Add(derivedTypes[i]);
            }

            return _types;
        }
    }
}