using Code.BlackCubeSubmodule.Math;
using Code.BlackCubeSubmodule.Services.Effects.DoTween;
using Code.BlackCubeSubmodule.Services.Effects.DoTween.EffectSettings;
using Code.BlackCubeSubmodule.Services.LifeTime;
using Code.Game.Constants.GeneratedCode;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Code.BlackCubeSubmodule.Services.UI.Views.Extensions
{
    public static class ViewExtensions
    {
        /// <summary>
        /// Gets all children of the view and shows them one by one with delay seconds between them.
        /// AppearEffect will be used with default settings.
        /// </summary>
        [PublicAPI]
        public static void ShowElementsOneByOne(this View view, float delayBetweenElements)
        {
            var appearEffectSettings = AppearEffectSettings.Default;
            ShowElementsOneByOne(view, delayBetweenElements, appearEffectSettings);
        }
        
        /// <summary>
        /// Gets all children of the view and shows them one by one with delay seconds between them.
        /// </summary>
        [PublicAPI]
        public static async void ShowElementsOneByOne(this View view, float delayBetweenElements, AppearEffectSettings appearEffectSettings)
        {
            var transform = view.transform;
            var ignoreTag = Tags.ScreenBaseIgnore.ToString();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.gameObject.CompareTag(ignoreTag)) continue;
                
                child.gameObject.SetActive(false);
            }
            
            var delay = delayBetweenElements.ToMilliseconds();
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.gameObject.CompareTag(ignoreTag)) continue;
                
                child.gameObject.SetActive(true);
                child.DoAppearEffect(appearEffectSettings);
                
                await UniTask.Delay(delay, cancellationToken: LifeTimeService.GetToken());
            }
        }
    }
}