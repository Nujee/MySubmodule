using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using SystemMath = System.Math;

namespace Code.BlackCubeSubmodule.Math
{
    public static class QuickMathExtensions
    {
        /// <summary>
        /// Returns given number multiplied by 1000.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToMilliseconds(this float f) => (int)(f * 1000);
        
        /// <summary>
        /// Returns given number multiplied by 1000.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToMilliseconds(this int i) => i * 1000;
       
        /// <summary>
        /// Returns modulus of a given number.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(this float f) => SystemMath.Abs(f);
        
        /// <summary>
        /// Returns modulus of a given number.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(this int i) => SystemMath.Abs(i);

        /// <summary>
        /// Returns sign of a given number.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(this float f) => SystemMath.Sign(f);
        
        /// <summary>
        /// Returns sign of a given number.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(this int i) => SystemMath.Sign(i);

        /// <summary>
        /// Returns given number in second power.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Square(this float f) => f * f;

        /// <summary>
        /// Returns given number in second power.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Square(this int i) => i * i;
        
        /// <summary>
        /// Returns given number in third power.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cube(this float f) => f * f * f;

        /// <summary>
        /// Returns given number in third power.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Cube(this int i) => i * i * i;

        /// <summary>
        /// Returns square root of a given number.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(this float a) => Mathf.Sqrt(a);
    }
}