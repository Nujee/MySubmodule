using UnityEngine;

namespace Code.MySubmodule.CollisionHandling.SanyaPhysics.Colliders
{
    public sealed class CustomSphereCollider : MonoBehaviour
    {
        [field: SerializeField] public float Radius { get; set; } = 1f;
        [field: SerializeField] public Vector3 Center { get; set; } = Vector3.zero;
        [field: SerializeField] public Color Color { get; private set; } = Color.cyan;
        public int Layer => gameObject.layer;
    }
}