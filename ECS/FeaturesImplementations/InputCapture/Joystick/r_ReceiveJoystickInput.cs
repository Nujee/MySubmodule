using Leopotam.EcsLite;
using UnityEngine;

namespace Code.BlackCubeSubmodule.ECS.FeaturesImplementations.InputCapture.Joystick
{
    public struct r_ReceiveJoystickInput
    {
        public Vector3 Input;
        public EcsPackedEntity Entity;

        public r_ReceiveJoystickInput(Vector3 input, EcsPackedEntity entity)
        {
            Input = input;
            Entity = entity;
        }
    }
}