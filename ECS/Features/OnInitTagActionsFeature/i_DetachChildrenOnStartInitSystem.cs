using Leopotam.EcsLite;
using UnityEngine;

namespace Code.MySubmodule.ECS.Features.OnInitTagActionsFeature
{
    public sealed class i_DetachChildrenOnStartInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var parents = GameObject.FindGameObjectsWithTag("DetachChildrenOnStart");
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