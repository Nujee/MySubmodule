using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Debug = UnityEngine.Debug;

namespace Code.BlackCubeSubmodule.Utility.PerformanceTesting
{
    public static class MethodPerformanceTesting
    {
        [PublicAPI]
        public static long Run(Action method)
        {
            const int warmupIterations = 100000;
            const int iterations = 10000000;

            for (var i = 0; i < warmupIterations; i++)
            {
                method.Invoke();
            }
            
            var stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < iterations; i++)
            {
                method.Invoke();
            }
            
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedTicks;
            var median = elapsed / iterations;

            Debug.Log($"Method took {elapsed} ticks in {iterations} iterations. Median: {median} ticks.");

            return elapsed;
        }
    }
}