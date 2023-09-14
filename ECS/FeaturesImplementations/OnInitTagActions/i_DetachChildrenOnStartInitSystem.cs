using Code.Game.Constants.GeneratedCode;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.BlackCubeSubmodule.ECS.OnInitTagActionsFeature
{
    public sealed class i_DetachChildrenOnStartInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var parents = GameObject.FindGameObjectsWithTag(Tags.DetachChildrenOnStart.ToString());
            for (var i = 0; i < parents.Length; i++)
            {
                for (var j = parents[i].transform.childCount - 1; j > -1; j--)
                {
                    parents[i].transform.GetChild(j).parent = null;
                }
            }
        }
    }
}