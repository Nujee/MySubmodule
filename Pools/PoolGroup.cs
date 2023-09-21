using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Code.MySubmodule.Pools
{
    /// <summary>
    /// AS OF NOW THERE IS NO SUPPORT FOR OVERLOADING METHODS.
    /// THE EASIEST WAY TO ADD IS TO START REUSING ECS WORLD BETWEEN SCENES. 
    /// </summary>
    /// <typeparam name="TPoolID"></typeparam>
    public class PoolGroup<TPoolID> : IMyPool
    where TPoolID: Enum
    {
        private readonly Dictionary<TPoolID, Pool> _internalPools = new Dictionary<TPoolID, Pool>();
        
        private int _startingSize = PoolsSharedData.DefaultStartingSize;
        private int _refillCount = PoolsSharedData.DefaultRefillCount;

        /// <summary>
        /// Changes starting size of all pools in this poolGroup. Should be called before Init().
        /// </summary>
        [PublicAPI]
        public PoolGroup<TPoolID> SetStartingSize(int startingSize)
        {
            _startingSize = startingSize;
            
            return this;
        }

        /// <summary>
        /// Changes refill count of all pools in this poolGroup. Should be called before Init().
        /// </summary>
        [PublicAPI]
        public PoolGroup<TPoolID> SetRefillCount(int refillCount)
        {
            _refillCount = refillCount;
            
            return this;
        }

        /// <summary>
        /// Inits poolGroup with essential parameters. Should be called in SessionCompositionRoot.
        /// </summary>
        [PublicAPI]
        public async UniTask<PoolGroup<TPoolID>> Init(params (TPoolID poolID, string prefabKey)[] pairs)
        {
            // This can cause long load time. But i can't figure out how to await all pools at once.
            foreach (var (poolID, prefabKey) in pairs)
            {
                var pool = await new Pool()
                    .SetStartingSize(_startingSize)
                    .SetRefillCount(_refillCount)
                    .Init(prefabKey);

                _internalPools.Add(poolID, pool);
            }

            PoolsSharedData.Inject(this);
            
            return this;
        }
        
        [PublicAPI]
        public Pool this[TPoolID index] => _internalPools[index];
    }
}