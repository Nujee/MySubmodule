using System;
using System.Collections.Generic;
using Code.BlackCubeSubmodule.DebugTools.BlackCubeLogger;
using Code.BlackCubeSubmodule.Services.UI.UiFrame;
using Code.BlackCubeSubmodule.Services.UI.Views;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.BlackCubeSubmodule.Pools
{
    public sealed class UiPool
    {
        private readonly Queue<View> _availablePooledObjects = new ();
        private readonly List<View> _allPooledObjects = new ();

#if UNITY_EDITOR
        private int _totalObjectedPooled;
#endif

        private GameObject _prefab;
        private Transform _poolParent;
        private int _refillCount;

        /// <summary>
        /// Type of the View class child attached to prefab.
        /// </summary>
        public Type PoolType { get; private set; }
        
        private UiPool() { }

        public static async UniTask<UiPool> New(UiPoolSettings settings)
        {
            var pool = new UiPool
            {
                _prefab = await settings.Prefab.LoadAssetAsync<GameObject>().ToUniTask(),
            };
            
            pool.PoolType = pool._prefab.GetComponent<View>().GetType();

            var poolParent = new GameObject
            {
#if UNITY_EDITOR
                name = $"{settings.Prefab.Asset.name} Pool"
#endif
            };

            pool._poolParent = poolParent.transform;
            pool._poolParent.SetParent(PoolsSharedData.UiPoolCommonParent);

            pool.FillPool(settings.StartingSize);

            return pool;
        }

        // /// <summary>
        // /// Get View form pool.
        // /// </summary>
        // [PublicAPI]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public View Get()
        // {
        //     return Get(Vector3.zero, Quaternion.identity);
        // }
        //
        // /// <summary>
        // /// Get View form pool.
        // /// </summary>
        // [PublicAPI]
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public View Get(Vector3 position)
        // {
        //     return Get(position, Quaternion.identity);
        // }
        
        /// <summary>
        /// Get View form pool.
        /// </summary>
        [PublicAPI]
        public View Get(/*Vector3 position, Quaternion rotation, Transform newParent = null*/)
        {
            if (_availablePooledObjects.Count < 1)
            {
                $"{_poolParent.gameObject.name} pool was emptied, refilling to {_allPooledObjects.Count + _refillCount}. Consider increasing pool starting size."
                    .Colored(Color.yellow)
                    .Log(_poolParent.gameObject);
                
                FillPool(_refillCount);
            }

            var pooledObject = _availablePooledObjects.Dequeue();

            return pooledObject;
        }

        /// <summary>
        /// Returns object to the pool.
        /// </summary>
        [PublicAPI]
        public void Return(View view)
        {
            view.transform.SetParent(_poolParent);

            _availablePooledObjects.Enqueue(view);
        }

        /// <summary>
        /// Returns all views to the pool and closes them.
        /// </summary>
        public void Reset()
        {
            _allPooledObjects.Clear();
            foreach (var view in _allPooledObjects)
            {
                view.Close();
                _availablePooledObjects.Enqueue(view);
            }
        }

        private void FillPool(int refillSize)
        {
            for (int i = 0; i < refillSize; i++)
            {
                var newPooledObject = Object.Instantiate(_prefab, _poolParent, true);

#if UNITY_EDITOR
                newPooledObject.gameObject.name = $"{_prefab.name} ({_totalObjectedPooled++})";
#endif

                var newView = newPooledObject.GetComponent<View>();
                newView.Close();
                _allPooledObjects.Add(newView);
                _availablePooledObjects.Enqueue(newView);
            }
        }
    }
}