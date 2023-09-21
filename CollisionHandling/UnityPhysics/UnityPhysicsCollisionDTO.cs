using JetBrains.Annotations;
using UnityEngine;

namespace Code.MySubmodule.CollisionHandling.UnityPhysics
{
    /// <summary>
    /// Data transfer object for collision data.
    /// </summary>
    public readonly struct UnityPhysicsCollisionDTO
    {
        /// <summary>
        /// ECS entity, associated with gameObject that caught collision. 
        /// </summary>
        [PublicAPI] 
        public readonly int SelfEntity;
        /// <summary>
        /// Collider that caught collision.
        /// </summary>
        [PublicAPI] 
        public readonly Collider SelfCollider;
        /// <summary>
        /// ECS entity, associated with other gameObject.
        /// </summary>
        [PublicAPI] 
        public readonly int OtherEntity;
        /// <summary>
        /// Other collider. 
        /// </summary>
        [PublicAPI] 
        public readonly Collider OtherCollider;

        [PublicAPI]
        public UnityPhysicsCollisionDTO(int selfEntity, Collider selfCollider, int otherEntity, Collider otherCollider)
        {
            SelfEntity = selfEntity;
            SelfCollider = selfCollider;
            OtherEntity = otherEntity;
            OtherCollider = otherCollider;
        }

        public override string ToString()
        {
            return $"({SelfCollider.gameObject}, entity {SelfEntity}) colliding with ({OtherCollider.gameObject.name}, entity {OtherEntity})";
        }
    }
}