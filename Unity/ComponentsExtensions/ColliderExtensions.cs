using Code.BlackCubeSubmodule.Utility.Constants;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Unity.ComponentsExtensions
{
    public static class ColliderExtensions
    {
        /// <summary>
        /// Returns true if provided point is within collider.
        /// </summary>
        [PublicAPI]
        public static bool IsPointWithinCollider(this Collider collider, Vector3 point)
        {
            return (collider.ClosestPoint(point) - point).sqrMagnitude < Numbers.VerySmallNumber;
        }
    }
}