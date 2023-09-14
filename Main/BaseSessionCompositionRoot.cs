using System.Collections.Generic;
using Code.BlackCubeSubmodule.DebugTools.BlackCubeLogger;
using Code.BlackCubeSubmodule.Analytics;
using Code.BlackCubeSubmodule.GameConfigs.AdressablesConfigs;
using Code.BlackCubeSubmodule.Pools;
using Code.BlackCubeSubmodule.Services.LevelBoot;
using Code.BlackCubeSubmodule.Services.UI.ScreenService;
using Code.BlackCubeSubmodule.Services.UI.UiFrame;
using Code.BlackCubeSubmodule.Services.UI.Views;
using Code.BlackCubeSubmodule.Unity.ComponentsExtensions;
using Code.BlackCubeSubmodule.Utility.Constants;
using Code.Game.Constants.GeneratedCode;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.BlackCubeSubmodule.Main
{
    
#pragma warning disable 1998
    
    [DisallowMultipleComponent]
    public class BaseSessionCompositionRoot : MonoBehaviour
    {
        private LevelService _levelService;
        private GameConfig _gameConfig;
        private BuildConfig _buildConfig;
        private ECSConfig _ecsConfig;
        private UiPoolsConfig _uiPoolsConfig;

        protected readonly List<object> _injectParameters = new ();

        public async void Init(GameLoadType gameLoadType)
        {
            await LoadAndInjectConfigs();
            SetGameStartingParameters();
            LoadServices();
            await LoadAndInjectUi();
            InitializeAnalytics();
            await LoadAndInjectAdressables();
            await LoadAndInjectPools();
            InitializeFeatures(_ecsConfig);

            _injectParameters.AddRange(PoolsSharedData.PersistentPools);

            switch (gameLoadType)
            {
                case GameLoadType.OnDeviceLoad:
                    LoadGameOnDevice(_levelService);
                    break;
                case GameLoadType.DevelopmentLoad:
                    LoadGameForDevelopment(_levelService);
                    break;
            }
        }
        
        // Protected methods should be left empty. 
        protected virtual async UniTask LoadAndInjectAdressables() { }
        
        // Protected methods should be left empty. 
        protected virtual async UniTask LoadAndInjectPools() { }

        private async UniTask LoadAndInjectConfigs()
        {
            (_gameConfig, _buildConfig, _ecsConfig, _uiPoolsConfig) = await UniTask.WhenAll(
                Addressables.LoadAssetAsync<GameConfig>("GameConfig").ToUniTask(),
                Addressables.LoadAssetAsync<BuildConfig>("BuildConfig").ToUniTask(),
                Addressables.LoadAssetAsync<ECSConfig>("ECSConfig").ToUniTask(),
                Addressables.LoadAssetAsync<UiPoolsConfig>("UiPoolsConfig").ToUniTask());

            _injectParameters.Add(_gameConfig);
            _injectParameters.Add(_buildConfig);
            _injectParameters.Add(_ecsConfig);
            _injectParameters.Add(_uiPoolsConfig);

            $"{Names.Submodule}: Configs have been loaded".Colored(Color.green).Log();
        }

        private async UniTask LoadAndInjectUi()
        {
            var prefab = await Addressables.LoadAssetAsync<GameObject>("UiFrame").ToUniTask();
            var uiFrame = Instantiate(prefab, Vector3.zero, Quaternion.identity).transform;
            DontDestroyOnLoad(uiFrame);

#if UNITY_EDITOR
            uiFrame.gameObject.name = UiNames.UIFrameGameObjectName;
#endif

            var uiCamera = uiFrame.GetComponentInChildren<Camera>();
            var background = uiFrame.GetChildWithTag(Tags.UIBackgroundLayer);
            var windows = uiFrame.GetChildWithTag(Tags.UIWindowsLayer);
            var overlay = uiFrame.GetChildWithTag(Tags.UIOverlayLayer);
            
            _injectParameters.Add(new UiFrameData(uiCamera, background, windows, overlay));
            
            var views = FindObjectsOfType<View>(true);
            foreach (var view in views)
            {
                _injectParameters.Add(view);
            }

            var screenService = await ScreenService.New(views, _uiPoolsConfig);
            _injectParameters.Add(screenService);
        }

        private void LoadServices()
        {
            _levelService = new LevelService(_buildConfig);
            _injectParameters.Add(_levelService);
            new PoolsSharedData();
        }
        
        private void SetGameStartingParameters()
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
            DOTween.SetTweensCapacity(_gameConfig.TweenCapacity, _gameConfig.SequenceCapacity);
            
#if UNITY_EDITOR
            Time.timeScale = _gameConfig.TimeScale;
            Cursor.lockState = _gameConfig.EditorCursorMode;
#endif
        }

        private void InitializeAnalytics()
        {
#if UNITY_IOS && !UNITY_EDITOR
            var nonDeterminedATTStatus = ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED;
            if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == nonDeterminedATTStatus) 
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking(OctavianAnalytics.Initialize);
            }         
#else
            BlackCubeAnalytics.Initialize(-1);
#endif
        }
        
        private void LoadGameOnDevice(LevelService levelService)
        {
            levelService.FinishedLoadingScene += StartSceneLevelSystems;
            const float _firstLoadLogDelay = 3f;
            levelService.ReloadCurrentScene(_firstLoadLogDelay);
        }
        
        /// <summary>
        /// Prepares features for ECSBuilder.
        /// Only single Init() call is needed per session. 
        /// </summary>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
        private void InitializeFeatures(ECSConfig ecsConfig)
        {
            foreach (var featureGroup in ecsConfig.FeatureGroups)
            {
                if (featureGroup.Features is not null)
                {
                    foreach (var featureContainer in featureGroup.Features)
                    {
                        if (featureContainer.AddToEcs)
                        {
                            featureContainer.Feature.Init();
                        }
                    }
                }
            }
        }
        
        private void LoadGameForDevelopment(LevelService levelService)
        {
            StartSceneLevelSystems();
            levelService.FinishedLoadingScene += StartSceneLevelSystems;
        }
        
        private void StartSceneLevelSystems()
        {
            new SceneCompositionRoot(_injectParameters, _ecsConfig);
        }
    }
}