using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.MySubmodule.Unity.ComponentsExtensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Return all children of this transform. 
        /// </summary>
        [PublicAPI]
        public static Transform[] GetAllChildrenNonAlloc(this Transform parent, Transform[] results)
        {
            if (results.Length < parent.childCount) throw new ArgumentOutOfRangeException();
            
            var childCount = parent.childCount;
            for (var i = 0; i < childCount; i++)
            {
                results[i] = parent.GetChild(i);
            }
            
            return results;
        }
    }
}