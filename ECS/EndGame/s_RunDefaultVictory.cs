using Code.BlackCubeSubmodule.Math;
using Code.BlackCubeSubmodule.Services.Effects;
using Code.BlackCubeSubmodule.Services.LifeTime;
using Code.BlackCubeSubmodule.Services.UI.ScreenService;
using Code.BlackCubeSubmodule.UI.VictoryScreen;
using Code.Game.Constants.GeneratedCode;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.BlackCubeSubmodule.ECS.EndGame
{
    public sealed class s_RunDefaultVictory : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<r_RunDefaultVictory>> _victoryRequests = default;

        private readonly EcsCustomInject<ScreenService> _screenService = default;
        private readonly EcsCustomInject<VictoryViewConfig> _victoryViewConfig = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _victoryRequests.Value)
            {
                DoOnVictory();
                
                systems.GetWorld().DelEntity(requestEntity);
            }
        }

        private async void DoOnVictory()
        {
            var delay = _victoryViewConfig.Value.ShowDelay.ToMilliseconds();
            await UniTask.Delay(delay, cancellationToken: LifeTimeService.GetToken());
            
            _screenService.Value.OpenScreen<VictoryView>();
            EffectName.Confetti.Play();
        }
    }
}