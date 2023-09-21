using JetBrains.Annotations;
using UnityEngine;

namespace Code.MySubmodule.Math
{
    [System.Serializable]
    public struct IntRange
    {
        [SerializeField] private int min;
        [SerializeField] private int max;

        public IntRange(int min, int max)
        {
            this.min = min;
            this.max = max;
            Length = (max - min).Abs();
        }
        
        [PublicAPI]
        public static IntRange Default => new IntRange(0, 1);

        [PublicAPI]
        public int Min => min;
        
        [PublicAPI]
        public int Max => max;
        
        [PublicAPI]
        public int Length { get; }
        
        [PublicAPI]
        public int RandomFromRange => Random.Range(min, max + 1);
        
        [PublicAPI]
        public bool Contains(int value) => value >= min && value <= max;
    }
}