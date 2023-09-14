using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Code.BlackCubeSubmodule.ECS.Features
{
    [Serializable]
    public sealed class FeatureContainer
    {
        [HorizontalGroup(Width = 0.2f, Gap = 0.1f, LabelWidth = 70f)]
        public bool AddToEcs = true;
        
        [HorizontalGroup(LabelWidth = 110f)]
        [EnableIf("AddToEcs")] 
        [GUIColor("GetInitialStateDropDownColor")]
        public InitialFeatureState InitialFeatureState = InitialFeatureState.Active;
        
        [TypeFilter("GetFilteredTypeList")]
        [LabelWidth(70f)]
        [SerializeReference]
        [EnableIf("AddToEcs")]
        public Feature Feature;
        
        private readonly List<Type> _types = new List<Type>();

        public IEnumerable<Type> GetFilteredTypeList()
        {
            _types.Clear();
            
            var derivedTypes = TypeCache.GetTypesDerivedFrom(typeof(Feature));
            for (var i = 0; i < derivedTypes.Count; i++)
            {
                _types.Add(derivedTypes[i]);
            }

            return _types;
        }
        
        private Color GetInitialStateDropDownColor()
        {
            Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
            return InitialFeatureState == InitialFeatureState.Active ? Color.green : Color.red;
        }
    }
}