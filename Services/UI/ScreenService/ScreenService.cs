using System;
using System.Collections.Generic;
using Code.BlackCubeSubmodule.GameConfigs.AdressablesConfigs;
using Code.BlackCubeSubmodule.Math;
using Code.BlackCubeSubmodule.Pools;
using Code.BlackCubeSubmodule.Services.LifeTime;
using Code.BlackCubeSubmodule.Services.UI.Views;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Code.BlackCubeSubmodule.Services.UI.ScreenService
{
    public sealed class ScreenService
    {
        private readonly Dictionary<int, UiPool> _screenPools = new();
        private readonly Dictionary<int, View> _screens = new();
        private readonly Dictionary<int, View> _openedScreens = new();

        private ScreenService() { }
        
        public static async UniTask<ScreenService> New(View[] potentialPreloadedScreens, UiPoolsConfig config)
        {
            var screenService = new ScreenService();
            
            for (var i = 0; i < potentialPreloadedScreens.Length; i++)
            {
                if (potentialPreloadedScreens[i] is IAccessibleToScreenService)
                {
                    screenService._screens.Add(potentialPreloadedScreens[i].GetType().Name.GetHashCode(), potentialPreloadedScreens[i]);
                }
            }

            foreach (var poolSettings in config.UiPoolSettings)
            {
                var newPool = await UiPool.New(poolSettings);
                screenService._screenPools.Add(newPool.PoolType.Name.GetHashCode(), newPool);
            }

            foreach (var screen in screenService._screens)
            {
                screen.Value.Close();
            }
            
            return screenService;
        }

        /// <summary>
        /// Will show TScreen after delay seconds and will close it after duration seconds.
        /// If TScreen is already open or opening, will do nothing.
        /// </summary>
        [PublicAPI]
        public void OpenScreenAsPopup<TScreen>(float duration, ScreenOpenType screenOpenType = ScreenOpenType.Single, float delay = 0f)
            where TScreen: View, IAccessibleToScreenService
        {
            OpenScreen<TScreen>(delay, screenOpenType);
            CloseScreen<TScreen>(duration);
        }
        
        /// <summary>
        /// Will show TScreen after delay seconds.
        /// If TScreen is already open or opening, will do nothing.
        /// </summary>
        [PublicAPI]
        public async void OpenScreen<TScreen>(float delay = 0f, ScreenOpenType screenOpenType = ScreenOpenType.Single)
            where TScreen: View, IAccessibleToScreenService
        {
            var screenID = typeof(TScreen).Name.GetHashCode();
            var view = default(View);
            var viewFromPool = false;
            if (_screens.TryGetValue(screenID, out var screen))
            {
                view = screen;
            }
            else if (_screenPools.TryGetValue(screenID, out var pool))
            {
                view = pool.Get();
                viewFromPool = true;
            }
            else
            {
                throw new Exception($"{typeof(TScreen)} has not been added to ScreenService.");
            }
            
            if (!view.IsOpen && !view.IsOpening)
            {
                view.Open(delay);
                if (viewFromPool)
                {
                    _openedScreens.Add(screenID, view);
                }
            
                if (screenOpenType == ScreenOpenType.Single)
                {
                    await UniTask.Delay(delay.ToMilliseconds(), cancellationToken: LifeTimeService.GetToken());
                    CloseScreens<TScreen>();
                }
            }
        }

        /// <summary>
        /// Will hide TScreen after delay seconds.
        /// If TScreen is already closed, will do nothing.
        /// </summary>
        public void CloseScreen<TScreen>(float delay = 0f)
            where TScreen: View, IAccessibleToScreenService
        {
            var screenID = typeof(TScreen).Name.GetHashCode();
            
            if (_screens.TryGetValue(screenID, out var screen))
            {
                if (screen.IsOpen || screen.IsOpening)
                {
                    screen.Close(delay);
                }
            }
            else if (_screenPools.TryGetValue(screenID, out var pool))
            {
                var view = _openedScreens[screenID];
                _openedScreens.Remove(screenID);
                if (view.IsOpen || view.IsOpening)
                {
                    view.Close(delay);
                    pool.Return(view);
                }
            }
            else
            {
                throw new Exception($"{typeof(TScreen)} has not been added to ScreenService.");
            }
        }

        private void CloseScreens<TScreen>() 
            where TScreen: View, IAccessibleToScreenService
        {
            foreach (var item in _screens)
            {
                var screenID = typeof(TScreen).Name.GetHashCode();
                if (item.Key != screenID)
                {
                    item.Value.Close();
                }
            }
        }
    }
}