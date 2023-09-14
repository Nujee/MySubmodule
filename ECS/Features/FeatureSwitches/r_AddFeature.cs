using Code.BlackCubeSubmodule.ECS.Features.RootEntity;
using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Code.BlackCubeSubmodule.ECS.Features.FeatureSwitches
{
    public struct r_AddFeature<T>
    where T: Feature
    {
        public EcsWorld World;
        public EcsPackedEntity TargetEntity;
        public int RequestEntity;

        public r_AddFeature(EcsWorld world, EcsPackedEntity targetEntity, int requestEntity)
        {
            World = world;
            TargetEntity = targetEntity;
            RequestEntity = requestEntity;
        }

        /// <summary>
        /// Adds feature to RootEntity.
        /// </summary>
        [PublicAPI]
        public void MarkFeatureAdded(int featureEntity)
        {
            var childEntitiesPool = World.GetPool<c_ChildEntities>();
            
            if (childEntitiesPool.Has(featureEntity))
            {
                throw new System.Exception("Feature entity can't be root entity.");
            }

            if (TargetEntity.Unpack(World, out var targetEntity))
            {
                if (!childEntitiesPool.Has(targetEntity))
                {
                    throw new System.Exception("Provided entity is not root entity.");
                }
                
                ref var c_childEntities = ref childEntitiesPool.Get(targetEntity);
                c_childEntities.FeatureEntities.Add(typeof(T), featureEntity);
                
                World.DelEntity(RequestEntity);
            }
        }
    }
}