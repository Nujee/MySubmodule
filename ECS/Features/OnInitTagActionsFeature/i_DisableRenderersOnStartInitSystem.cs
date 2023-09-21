using Leopotam.EcsLite;
using UnityEngine;

namespace Code.MySubmodule.ECS.Features.OnInitTagActionsFeature
{
    public sealed class i_DisableRenderersOnStartInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var objectsToDisable = GameObject.FindGameObjectsWithTag("DisableRenderersOnStart");
            for (var i = 0; i < objectsToDisable.Length; i++)
            {
                var renderer = objectsToDisable[i].GetComponent<Renderer>();
                if (renderer) renderer.enabled = false;
            }
        }
    }
}