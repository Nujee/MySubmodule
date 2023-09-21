using TriInspector;
using UnityEngine;

namespace Code.MySubmodule.ECS.Features
{
    [System.Serializable]
    public struct FeatureContainer
    {
        public bool IsActive;

        [SerializeReference, SerializeReferenceButton]
        [LabelWidth(60)] [EnableIf("IsActive")]
        public IFeature Feature;
    }
}