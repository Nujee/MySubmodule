using Code.BlackCubeSubmodule.Unity.ComponentsExtensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.BlackCubeSubmodule.ECS.UI.FollowUIElements
{
    public sealed class s_UpdateFollowUIElements : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<c_FollowUIElement>> _uiElements = default;
        private readonly EcsCustomInject<Camera> _camera = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _uiElements.Value)
            {
                ref var c_uiElement = ref _uiElements.Pools.Inc1.Get(entity);
                
                var state = c_uiElement.Target.gameObject.activeInHierarchy;
                c_uiElement.RectTransform.gameObject.SetActive(state);
                if (!state) continue;
                
                var newPosition = _camera.Value.WorldToScreenPoint(c_uiElement.Target.position);
                c_uiElement.RectTransform.SetPositionAndRotation(newPosition.WithChangedAxes(z: 0f), Quaternion.identity);
            }
        }
    }
}