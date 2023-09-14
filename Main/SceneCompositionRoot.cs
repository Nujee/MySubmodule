using System.Collections.Generic;
using Code.BlackCubeSubmodule.ECS.DependencyInjection;
using Code.BlackCubeSubmodule.GameConfigs.AdressablesConfigs;
using Code.BlackCubeSubmodule.Services.Camera;
using Code.BlackCubeSubmodule.Services.Effects;
using Code.BlackCubeSubmodule.Services.LifeTime;
using JoystickPack;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Main
{
    /// <summary>
    /// Loads and prepares for injection all level dependencies.
    /// User should use LevelComposition root instead of this class. 
    /// </summary>
    public sealed class SceneCompositionRoot
    {
        private static readonly List<object> _injectParameters = new List<object>();
        
        public SceneCompositionRoot(List<object> injectParameters, ECSConfig ecsConfig)
        {
            _injectParameters.Clear();
            _injectParameters.AddRange(injectParameters);

            PrepareServicesForInjection();
            InjectIShouldBeInjectedInheritors();
            InjectSceneObjects();
            InitEcs(ecsConfig);
        }

        private void PrepareServicesForInjection()
        {
            var lifeTimeGameObject = new GameObject
            {
                #if UNITY_EDITOR
                name = "LifeTimeService"
                #endif
            };
            var lifeTimeService = lifeTimeGameObject.AddComponent<LifeTimeService>();
            lifeTimeService.Init();
            
            EffectService.Init();
            
            var cameraService = new CameraService(default);
            _injectParameters.Add(cameraService);
        }

        private void InjectIShouldBeInjectedInheritors()
        {
            var iInjectables = Object.FindObjectsOfType<MonoBehaviour>();
            foreach (var monoBehaviour in iInjectables)
            {
                if (monoBehaviour is IShouldBeInjected)
                {
                    _injectParameters.Add(monoBehaviour);
                }    
            }
        }

        private void InjectSceneObjects()
        {
            _injectParameters.Add(Camera.main);
            
            var joystick = Object.FindObjectOfType<Joystick>();
            if (joystick)
            {
                _injectParameters.Add(joystick);   
            }
        }

        private void InitEcs(ECSConfig ecsConfig)
        {
            var ecsBuilder = new EcsBuilder(_injectParameters, ecsConfig);
            LifeTimeService.AddToDisposable(ecsBuilder);
        }
    }
}