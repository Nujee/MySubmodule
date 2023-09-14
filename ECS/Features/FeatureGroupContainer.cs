using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.BlackCubeSubmodule.ECS.Features
{
    [System.Serializable]
    public struct FeatureGroupContainer
    {
        [LabelWidth(80f)]
        public SystemType SystemType;

        [HideInInspector]
        public bool ShouldShow => (int)SystemType > 3;
        
        [ShowIf("ShouldShow")]
        public float UpdateInterval;
        
        [ListDrawerSettings(NumberOfItemsPerPage = 40)]
        public FeatureContainer[] Features;
    }
}