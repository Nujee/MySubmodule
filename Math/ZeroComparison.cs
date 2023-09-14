using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Math
{
    public static class ZeroComparison
    {
        /// <summary>
        /// Compares float to zero
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this float a) => Mathf.Approximately(a, 0f);
    }
}