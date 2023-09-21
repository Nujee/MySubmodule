using Code.MySubmodule.GameSettings;
using Code.MySubmodule.Math;
using Code.MySubmodule.Services.Effects.DoTween;
using Code.MySubmodule.Services.Effects.DoTween.EffectSettings;
using Code.MySubmodule.Services.LevelBoot;
using Code.MySubmodule.Services.LifeTime;
using Code.MySubmodule.Services.UI.ScreenService;
using Code.MySubmodule.Services.UI.Views;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MySubmodule.Services.UI.Screens.ConcreteScreens
{
    [DisallowMultipleComponent]
    public sealed class VictoryScreen : View, IAccessibleToScreenService
    {
        private readonly EcsCustomInject<UIConfig> _uiConfig = default;
        private readonly EcsCustomInject<LevelService> _levelService = default;

        private Image _text;
        private Button _button;

        public override void Init(IEcsSystems systems)
        {
            _text = GetComponentInChildren<Image>(true);
            _button = GetComponentInChildren<Button>(true);
            _button.onClick.AddListener(() => _levelService.Value.LoadNextScene());
        }
        
        protected override async void OnOpen()
        {
            _button.gameObject.SetActive(false);
            _text.transform.DoAppearEffect(AppearEffectSettings.Default);
            
            var delay = _uiConfig.Value.DelayBetweenUIElementsAppearance.ToMilliseconds();
            await UniTask.Delay(delay, cancellationToken: LifeTimeService.GetToken());
            
            _button.gameObject.SetActive(true);
            _button.transform.DoAppearEffect(AppearEffectSettings.Default);
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}