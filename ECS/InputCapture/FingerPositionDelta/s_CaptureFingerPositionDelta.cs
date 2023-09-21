using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.MySubmodule.ECS.InputCapture.FingerPositionDelta
{
    public sealed class s_CaptureFingerPositionDelta : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<m_ReceivingInput>> _inputReceivers = default;
        private readonly EcsPoolInject<r_ProcessFingerPositionDeltaInput> _requestPool = default;

        private Vector3 _previousFramePosition;

        public void Run(IEcsSystems systems)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _previousFramePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                var fingerScreenPosition = Input.mousePosition;
                var delta = fingerScreenPosition - _previousFramePosition;
                var input = new Vector3(delta.x, 0, delta.y);
                
                foreach (var entity in _inputReceivers.Value)
                {
                    if (_requestPool.Value.Has(entity)) _requestPool.Value.Del(entity);
                
                    ref var r_processFingerPositionDeltaInput = ref _requestPool.Value.Add(entity);
                    r_processFingerPositionDeltaInput.Delta = input;
                }

                _previousFramePosition = fingerScreenPosition;
            }
        }
    }
}