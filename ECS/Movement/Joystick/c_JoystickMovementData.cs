using UnityEngine;

namespace Code.MySubmodule.ECS.Movement.Joystick
{
    public struct c_JoystickMovementData
    {
        public Transform RotatingTransform;
        public Transform MovingTransform;
        public Rigidbody MovingRigidbody;
        public float RotationSpeed;
        public float MovementSpeed;
    }
}