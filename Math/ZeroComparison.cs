using JetBrains.Annotations;
using UnityEngine;

namespace Code.MySubmodule.Math
{
    public static class ZeroComparison
    {
        /// <summary>
        /// Compares float to zero
        /// </summary>
        [PublicAPI]
        public static bool IsZero(this float a) => Mathf.Approximately(a, 0f);
    }
}