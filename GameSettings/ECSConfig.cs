using Code.MySubmodule.ECS.Features;
using Leopotam.EcsLite;
using TriInspector;
using UnityEngine;

namespace Code.MySubmodule.GameSettings
{
    [CreateAssetMenu(fileName = "ECSConfig", menuName = "Game Settings/ECS Config", order = 3)]
    public sealed class ECSConfig : ScriptableObject
    {
        [Title("Init Systems")] [HideLabel] [LabelWidth(1)] 
        [SerializeReference, SerializeReferenceButton]
        public IEcsInitSystem[] InitSystems;

        [Title("Features")] [HideLabel]
        public FeatureGroupContainer[] FeatureGroups;
    }
}