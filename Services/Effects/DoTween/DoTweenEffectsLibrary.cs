using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Services.Effects.DoTween
{
    // CLASS FOR DELETION
    public static partial class DoTweenEffectsLibrary
    {
        [PublicAPI]
        [Obsolete]
        public static Tweener DoPulse(this Transform transform, float duration = 1f, float target = 1.5f, 
            Ease ease = Ease.Linear)
        {
            return transform.DOScale(Vector3.one * target, duration)
                .SetEase(ease)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}