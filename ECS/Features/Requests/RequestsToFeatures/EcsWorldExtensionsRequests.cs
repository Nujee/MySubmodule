using System.Runtime.CompilerServices;
using Code.BlackCubeSubmodule.Math;
using Code.BlackCubeSubmodule.Services.LifeTime;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Code.BlackCubeSubmodule.ECS.Features.Requests.RequestsToFeatures
{
    public static class EcsWorldExtensionsRequests
    {
        /// <summary>
        /// Will create a new entity with a request of type T.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void AddRequest<T>(this EcsWorld world, float delay = 0f)
        where T: struct
        {
            if (delay > 0f)
            {
                await UniTask.Delay(delay.ToMilliseconds(), cancellationToken: LifeTimeService.GetToken());
            }
            
            var requestEntity = world.NewEntity();
            var pool = (EcsPool<T>)world.GetPoolByType(typeof(T));

            pool.Add(requestEntity);
        }
        
        /// <summary>
        /// Will create a new entity with a request of type T.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void AddRequest<T>(this EcsWorld world, T requestComponent, float delay = 0f)
            where T: struct
        {
            if (delay > 0f)
            {
                await UniTask.Delay(delay.ToMilliseconds(), cancellationToken: LifeTimeService.GetToken());
            }

            var requestEntity = world.NewEntity();
            var pool = (EcsPool<T>)world.GetPoolByType(typeof(T));

            ConfigureRequest(pool, requestEntity, requestComponent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ConfigureRequest<T>(EcsPool<T> pool, int requestEntity, T requestComponent)
            where T : struct
        {
            ref var request = ref pool.Add(requestEntity);
            request = requestComponent;
        }
    }
}