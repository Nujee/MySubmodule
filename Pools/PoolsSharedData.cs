using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Pools
{
    public class PoolsSharedData
    {
        public const int DefaultStartingSize = 16;
        public const int DefaultRefillCount = 8;

        private static readonly List<IInjectablePool> _persistentPools = new List<IInjectablePool>();

        public static ReadOnlyCollection<IInjectablePool> PersistentPools => _persistentPools.AsReadOnly();
        
        public static Transform PoolCommonParent { get; private set; }
        public static Transform UiPoolCommonParent { get; private set; }
        
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
            
            var uiPoolCommonParent = new GameObject
            {
#if UNITY_EDITOR
                name = "UI_POOL_COMMON_PARENT"
#endif
            };
            
            Object.DontDestroyOnLoad(uiPoolCommonParent);
            UiPoolCommonParent = uiPoolCommonParent.transform;
        }

        public static void Inject(IInjectablePool pool)
        {
            _persistentPools.Add(pool);
        }
    }
}
