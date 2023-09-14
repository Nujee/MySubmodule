using Code.Game.Constants.GeneratedCode;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.BlackCubeSubmodule.ECS.OnInitTagActionsFeature
{
    public sealed class i_DisableRenderersOnStartInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var objectsToDisable = GameObject.FindGameObjectsWithTag(Tags.DisableRenderersOnStart.ToString());
            for (var i = 0; i < objectsToDisable.Length; i++)
            {
                var renderer = objectsToDisable[i].GetComponent<Renderer>();
                if (renderer) renderer.enabled = false;
            }
        }
    }
}