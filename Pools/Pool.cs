using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Code.BlackCubeSubmodule.DebugTools.BlackCubeLogger;
using Code.BlackCubeSubmodule.ECS.Components.UnityComponents;
using Code.BlackCubeSubmodule.Services.LifeTime;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Code.BlackCubeSubmodule.Pools
{
    /// <summary>
    /// Pool specialized for work with ECS.
    /// Pool creates ECS entities on its own. Each created entity will have c_Transform attached to it. 
    /// </summary>
    public class Pool : IDisposable, IInjectablePool
    {
        private readonly Queue<GameObject> _availablePooledObjects = new();
        private readonly List<GameObject> _allPooledObjects = new();
        
        private int _startingSize = PoolsSharedData.DefaultStartingSize;
        private int _refillCount = PoolsSharedData.DefaultRefillCount;

#if UNITY_EDITOR
        private int _totalObjectedPooled;
#endif

        private EcsWorld _ecsWorld;
        private Transform _poolParent;
        private GameObject _prefabLoadHandle;

        public GameObject Prefab => _prefabLoadHandle;

        /// <summary>
        /// Changes pool starting size. Should be called before Init().
        /// </summary>
        [PublicAPI]
        public Pool SetStartingSize(int startingSize)
        {
            _startingSize = startingSize;
            return this;
        }

        /// <summary>
        /// Changes pool refill count.  
        /// </summary>
        [PublicAPI]
        public Pool SetRefillCount(int refillCount)
        {
            _refillCount = refillCount;
            return this;
        }

        /// <summary>
        /// Inits pool with essential parameters. Should be called in SessionCompositionRoot.
        /// </summary>
        public async UniTask<Pool> Init(string prefabKey)
        {
            var prefab = await Addressables.LoadAssetAsync<GameObject>(prefabKey);
            return Init(prefabKey, prefab);
        }
        
        /// <summary>
        /// Inits pool with essential parameters. Should be called in SessionCompositionRoot.
        /// </summary>
        public async UniTask<Pool> Init(AssetReference assetReference)
        {
            var prefab = await assetReference.LoadAssetAsync<GameObject>();
            return Init(assetReference.Asset.name, prefab);
        }
        
        private Pool Init(string poolName, GameObject prefab)
        {
            var poolParent = new GameObject
            {
#if UNITY_EDITOR
                name = $"{poolName} Pool"
#endif
            };
            
            _poolParent = poolParent.transform;
            _poolParent.SetParent(PoolsSharedData.PoolCommonParent);

            _prefabLoadHandle = prefab;
            FillPool(_startingSize);
            
            PoolsSharedData.Inject(this);
            return this;
        }

        public void LinkWithCurrentScene(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
            LifeTimeService.AddToDisposable(this);
        }
        
        /// <summary>
        /// Get Object form pool.
        /// Object will automatically be enabled. 
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return Get(Vector3.zero, Quaternion.identity);
        }
        
        /// <summary>
        /// Get Object form pool.
        /// Object will automatically be enabled. 
        /// </summary>
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get(Vector3 position)
        {
            return Get(position, Quaternion.identity);
        }

        /// <summary>
        /// Get Object form pool.
        /// Object will automatically be enabled. 
        /// </summary>
        [PublicAPI]
        public int Get(Vector3 position, Quaternion rotation, Transform newParent = null)
        {
            if (_availablePooledObjects.Count < 1)
            {
                $"{_poolParent.gameObject.name} pool was emptied, refilling to {_allPooledObjects.Count + _refillCount}. Consider increasing pool starting size."
                    .Colored(Color.yellow)
                    .Log(_poolParent.gameObject);
                
                FillPool(_refillCount);
            }

            var pooledObject = _availablePooledObjects.Dequeue();
            var entity = _ecsWorld.NewEntity();

            pooledObject.transform.position = position;
            pooledObject.transform.SetParent(newParent);
            pooledObject.transform.rotation = rotation;
            pooledObject.transform.gameObject.SetActive(true);
            
            ref var c_transform = ref _ecsWorld.GetPool<c_Transform>().Add(entity);
            c_transform.Transform = pooledObject.transform;

            return entity;
        }
        
        /// <summary>
        /// Returns object to the pool.
        /// Object will automatically be disabled. 
        /// </summary>
        [PublicAPI]
        public void Return(int entity)
        {
            ref var c_transform = ref _ecsWorld.GetPool<c_Transform>().Get(entity);
            var pooledObject = c_transform.Transform.gameObject;
            pooledObject.transform.SetParent(_poolParent);
            pooledObject.SetActive(false);

            _availablePooledObjects.Enqueue(pooledObject);
            
            _ecsWorld.DelEntity(entity);
        }

        private void FillPool(int refillSize)
        {
            for (int i = 0; i < refillSize; i++)
            {
                var newPooledObject = Object.Instantiate(_prefabLoadHandle, _poolParent, true);

#if UNITY_EDITOR
                newPooledObject.gameObject.name = $"{_prefabLoadHandle.name} ({_totalObjectedPooled++})";
#endif

                newPooledObject.gameObject.SetActive(false);
                _allPooledObjects.Add(newPooledObject);
                _availablePooledObjects.Enqueue(newPooledObject);
            }
        }

        public override string ToString()
        {
            return _poolParent.gameObject.name;
        }

        public void Dispose()
        {
            foreach (var pooledObject in _allPooledObjects)
            {
                if (pooledObject)
                {
                    pooledObject.SetActive(false);   
                }
            }
            
            _availablePooledObjects.Clear();
            
            foreach (var pooledObject in _allPooledObjects)
            {
                _availablePooledObjects.Enqueue(pooledObject);
            }
        }
    }
}