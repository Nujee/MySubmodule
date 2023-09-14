using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.IL2CPP.CompilerServices;

namespace Code.BlackCubeSubmodule.Math
{
    public static class RandomMethods
    {
        /// <summary>
        /// Randomly shuffles provided collection. Doesn't allocate.
        /// </summary>
        [PublicAPI]
        public static IList<T> KnuthShuffle<T>(this IList<T> collection)
        {
            var count = collection.Count;
            while (count > 1)
            {
                var randomIndex = UnityEngine.Random.Range(0, count--);
                (collection[count], collection[randomIndex]) = (collection[randomIndex], collection[count]);
            }
            
            return collection;
        }
        
        /// <summary>
        /// Will return random item from collection. If collection is empty will return default T.
        /// </summary>
        [PublicAPI]
        [CanBeNull]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.NullChecks, false)]
        public static T Random<T>(this IList<T> collection)
        {
            if (collection.Count == 0) return default;
            return collection[UnityEngine.Random.Range(0, collection.Count)];
        }

        /// <summary>
        /// Return provided number with random sign.
        /// </summary>
        [PublicAPI]
        [Il2CppSetOption(Option.NullChecks, false)]
        public static int WithRandomSign(this int number)
        {
            var randomSign = UnityEngine.Random.Range(0, 2) * 2 - 1;
            return number * randomSign;
        }
        
        /// <summary>
        /// Return provided number with random sign.
        /// </summary>
        [PublicAPI]
        public static float WithRandomSign(this float number)
        {
            var randomSign = UnityEngine.Random.Range(0, 2) * 2 - 1;
            return number * (float)randomSign;
        }

        /// <summary>
        /// Recalculated chances to 100% and return array with 100 elements with provided chances.
        /// Array is allocated. Use only on init. 
        /// </summary>
        /// <param name="decimalPrecision">Chances precision</param>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //TODO: add ZeroDivision-disabled attribute and ArrayBoundsCheck-disabled attribute
        public static T[] GetSelectionArray<T>(T[] array, Func<T, float> weightSelector, DecimalPrecision precisionType = DecimalPrecision.Double)
        {
            var precisionFactor = (int)System.Math.Pow(10d, (int)precisionType);
            
            var totalWeightsSum = 0f;
            for (var i = 0; i < array.Length; i++)
            {
                totalWeightsSum += weightSelector(array[i]);
            }

            Span<int> parts = stackalloc int[array.Length];
            var partsSum = 0;
            for (var i = 0; i < parts.Length; i++)
            {
                var weightNormalized = weightSelector(array[i]) / totalWeightsSum;
                parts[i] = (int)System.Math.Round(weightNormalized * precisionFactor);
                partsSum += parts[i];
            }
            
            var delta = precisionFactor - partsSum;
            var max = 0;
            var maxIndex = 0;
            for (var i = 0; i < parts.Length; i++)
            {
                if (parts[i] > max)
                {
                    max = parts[i];
                    maxIndex = i;
                }
            }
            parts[maxIndex] += delta;
            
            var result = new T[precisionFactor];
            var index = 0;
            for (var i = 0; i < array.Length; i++)
            {
                for (var j = 0; j < parts[i]; j++)
                {
                    result[index++] = array[i];
                }
            }
            
            return result;
        }
    }
}