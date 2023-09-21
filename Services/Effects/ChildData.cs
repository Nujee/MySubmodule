using UnityEngine;

namespace Code.MySubmodule.Services.Effects
{
    public readonly struct ChildData
    {
        public readonly Transform Parent;
        public readonly Vector3 LocalPosition;
        public readonly Quaternion LocalRotation;
        
        public ChildData(Transform child)
        {
            Parent = child.parent;
            LocalPosition = child.localPosition;
            LocalRotation = child.localRotation;
        }
    }
}