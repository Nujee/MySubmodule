using Code.BlackCubeSubmodule.ECS.Features.Requests.RequestsToFeatures;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.BlackCubeSubmodule.ECS.FeaturesImplementations.InputCapture.Joystick
{
    public sealed class s_CaptureJoystickInput : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<c_ReceivingJoystickInput>> _inputReceivers = default;

        private readonly EcsCustomInject<JoystickPack.Joystick> _joystick = default;

        private Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0f, 45f, 0f));

        public void Run(IEcsSystems systems)
        {
            var input = new Vector3(_joystick.Value.Horizontal, 0, _joystick.Value.Vertical);
            var inputToIso = _isoMatrix.MultiplyPoint3x4(input);

            var world = systems.GetWorld();
            foreach (var entity in _inputReceivers.Value)
            {
                var packedEntity = world.PackEntity(entity);
                systems.GetWorld().AddRequest(new r_ReceiveJoystickInput(inputToIso, packedEntity));   
            }
        }
    }
}