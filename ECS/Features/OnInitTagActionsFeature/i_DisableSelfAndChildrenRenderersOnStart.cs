using Leopotam.EcsLite;
using UnityEngine;

namespace Code.MySubmodule.ECS.Features.OnInitTagActionsFeature
{
    public sealed class i_DisableSelfAndChildrenRenderersOnStart : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var objectsToDisable = GameObject.FindGameObjectsWithTag("DisableSelfAndChildrenRenderersOnStart");
            for (var i = 0; i < objectsToDisable.Length; i++)
            {
                var renderers = objectsToDisable[i].GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    renderer.enabled = false;
                }
            }
        }
    }
}