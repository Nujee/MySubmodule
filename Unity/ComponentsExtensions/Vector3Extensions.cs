using JetBrains.Annotations;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Unity.ComponentsExtensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Changes one or more axes of given Vector3.
        /// </summary>
        [PublicAPI]
        public static Vector3 WithChangedAxes(this Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? v.x, y ?? v.y, z ?? v.z);
        }

        /// <summary>
        /// Prints vector3 to console
        /// </summary>
        [PublicAPI]
        public static void Print(this Vector3 a, string prefix = "")
        {
#if UNITY_EDITOR
            var message = $"({prefix} {a.x :0.000000}, {a.y :0.000000}, {a.z :0.000000})";
            Debug.Log(message);
#endif
        }
    }
}





