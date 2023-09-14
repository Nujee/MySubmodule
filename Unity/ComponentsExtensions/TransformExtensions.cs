using System;
using System.Runtime.CompilerServices;
using Code.Game.Constants.GeneratedCode;
using JetBrains.Annotations;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Unity.ComponentsExtensions
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
        
        /// <summary>
        /// Returns first child with tag. Only 1st generation children are checked.
        /// </summary>
        [PublicAPI]
        [CanBeNull]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.NullChecks, false)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetChildWithTag(this Transform parent, Tags tag)
        {
            for (int j = 0; j < parent.childCount; j++)
            {
                var child = parent.GetChild(j);
                if (child.CompareTag(tag.ToString()))
                {
                    return child;
                }
            }

            return null;
        }
    }
}