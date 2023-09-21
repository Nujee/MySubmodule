using JetBrains.Annotations;
using UnityEngine;

namespace Code.MySubmodule.Math
{
    public static class Distance
    {
        /// <summary>
        /// Ignores Y axis.
        /// </summary>
        [PublicAPI]
        public static float Manhattan(Vector3 a, Vector3 b)
        {
            var x = (a.x - b.x).Abs();
            var z = (a.z - b.z).Abs();
            return x + z;
        }     
        
        /// <summary>
        /// Returns square magnitude of direction. Ignores Y axis.
        /// </summary>
        [PublicAPI]
        public static float Square(Vector3 from, Vector3 to)
        {
            var direction = from - to;
            direction.y = 0f;
            return direction.sqrMagnitude;
        }
    }
}