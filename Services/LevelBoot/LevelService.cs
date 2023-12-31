﻿using System;
using Code.MySubmodule.Analytics;
using Code.MySubmodule.DebugTools.MyLogger;
using Code.MySubmodule.GameSettings;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Code.MySubmodule.Services.LevelBoot
{
    public sealed class LevelService : IDisposable
    {
        private const string CurrentLevelIndex = "LevelIndexToLoad";
        private const string PreviousLevelIndex = "LastStartedLevel";
        private const string TotalLevelsPlayed = "TotalLevelsPlayed";

        private readonly BuildSettings _buildSettings;

        private int _currentLevelIndex = 0;
        private int _previousLevelIndex = -1;
        private int _totalLevelsPlayed = 1;

        private AsyncOperationHandle<SceneInstance> _loadHandle;

        public event Action FinishedLoadingScene;
        public event Action OnLoadNextScene = () => { };

        public LevelService(BuildSettings buildSettings)
        {
            _buildSettings = buildSettings;

            if (PlayerPrefs.HasKey(CurrentLevelIndex))
            {
                _currentLevelIndex = PlayerPrefs.GetInt(CurrentLevelIndex);
            }

            if (PlayerPrefs.HasKey(PreviousLevelIndex))
            {
                _previousLevelIndex = PlayerPrefs.GetInt(PreviousLevelIndex);
            }

            if (PlayerPrefs.HasKey(TotalLevelsPlayed))
            {
                _totalLevelsPlayed = PlayerPrefs.GetInt(TotalLevelsPlayed);
            }
        }

        /// <summary>
        /// Reloads current scene. How this happens depends on GameConfig parameters and whether game is running in Editor or not. 
        /// </summary>
        [PublicAPI]
        public void ReloadCurrentScene(float logDelay = 0f)
        {
#if UNITY_EDITOR
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                "Error. Loading from Main Scene in Editor is not supported".Colored(Color.red).Log();
            }
            else
            {
                var activeSceneName = SceneManager.GetActiveScene().name;
                var handle = SceneManager.LoadSceneAsync(activeSceneName);
                handle.completed += _ =>
                {
                    if (_currentLevelIndex == _previousLevelIndex)
                        MyAnalytics.LogLevelRestart(_totalLevelsPlayed, logDelay);
                    else MyAnalytics.LogLevelStart(_totalLevelsPlayed, logDelay);
                };
                handle.completed += _ =>
                {
                    _previousLevelIndex = _currentLevelIndex;
                    SavePlayerPrefs();
                };
                handle.completed += _ => FinishedLoadingScene.Invoke();
            }
#else
            ReloadScene();
#endif
            void ReloadScene()
            {
                _loadHandle =
                    Addressables.LoadSceneAsync(_buildSettings.Scenes[_currentLevelIndex], LoadSceneMode.Single);
                _loadHandle.Completed += _ =>
                {
                    if (_currentLevelIndex == _previousLevelIndex)
                        MyAnalytics.LogLevelRestart(_totalLevelsPlayed, logDelay);
                    else MyAnalytics.LogLevelStart(_totalLevelsPlayed, logDelay);
                };
                _loadHandle.Completed += _ =>
                {
                    _previousLevelIndex = _currentLevelIndex;
                    SavePlayerPrefs();
                };
                _loadHandle.Completed += _ => FinishedLoadingScene.Invoke();
            }
        }

        /// <summary>
        /// Loads next scene (currentSceneIndex + 1) from BlackCube's BuildSettings.  
        /// </summary>
        [PublicAPI]
        public void LoadNextScene()
        {
            OnLoadNextScene.Invoke();
            
            var nextSceneIndex = _currentLevelIndex + 1 >= _buildSettings.Scenes.Length
                ? _buildSettings.LevelLoopFirstIndex
                : _currentLevelIndex + 1;

            MyAnalytics.LogLevelComplete(_totalLevelsPlayed);

            _loadHandle = Addressables.LoadSceneAsync(_buildSettings.Scenes[nextSceneIndex], LoadSceneMode.Single);
            _loadHandle.Completed += UpdateLevelData;
            _loadHandle.Completed += _ => MyAnalytics.LogLevelStart(_totalLevelsPlayed);
            _loadHandle.Completed += _ => FinishedLoadingScene.Invoke();
        }

        private void UpdateLevelData(AsyncOperationHandle<SceneInstance> _)
        {
            _currentLevelIndex = _currentLevelIndex + 1 >= _buildSettings.Scenes.Length
                ? _buildSettings.LevelLoopFirstIndex
                : _currentLevelIndex + 1;
            _previousLevelIndex = _currentLevelIndex;
            _totalLevelsPlayed++;

            SavePlayerPrefs();
        }

        private void SavePlayerPrefs()
        {
            PlayerPrefs.SetInt(CurrentLevelIndex, _currentLevelIndex);
            PlayerPrefs.SetInt(PreviousLevelIndex, _previousLevelIndex);
            PlayerPrefs.SetInt(TotalLevelsPlayed, _totalLevelsPlayed);
            PlayerPrefs.Save();
        }

        public void Dispose()
        {
            SavePlayerPrefs();

            if (_loadHandle.IsValid())
            {
                Addressables.UnloadSceneAsync(_loadHandle);
            }
        }
    }
}