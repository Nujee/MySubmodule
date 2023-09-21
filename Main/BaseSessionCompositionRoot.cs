using System.Collections.Generic;
using Code.MySubmodule.Analytics;
using Code.MySubmodule.DebugTools.MyLogger;
using Code.MySubmodule.GameSettings;
using Code.MySubmodule.Pools;
using Code.MySubmodule.Services.LevelBoot;
using Code.MySubmodule.Utility.Constants;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.MySubmodule.Main
{
    [DisallowMultipleComponent]
    public class BaseSessionCompositionRoot : MonoBehaviour
    {
        [field: SerializeField] public AssetReference EcsConfig { get; private set; }
        
        private LevelService _levelService;
        private GameConfig _gameConfig;
        private BuildSettings _buildSettings;
        private ECSConfig _ecsConfig;
        private UIConfig _uiConfig;

        protected readonly List<object> _injectParameters = new List<object>();

        public async void Init(GameLoadType gameLoadType)
        {
            await LoadAndInjectConfigs();
            SetGameStartingParameters();
            LoadServices();
            InitializeAnalytics();
            await LoadAndInjectAdressables();
            await LoadAndInjectPools();

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
        
        protected virtual async UniTask LoadAndInjectPools() { }

        private void StartSceneLevelSystems()
        {
            FindObjectOfType<BaseSceneCompositionRoot>()
                .Init(_injectParameters, _gameConfig, _ecsConfig);
        }

        private async UniTask LoadAndInjectConfigs()
        {
            (_gameConfig, _buildSettings, _ecsConfig, _uiConfig) = await UniTask.WhenAll(
                Addressables.LoadAssetAsync<GameConfig>("GameConfig").ToUniTask(),
                Addressables.LoadAssetAsync<BuildSettings>("BuildSettings").ToUniTask(),
                EcsConfig.LoadAssetAsync<ECSConfig>().ToUniTask(),
                Addressables.LoadAssetAsync<UIConfig>("UIConfig").ToUniTask());

            _injectParameters.Add(_gameConfig);
            _injectParameters.Add(_buildSettings);
            _injectParameters.Add(_ecsConfig);
            _injectParameters.Add(_uiConfig);

            $"{Names.Submodule}: Configs have been loaded".Colored(Color.green).Log();
        }

        private void LoadServices()
        {
            _levelService = new LevelService(_buildSettings);
            _injectParameters.Add(_levelService);
            new PoolsSharedData();
        }
        
        private void SetGameStartingParameters()
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
            
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
            MyAnalytics.Initialize(-1);
#endif
        }
        
        private void LoadGameOnDevice(LevelService levelService)
        {
            levelService.FinishedLoadingScene += StartSceneLevelSystems;
            const float _firstLoadLogDelay = 3f;
            levelService.ReloadCurrentScene(_firstLoadLogDelay);
        }
        
        private void LoadGameForDevelopment(LevelService levelService)
        {
            StartSceneLevelSystems();
            levelService.FinishedLoadingScene += StartSceneLevelSystems;
        }

        // public void Dispose()
        // {
        //     // _sessionLifeTime?.Cancel();
        //     // Addressables.Release(_gameConfig);
        //     // Addressables.Release(_ecsConfig);
        //     // Addressables.Release(_buildSettings);
        //     // if (_levelService != null)
        //     // {
        //     //     _levelService.FinishedLoadingScene -= StartSceneLevelSystems;
        //     //     _levelService.Dispose();
        //     // }
        //     //
        //     // BlackCubeAnalytics.Dispose();
        //     _source.Dispose();
        // }
    }
}