using System.Runtime.CompilerServices;
using Code.BlackCubeSubmodule.Math;
using Code.BlackCubeSubmodule.Services.LifeTime;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Code.BlackCubeSubmodule.ECS.Features.Requests.RequestTrain
{
    /// <summary>
    /// Train is a tool to reduce amount of code needed for sequential execution of systems.
    /// </summary>
    public sealed class Train
    {
        private readonly EcsWorld _world;
        private readonly int _entity;

        private float _accumulatedDelay;

        public Train(EcsWorld world, int entity)
        {
            _world = world;
            _entity = entity;
            _accumulatedDelay = 0f;

            _world.GetPool<c_TrainRunning>().Add(entity);
        }
        
        /// <summary>
        /// Adds step to train. A delay before step execution could be specified.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [PublicAPI]
        public Train AddStep<T>(float delay = 0f)
        where T: IEcsRunSystem
        {
            RunStep<T>(delay);
            
            return this;
        }
        
        private async void RunStep<T>(float delay = 0f)
        where T: IEcsRunSystem
        {
            var pool = _world.GetPool<Step<T>>();
            _accumulatedDelay += delay;

            await UniTask.Delay(_accumulatedDelay.ToMilliseconds(), cancellationToken: LifeTimeService.GetToken());
            pool.Add(_entity);
        }
    }
}