using Code.BlackCubeSubmodule.Services.UI.ScreenService;
using Code.BlackCubeSubmodule.UI.DefeatScreen;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.BlackCubeSubmodule.ECS.EndGame
{
    public sealed class s_RunDefaultDefeat : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<r_RunDefaultDefeat>> _defeatRequests = default;

        private readonly EcsCustomInject<ScreenService> _screenService = default;
        private readonly EcsCustomInject<DefeatViewConfig> _defeatViewConfig = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _defeatRequests.Value)
            {
                DoOnDefeat();
                
                systems.GetWorld().DelEntity(requestEntity);
            }
        }

        private void DoOnDefeat()
        {
            _screenService.Value.OpenScreen<DefeatView>(_defeatViewConfig.Value.ShowDelay);
        }
    }
}