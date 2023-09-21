using System;
using System.Collections.Generic;
using Code.MySubmodule.ECS;
using Code.MySubmodule.ECS.LevelEntity;
using Code.MySubmodule.GameSettings;
using Code.MySubmodule.Pools;
using Code.MySubmodule.Services.LifeTime;
using Code.MySubmodule.Services.UI.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UniRx;
using UnityEngine;

namespace Code.MySubmodule.Main
{
    /// <summary>
    /// Build ECS systems and runs them. 
    /// User should use LevelECS instead of this class. 
    /// </summary>
    public class ECS : MonoBehaviour ,IDisposable
    {
        protected readonly Dictionary<SystemType, EcsSystems> _systems = new();
        
        private EcsWorld _world;

        /// <summary>
        /// This method is substitute for constructor.
        /// It builds ECS by adding systems and injecting dependencies. 
        /// </summary>
        public void InitECS(List<object> injectParameters, ECSConfig ecsConfig)
        {
            _systems.Clear();
            _world = new EcsWorld();

            foreach (var parameter in injectParameters)
            {
                if (parameter is Pool pool) pool.LinkWithCurrentScene(_world);
            }

            var systemTypes = Enum.GetValues(typeof(SystemType));
            foreach (var item in systemTypes)
            {
                _systems.Add((SystemType)item, new EcsSystems(_world));
            }
            
            AddInternalGameplaySystems();
            
            AddFeatures(ecsConfig);
            
            AddViewSystems();

            AddGameplaySystems();
            
#if UNITY_EDITOR
            AddDebugSystems();
#endif

            Inject(injectParameters);

            foreach (var system in _systems)
            {
                system.Value.Init();
            }
            
            StartPlayerLoop();
        }

        private void AddInternalGameplaySystems()
        {
            _systems[SystemType.Init]
                .Add(new i_Level());
        }

        private void AddFeatures(ECSConfig ecsConfig)
        {
            foreach (var system in ecsConfig.InitSystems)
            {
                if (system is not null)
                {
                    _systems[SystemType.Init].Add(system); 
                }
            }
            
            foreach (var featureGroup in ecsConfig.FeatureGroups)
            {
                if (featureGroup.Features is not null)
                {
                    foreach (var featureContainer in featureGroup.Features)
                    {
                        if (featureContainer.IsActive)
                        {
                            featureContainer.Feature.Init(_systems[featureGroup.SystemType]);
                        }
                    }
                }
            }
        }

        private void AddViewSystems()
        {
            var views = FindObjectsOfType<View>(true);
            var initSystems = _systems[SystemType.Init];
            for (var i = 0; i < views.Length; i++)
            {
                initSystems.Add(views[i]);
            }
        }

        protected virtual void AddGameplaySystems() { }

        private void AddDebugSystems()
        {
#if UNITY_EDITOR
            _systems[SystemType.Update].Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());   
#endif
        }

        private void Inject(List<object> injectParameters)
        {
            var injectArray = injectParameters.ToArray();
            foreach (var system in _systems)
            {
                system.Value.Inject(injectArray);
            }
        }
        
        protected virtual void StartPlayerLoop()
        {
            var disposable = LifeTimeService.GetDisposable();
            
            Observable.EveryUpdate()
                .Subscribe(_ => _systems[SystemType.Update].Run())
                .AddTo(disposable);
            
            Observable.EveryLateUpdate()
                .Subscribe(_ => _systems[SystemType.LateUpdate].Run())
                .AddTo(disposable);
            
            Observable.EveryFixedUpdate()
                .Subscribe(_ => _systems[SystemType.FixedUpdate].Run())
                .AddTo(disposable);
        }

        public void Dispose()
        {
            _world.Destroy();
            foreach (var system in _systems)
            {
                system.Value.Destroy();
            }
        }
    }
}