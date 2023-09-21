using Code.MySubmodule.Math;
using Code.MySubmodule.Services.Effects.DoTween.EffectSettings;
using Code.MySubmodule.Services.LifeTime;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MySubmodule.Services.Effects.DoTween
{
    public static class DoTweenEffectsLibrary
    {
        [PublicAPI]
        public static Tweener DoAppearEffect(this Transform transform)
        {
            return DoAppearEffect(transform, AppearEffectSettings.Default);
        }

        [PublicAPI]
        public static Tweener DoAppearEffect(this Transform transform, AppearEffectSettings settings)
        {
            return transform.DOScale(Vector3.one * settings.StartingScale, settings.Duration)
                .From()
                .SetEase(settings.Ease);
        }

        [PublicAPI]
        public static async UniTask<Tweener> DoExpandEffect(this Transform transform)
        {
            var tweener = await DoExpandEffect(transform, ExpandEffectSettings.Default);
            
            return tweener;
        }
        
        [PublicAPI]
        public static async UniTask<Tweener> DoExpandEffect(this Transform transform, ExpandEffectSettings settings)
        {
            var tweener = transform.DOScale(Vector3.one * settings.StartingScale, settings.Duration)
                .From()
                .SetEase(settings.Ease);

            await UniTask.Delay(settings.Duration.ToMilliseconds(), cancellationToken: LifeTimeService.GetToken());

            return tweener;
        }
        
        [PublicAPI]
        public static async UniTask<Tweener> DoShrinkEffect(this Transform transform)
        {
            var tweener = await DoShrinkEffect(transform, ShrinkEffectSettings.Default);

            return tweener;
        }
        
        [PublicAPI]
        public static async UniTask<Tweener> DoShrinkEffect(this Transform transform, ShrinkEffectSettings settings)
        {
            var tweener =  transform.DOScale(settings.TargetScale, settings.Duration)
                .SetEase(settings.Ease);
            
            await UniTask.Delay(settings.Duration.ToMilliseconds(), cancellationToken: LifeTimeService.GetToken());
            
            return tweener;
        }
        
        [PublicAPI]
        public static Tweener DoPulse(this Transform transform)
        {
            return DoPulse(transform, PulseEffectSettings.Default);
        }
        
        [PublicAPI]
        public static Tweener DoPulse(this Transform transform, PulseEffectSettings settings)
        {
            return transform.DOScale(Vector3.one * settings.TargetScale, settings.Duration)
                .SetEase(settings.Ease)
                .SetLoops(-1, settings.LoopType);
        }
        
        [PublicAPI]
        public static Tweener DoFade(this Image image)
        {
            return DoFade(image, FadeEffectSettings.Default);
        }
        
        [PublicAPI]
        public static Tweener DoFade(this Image image, FadeEffectSettings settings)
        {
            var rendererColor = image.color;
            return DOTween.To(() => rendererColor.a, x => rendererColor.a = x, settings.EndValue, settings.Duration)
                .OnUpdate(() => image.color = rendererColor);
        }
    }
}