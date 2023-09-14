using Code.BlackCubeSubmodule.Math;
using Code.BlackCubeSubmodule.Services.LevelBoot;
using Code.BlackCubeSubmodule.Services.LifeTime;
using Code.BlackCubeSubmodule.Services.UI.ScreenService;
using Code.BlackCubeSubmodule.Services.UI.Views;
using Code.BlackCubeSubmodule.Services.UI.Views.Extensions;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.UI;
 
namespace Code.BlackCubeSubmodule.UI.DefeatScreen
{
    [DisallowMultipleComponent]
    public sealed class DefeatView : View, IAccessibleToScreenService
    {
        private readonly EcsCustomInject<DefeatViewConfig> _config = default;
        private readonly EcsCustomInject<LevelService> _levelService = default;
        
        [SerializeField] private Button _button;

        public override void Init(IEcsSystems systems)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => _levelService.Value.ReloadCurrentScene());
        }
 
        protected override async void OnOpen()
        {
            this.ShowElementsOneByOne(_config.Value.DelayBetweenElementsPopup, _config.Value.AppearEffect);
            
            await UniTask.Delay(_config.Value.AutoCloseDelay.ToMilliseconds(), cancellationToken: LifeTimeService.GetToken());
            _button.onClick.Invoke();
        }
 
        public void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}