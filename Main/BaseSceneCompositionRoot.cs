using System.Collections.Generic;
using Code.MySubmodule.GameSettings;
using Code.MySubmodule.Services.Camera;
using Code.MySubmodule.Services.Effects;
using Code.MySubmodule.Services.LifeTime;
using Code.MySubmodule.Services.UI;
using Code.MySubmodule.Services.UI.ScreenService;
using Code.MySubmodule.Services.UI.UILayers.Markers;
using Code.MySubmodule.Services.UI.Views;
using DG.Tweening;
using UnityEngine;

namespace Code.MySubmodule.Main
{
    /// <summary>
    /// Loads and prepares for injection all level dependencies.
    /// User should use LevelComposition root instead of this class. 
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ECS), typeof(LifeTimeService))]
    public class BaseSceneCompositionRoot : MonoBehaviour
    {
        // May be move it to session composition root?
        protected readonly List<object> _injectParameters = new List<object>();

        /// <summary>
        /// This method is substitute for constructor.
        /// </summary>
        public void Init(List<object> injectParameters, GameConfig gameConfig, ECSConfig ecsConfig)
        {
            DOTween.SetTweensCapacity(gameConfig.TweenCapacity, gameConfig.SequenceCapacity);

            _injectParameters.AddRange(injectParameters);
            _injectParameters.Add(Camera.main);
            
            PrepareServicesForInjection();
            InitUI();
            InjectSceneObjects();
            InitECS(ecsConfig);
        }

        private void PrepareServicesForInjection()
        {
            var lifeTimeService = GetComponent<LifeTimeService>();
            lifeTimeService.Init();
            
            EffectService.Init();
            
            var cameraService = new CameraService(default);
            _injectParameters.Add(cameraService);
            
            var screenService = FindObjectOfType<ScreenService>();
            screenService.Initialise();
            _injectParameters.Add(screenService);
        }

        private void InitUI()
        {
            var uiConfigs = FindObjectsOfType<ViewConfig>();
            foreach (var config in uiConfigs)
            {
                _injectParameters.Add(config);
            }
            
            _injectParameters.Add(FindObjectOfType<OverlayLayer>());

            var views = FindObjectsOfType<View>(true);
            foreach (var view in views)
            {
                _injectParameters.Add(view);
            }
        }
        
        // Should be left empty. 
        protected virtual void InjectSceneObjects() { }

        private void InitECS(ECSConfig ecsConfig)
        {
            var ecsBuilder = GetComponent<ECS>();
            ecsBuilder.InitECS(_injectParameters, ecsConfig);
            LifeTimeService.AddToDisposable(ecsBuilder);
        }
    }
}