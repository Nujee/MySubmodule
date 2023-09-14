using System.Runtime.CompilerServices;
using Code.BlackCubeSubmodule.ECS.Features.RootEntity;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;

namespace Code.BlackCubeSubmodule.ECS.Features.FeatureSwitches
{
    public static class EcsWorldExtensionsFeatureSwitches
    {
        /// <summary>
        /// Starts execution of feature for all entities, that had it added.
        /// Does not add feature to entities, that don't have it.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TurnFeatureOn<T>(this EcsWorld world) 
            where T : Feature
        {
            if (!world.GetPool<active_feature__<T>>().Has(0))
            {
                world.GetPool<active_feature__<T>>().Add(0);
            }
            
            ChangeSystemGroupState(world, typeof(T).Name, true);
        }
        
        /// <summary>
        /// Completely stops feature execution. It won't run for all entities.
        /// Does not remove feature from entities, that have it.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TurnFeatureOff<T>(this EcsWorld world) 
            where T : Feature
        {
            if (world.GetPool<active_feature__<T>>().Has(0))
            {
                world.GetPool<active_feature__<T>>().Del(0);
            }
            
            ChangeSystemGroupState(world, typeof(T).Name, false);
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ChangeSystemGroupState(EcsWorld world, string groupName, bool newState)
        {
            var entity = world.NewEntity();
            ref var eventGroup = ref world.GetPool<EcsGroupSystemState>().Add(entity);
            eventGroup.Name = groupName;
            eventGroup.State = newState;
        }
        
        /// <summary>
        /// Creates request to add feature for entity. Request should be processed by featureSetup system.
        /// MarkFeatureAdded() should be called on request after it was received. 
        /// </summary>
        /// <param name= "targetEntity"> Ecs entity to which feature will be added </param>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddFeature<T>(this EcsWorld world, int targetEntity)
        where T: Feature
        {
            var requestEntity = world.NewEntity();
            var pool = world.GetPool<r_AddFeature<T>>();
            
            ref var c_setupFeature = ref pool.Add(requestEntity);
            c_setupFeature.World = world;
            c_setupFeature.TargetEntity = world.PackEntity(targetEntity);
            c_setupFeature.RequestEntity = requestEntity;
        }

        /// <summary>
        /// Deletes feature for entity. After this call feature will not be executed for this entity.
        /// </summary>
        /// <param name="targetEntity"> Ecs entity from which feature will be removed </param>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveFeature<T>(this EcsWorld world, int targetEntity)
        {
            var childEntitiesPool = world.GetPool<c_ChildEntities>();
            ref var c_childEntities = ref childEntitiesPool.Get(targetEntity);
            
            if (c_childEntities.FeatureEntities.ContainsKey(typeof(T)))
            {
                var featureEntity = c_childEntities.FeatureEntities[typeof(T)];

                c_childEntities.FeatureEntities.Remove(typeof(T));
                world.DelEntity(featureEntity);
            }
            else
            {
                throw new System.Exception($"Entity {targetEntity} does not have feature {typeof(T)} added to it.");
            }
        }
    }
}