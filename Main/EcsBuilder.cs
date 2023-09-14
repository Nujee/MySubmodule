using System;
using System.Collections.Generic;
using Code.BlackCubeSubmodule.DebugTools.BlackCubeLogger;
using Code.BlackCubeSubmodule.ECS;
using Code.BlackCubeSubmodule.ECS.Features;
using Code.BlackCubeSubmodule.ECS.Features.FeatureSwitches;
using Code.BlackCubeSubmodule.GameConfigs.AdressablesConfigs;
using Code.BlackCubeSubmodule.Pools;
using Code.BlackCubeSubmodule.Services.LifeTime;
using Code.BlackCubeSubmodule.Services.UI.Views;
using Code.BlackCubeSubmodule.Utility.Constants;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.BlackCubeSubmodule.Main
{
    /// <summary>
    /// Build ECS systems and runs them. 
    /// User should use LevelECS instead of this class. 
    /// </summary>
    public sealed class EcsBuilder : IDisposable
    {
        private readonly EcsWorld _world;
        private readonly Dictionary<SystemType, EcsSystems> _systems = new();

        // private EcsSystems _featureInitSystemsContainer;

        /// <summary>
        /// This method is substitute for constructor.
        /// It builds ECS by adding systems and injecting dependencies. 
        /// </summary>
        public EcsBuilder(List<object> injectParameters, ECSConfig ecsConfig)
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
            
            AddFeatures(ecsConfig);
            
            AddViewSystems();

#if UNITY_EDITOR
            AddDebugSystems();
#endif
            
            Inject(injectParameters);

            foreach (var systems in _systems)
            {
                systems.Value.Init();
            }
            
            StartPlayerLoop(_world, ecsConfig);
            
            $"{Names.Submodule}: {nameof(EcsBuilder)} finished initialization"
                .Colored(Color.green)
                .Log();
        }

        /// <summary>
        /// Separate initialization of InitSystems and other features.
        /// Each system in some feature is added to a specific systemGroup (update, fixed, late or some custom)
        /// </summary>
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
                        if (featureContainer.AddToEcs)
                        {
                            var currentSystems = _systems[featureGroup.SystemType];
                            var currentFeature = featureContainer.Feature;
                            currentSystems.AddGroup(currentFeature.GetType().Name, false, null, currentFeature);
                        }
                    }
                }
            }
        }

        // This method is purely for DI into view. 
        private void AddViewSystems()
        {
            var views = Object.FindObjectsOfType<View>(true);
            var initSystems = _systems[SystemType.Init];
            for (var i = 0; i < views.Length; i++)
            {
                initSystems.Add(views[i]);
            }
        }

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
        
        private void StartPlayerLoop(EcsWorld world, ECSConfig ecsConfig)
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
            
            foreach (var featureGroup in ecsConfig.FeatureGroups)
            {
                if (featureGroup.Features is not null)
                {
                    foreach (var featureContainer in featureGroup.Features)
                    {
                        if (featureContainer is { AddToEcs: true, InitialFeatureState: InitialFeatureState.Active })
                        {
                            var featureName = featureContainer.Feature.GetType().Name;
                            EcsWorldExtensionsFeatureSwitches.ChangeSystemGroupState(world, featureName, true);
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var system in _systems)
            {
                system.Value.Destroy();
            }

            _systems.Clear();

            _world.Destroy();
        }
    }
}