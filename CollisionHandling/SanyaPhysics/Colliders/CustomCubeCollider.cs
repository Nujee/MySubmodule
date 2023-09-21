using UnityEngine;

namespace Code.MySubmodule.CollisionHandling.SanyaPhysics.Colliders
{
    public sealed class CustomCubeCollider : MonoBehaviour
    {
        [field: SerializeField] public Vector3 Size { get; set; } = Vector3.one;
        [field: SerializeField] public Vector3 Center { get; set; } = Vector3.zero;
        [field: SerializeField] public Color Color { get; private set; } = Color.cyan;
    }
}