using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Code.BlackCubeSubmodule.ECS.Features.Requests.RequestTrain
{
    public static class EcsSystemsExtensionsRequestTrain
    {
        /// <summary>
        /// Adds train to entity. Train will add steps to the entity one by one. 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [PublicAPI]
        public static Train AddTrainTo(this IEcsSystems systems, int entity)
        {
            return new Train(systems.GetWorld(), entity);
        }
        
        /// <summary>
        /// Removes train from entity.
        /// </summary>
        [PublicAPI]
        public static void CleanTrainOn(this IEcsSystems systems, int entity)
        {
            systems.GetWorld().GetPool<c_TrainRunning>().Del(entity);
        }
    }
}