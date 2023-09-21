using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Code.MySubmodule.ECS.ExtensionMethods
{
    public static class EcsPoolExtensions
    {
        /// <summary>
        /// Returns component from pool for provided entity.
        /// If entity has no such component it will be added. 
        /// </summary>
        [PublicAPI]
        public static ref T TryGet<T>(this EcsPool<T> pool, int entity)
        where T: struct
        {
            if (pool.Has(entity))
            {
                return ref pool.Get(entity);
            }
            else
            {
                return ref pool.Add(entity);
            }
        }
        
        /// <summary>
        /// If entity has component it will be removed and new component will be added and returned by reference. 
        /// </summary>
        [PublicAPI]
        public static ref T Replace<T>(this EcsPool<T> pool, int entity)
            where T: struct
        {
            if (pool.Has(entity))
            {
                pool.Del(entity);
            }

            return ref pool.Add(entity);
        }
    }
}