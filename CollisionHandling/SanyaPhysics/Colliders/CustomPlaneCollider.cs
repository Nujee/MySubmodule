using UnityEngine;

namespace Code.MySubmodule.CollisionHandling.SanyaPhysics.Colliders
{
    public sealed class CustomPlaneCollider : MonoBehaviour
    {
        [field: SerializeField] public Vector2 Size { get; set; }
        [field: SerializeField] public Vector3 Center { get; set; } = Vector3.zero;
        [field: SerializeField] public Color Color { get; private set; } = Color.cyan;
    }
}