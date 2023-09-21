using TriInspector;
using UnityEngine;

namespace Code.MySubmodule.ECS.Features
{
    [System.Serializable]
    public struct FeatureGroupContainer
    {
        public SystemType SystemType;

        [HideInInspector]
        public bool ShouldShow => (int)SystemType > 3;
        
        [ShowIf("ShouldShow")]
        public float UpdateInterval;
        
        public FeatureContainer[] Features;
    }
}