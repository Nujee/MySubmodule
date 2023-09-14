using JetBrains.Annotations;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Math.Parabolas
{
    public readonly struct StaticParabola
    {
        private readonly Vector3 _startingPoint;
        private readonly Vector3 _endPoint;
        private readonly float _height;

        [PublicAPI]
        public StaticParabola(Vector3 startingPoint, Vector3 endPoint, float height)
        {
            _startingPoint = startingPoint;
            _endPoint = endPoint;
            _height = height;
        }

        /// <summary>
        /// Returns position of point on parabola fraction of lenght t.
        /// </summary>
        [PublicAPI]
        public Vector3 GetPositionOnParabola(float t)
        {
            var startPosition = _startingPoint;
            var endPosition = _endPoint;
            return Parabola.GetPosition(startPosition, endPosition, _height, t);
        }
    }
}