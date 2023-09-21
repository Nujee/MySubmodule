using JetBrains.Annotations;
using UnityEngine;

namespace Code.MySubmodule.Math.Parabolas
{
    public sealed class DynamicParabola
    {
        private readonly Transform _startingPoint;
        private readonly Transform _endPoint;
        private readonly float _height;

        [PublicAPI]
        public DynamicParabola(Transform startingPoint, Transform endPoint, float height)
        {
            _startingPoint = startingPoint;
            _endPoint = endPoint;
            _height = height;
        }

        [PublicAPI]
        public StaticParabola ToStaticParabola()
        {
            return new StaticParabola(_startingPoint.position, _endPoint.position, _height);
        }

        /// <summary>
        /// Returns position of point on parabola fraction of lenght t.
        /// </summary>
        [PublicAPI]
        public Vector3 GetPositionOnParabola(float t)
        {
            var startPosition = _startingPoint.position;
            var endPosition = _endPoint.position;
            return Parabola.GetPosition(startPosition, endPosition, _height, t);
        }
    }
}