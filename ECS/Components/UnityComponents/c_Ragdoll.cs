using UnityEngine;

namespace Code.MySubmodule.ECS.Components.UnityComponents
{
    public struct c_Ragdoll
    {
        public Rigidbody[] Rigidbodies;
        public Transform Root;
    }
}