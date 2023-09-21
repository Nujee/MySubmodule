using Code.MySubmodule.ECS.InputCapture.Joystick;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.MySubmodule.ECS.Movement.Joystick
{
    public sealed class s_FromJoystickRigidbodyMove : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<r_ProcessJoystickInput, c_JoystickMovementData>> _player = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _player.Value)
            {
                ref var c_joystickMovementData = ref _player.Pools.Inc2.Get(entity);
                ref var r_processJoystickInput = ref _player.Pools.Inc1.Get(entity);
                var input = r_processJoystickInput.Input;
                
                ChangeRotation(input, ref c_joystickMovementData);
                ChangePosition(input, ref c_joystickMovementData);

                _player.Pools.Inc1.Del(entity);
            }
        }

        private void ChangeRotation(Vector3 input, ref c_JoystickMovementData movementData)
        {
            movementData.MovingRigidbody.angularVelocity = Vector3.zero;
            if (input == Vector3.zero) return;
            
            var newRotation = Quaternion.LookRotation(input, Vector3.up);
            var transform = movementData.RotatingTransform;
            var turnDelta = movementData.RotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, turnDelta);
        }

        private void ChangePosition(Vector3 input, ref c_JoystickMovementData movementData)
        {
            var transform = movementData.MovingTransform;
            var move = transform.forward * input.magnitude * movementData.MovementSpeed * Time.deltaTime;

            movementData.MovingRigidbody.velocity = move;
        }
    }
}