using Code.Game.Constants.GeneratedCode;
using Code.MySubmodule.ECS.ExtensionMethods;
using Code.MySubmodule.ECS.LevelEntity;
using Code.MySubmodule.Services.Effects;
using Code.MySubmodule.Services.UI.Screens.ConcreteScreens;
using Code.MySubmodule.Services.UI.ScreenService;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.MySubmodule.ECS.EndGame
{
    public sealed class s_RunDefaultVictory : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<c_LevelRuntimeData, r_Victory>, Exc<m_GameHasEnded>> _levels = default;
        private readonly EcsPoolInject<m_GameHasEnded> _gameEnded = default;
        private readonly EcsCustomInject<ScreenService> _screenService = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _levels.Value)
            {
                _levels.Pools.Inc2.Del(entity);
                _gameEnded.Value.Add(entity);
                DoOnVictory(systems.GetWorld());
            }
        }

        private void DoOnVictory(EcsWorld world)
        {
            _screenService.Value.OpenScreen<VictoryScreen>();
            EffectName.Confetti.Play();
            
            world.TurnSystemGroupOff(SystemType.Update.ToString());
            world.TurnSystemGroupOn("Endgame");
        }
    }
}