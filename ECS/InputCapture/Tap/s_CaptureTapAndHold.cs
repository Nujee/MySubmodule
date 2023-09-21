using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.MySubmodule.ECS.InputCapture.Tap
{
    public class s_CaptureTapAndHold : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<m_ReceivingInput>> _inputReceivers = default;
        private readonly EcsPoolInject<r_ProcessTap> _processTapRequestPool = default;
        private readonly EcsPoolInject<r_ProcessUntap> _processUntapRequestPool = default;
        private readonly EcsPoolInject<m_IsHold> _isHold = default;

        public void Run(IEcsSystems systems)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            foreach (var entity in _inputReceivers.Value)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_processTapRequestPool.Value.Has(entity)) _processTapRequestPool.Value.Del(entity);
                    
                    ref var tapProcessRequest = ref _processTapRequestPool.Value.Add(entity);
                    tapProcessRequest.TapScreenPosition = Input.mousePosition;

                    if (!_isHold.Value.Has(entity)) _isHold.Value.Add(entity);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (_processUntapRequestPool.Value.Has(entity)) _processUntapRequestPool.Value.Del(entity);
                    
                    ref var tapProcessRequest = ref _processUntapRequestPool.Value.Add(entity);
                    tapProcessRequest.UntapScreenPosition = Input.mousePosition;
                    
                    if (_isHold.Value.Has(entity)) _isHold.Value.Del(entity);
                }
            }
        }
    }
}