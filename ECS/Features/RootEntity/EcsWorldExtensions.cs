using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Code.BlackCubeSubmodule.ECS.Features.RootEntity
{
    public static class EcsWorldExtensions
    {
        /// <summary>
        /// Makes entity root.
        /// Depth of the tree can only be 1.  
        /// </summary>
        /// <param name="world"></param>
        /// <param name="targetEntity"></param>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MakeRootEntity(this EcsWorld world, int targetEntity)
        {
            var childEntitiesPool = world.GetPool<c_ChildEntities>();
            
            ref var c_childEntities = ref childEntitiesPool.Add(targetEntity);
            c_childEntities.FeatureEntities = new Dictionary<System.Type, int>();
        }

        /// <summary>
        /// Will destroy targetEntity and all its child entities.
        /// If entity is not root entity it will still be destroyed.
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelRootEntity(this EcsWorld world, int targetEntity)
        {
            var childEntitiesPool = world.GetPool<c_ChildEntities>();

            if (childEntitiesPool.Has(targetEntity))
            {
                ref var c_childEntities = ref childEntitiesPool.Get(targetEntity);
                foreach (var pair in c_childEntities.FeatureEntities)
                {
                    world.DelEntity(pair.Value);
                }
            }
            
            world.DelEntity(targetEntity);
        }
    }
}