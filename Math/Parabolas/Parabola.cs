using JetBrains.Annotations;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Math.Parabolas
{
    public static class Parabola
    {
        /// <summary>
        /// Return position on parabola with provided parameters. 
        /// </summary>
        [PublicAPI]
        public static Vector3 GetPosition(Vector3 startPosition, Vector3 endPosition, float height, float t)
        {
            var mid = Vector3.Lerp(startPosition, endPosition, t);
            var y = GetY(t, height) + Mathf.Lerp(startPosition.y, endPosition.y, t);
            return new Vector3(mid.x, y, mid.z);

            float GetY(float x, float height)
            {
                return -4 * height * x * x + 4 * height * x;
            }
        }
    }
}