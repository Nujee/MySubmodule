using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Code.MySubmodule.Pools
{
    public class PoolsSharedData
    {
        public const int DefaultStartingSize = 16;
        public const int DefaultRefillCount = 8;

        private static readonly List<IMyPool> _persistentPools = new List<IMyPool>();

        public static ReadOnlyCollection<IMyPool> PersistentPools => _persistentPools.AsReadOnly();
        
        public static Transform PoolCommonParent { get; private set; }
        
        public PoolsSharedData()
        {
            var poolCommonParent = new GameObject
            {
#if UNITY_EDITOR
                name = "POOL_COMMON_PARENT"
#endif
            };
            
            Object.DontDestroyOnLoad(poolCommonParent);
            
            PoolCommonParent = poolCommonParent.transform;
        }

        public static void Inject(IMyPool pool)
        {
            _persistentPools.Add(pool);
        }
    }
}
