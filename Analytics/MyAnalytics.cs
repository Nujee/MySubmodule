#if FREEPLAY_SDK_SET
#define USING_FB
#endif

#if USING_FB
using Facebook.Unity;
#endif
#if USING_FB && UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using Code.MySubmodule.DebugTools.MyLogger;
using Code.MySubmodule.Math;
using Code.MySubmodule.Utility.Constants;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.MySubmodule.Analytics
{
    // List of SDK sets defines: 
    [HelpURL("https://supertest231.atlassian.net/wiki/spaces/DOCUMENTAT/pages/196707")]
    public static class MyAnalytics
    {
        private const string LevelStarted = "LevelStarted";
        private const string LevelComplete = "LevelComplete";
        private const string LevelFailed = "LevelRestart";

        private static readonly List<ISDKAdapter> _adapters = new(4);
        
        [PublicAPI]
        public static event Action<int, string> OnLevelStart;
        [PublicAPI]
        public static event Action<int, string> OnLevelRestart;
        [PublicAPI]
        public static event Action<int, string> OnLevelFinished;
        
        public static async void LogLevelStart(int index, float logDelay = 0f)
        {
            await UniTask.Delay(logDelay.ToMilliseconds());
            
            var log = $"{Names.Analytics}_{LevelStarted}_{index}";
            
            OnLevelStart?.Invoke(index, log);
            Debug.Log(log);
        }
        
        public static async void LogLevelRestart(int index, float logDelay = 0f)
        {
            await UniTask.Delay(logDelay.ToMilliseconds());
            
            var log = $"{Names.Analytics}_{LevelFailed}_{index}";

            OnLevelRestart?.Invoke(index, log);
            Debug.Log(log);
        }
        
        public static void LogLevelComplete(int index)
        {
            var log = $"{Names.Analytics}_{LevelComplete}_{index}";

            OnLevelFinished?.Invoke(index, log);
            Debug.Log(log);
        }

        /// <summary>
        /// This method should be called after ATT permission request popup have been handled by user. 
        /// </summary>
        public static void Initialize(int ATTStatus)
        {
#if USING_FB
            if (!FB.IsInitialized) 
            { 
                FB.Init(FacebookInitCallback);
            } 
            else 
            {
                FB.ActivateApp();
            }
#else 
            InitializeSDKs();
#endif

            Debug.Log($"{Names.Analytics}: initialized with ATT status {ATTStatus}.".Colored(Color.green));
        }

        // If smth don't work check Player Crushtime 0.4 for iOS. There is was working. 
#if USING_FB
        private static async void FacebookInitCallback()
        {

            if (FB.IsInitialized) 
            {
                FB.ActivateApp();
            } 
            else 
            {
                Debug.LogWarning("Octavian Analytics: Failed to Initialize the Facebook SDK");
            }
            
            FB.Mobile.SetAdvertiserTrackingEnabled(true);
            FB.Mobile.SetAutoLogAppEventsEnabled(true);
            
            InitializeSDKs();

#if USING_FB && UNITY_IOS
            await UniTask.Delay(1.ToMilliseconds());
            SkAdNetworkBinding.SkAdNetworkRegisterAppForNetworkAttribution();
#endif
        }
#endif
        
        private static void InitializeSDKs()
        {
            var adapters = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISDKAdapter>();
            foreach (var adapter in adapters)
            {
                adapter.Init();
            }
        }

        public static void Dispose()
        {
            foreach (var adapter in _adapters)
            {
                adapter.Dispose();
            }
            
            _adapters.Clear();
        }
    }
}