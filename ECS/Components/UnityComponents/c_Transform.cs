using UnityEngine;
using UnityEngine.Serialization;

namespace Code.MySubmodule.ECS.Components.UnityComponents
{
    public struct c_Transform
    {
        [FormerlySerializedAs("Transform")] public Transform Value;
    }
}