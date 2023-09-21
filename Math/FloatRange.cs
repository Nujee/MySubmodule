using JetBrains.Annotations;
using UnityEngine;

namespace Code.MySubmodule.Math
{
    [System.Serializable]
    public struct FloatRange
    {
        [SerializeField] private float min;
        [SerializeField] private float max;

        public FloatRange(float min, float max)
        {
            this.min = min;
            this.max = max;
            Length = (max - min).Abs();
        }

        [PublicAPI]
        public static FloatRange Default => new FloatRange(0f, 1f);

        [PublicAPI]
        public float Min => min;
        
        [PublicAPI]
        public float Max => max;
        
        [PublicAPI]
        public float Length { get; private set; }
        
        [PublicAPI]
        public float RandomFromRange => Random.Range(min, max);
        
        [PublicAPI]
        public bool Contains(float value) => value >= min && value <= max;
    }
}