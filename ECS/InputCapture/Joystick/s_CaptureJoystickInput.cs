using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.MySubmodule.ECS.InputCapture.Joystick
{
    public sealed class s_CaptureJoystickInput : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<m_ReceivingInput>> _inputReceivers = default;
        private readonly EcsPoolInject<r_ProcessJoystickInput> _requestPool = default;
        private readonly EcsCustomInject<JoystickPack.Joystick> _joystick = default;

        private Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0f, 45f, 0f));

        public void Run(IEcsSystems systems)
        {
            var input = new Vector3(_joystick.Value.Horizontal, 0, _joystick.Value.Vertical);
            var inputToIso = _isoMatrix.MultiplyPoint3x4(input);
            
            foreach (var entity in _inputReceivers.Value)
            {
                if (_requestPool.Value.Has(entity)) _requestPool.Value.Del(entity);
                
                ref var r_processJoystickInput = ref _requestPool.Value.Add(entity);
                r_processJoystickInput.Input = inputToIso;
            }
        }
    }
}