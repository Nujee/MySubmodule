using Code.BlackCubeSubmodule.UI.FpsCounter;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.BlackCubeSubmodule.ECS.Features.PerformanceStatistics
{
    public sealed class s_UpdateFpsCounter : IEcsInitSystem, IEcsRunSystem
    {
        private const float UpdateInterval = 0.5f;
        
        private readonly EcsFilterInject<Inc<c_FpsCounterTimer, c_FpsCounterModel>> _fpsCounterTimer = default;

        private readonly EcsCustomInject<FpsCounterView> _fpsCounterView = default;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var counterEntity = world.NewEntity();

            ref var c_timer = ref world.GetPool<c_FpsCounterTimer>().Add(counterEntity);
            c_timer.Accumulated = 0f;
            c_timer.Frames = 0;
            c_timer.Delay = UpdateInterval;

            ref var c_model = ref world.GetPool<c_FpsCounterModel>().Add(counterEntity);
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _fpsCounterTimer.Value)
            {
                ref var c_timer = ref _fpsCounterTimer.Pools.Inc1.Get(entity);
                c_timer.Accumulated += Time.timeScale / Time.deltaTime;
                c_timer.Frames++;
                c_timer.Delay -= Time.deltaTime;
                
                if (c_timer.Delay < 0f)
                {
                    var fps = c_timer.Accumulated / c_timer.Frames;
                    
                    ref var c_model = ref _fpsCounterTimer.Pools.Inc2.Get(entity);
                    c_model.Fps.Value = fps;
                    
                    c_timer.Accumulated = 0f;
                    c_timer.Frames = 0;
                    c_timer.Delay = UpdateInterval;
                }
            }
        }
    }
}