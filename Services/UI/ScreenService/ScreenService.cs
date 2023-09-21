using System.Collections.Generic;
using Code.MySubmodule.Math;
using Code.MySubmodule.Services.LifeTime;
using Code.MySubmodule.Services.UI.Views;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.MySubmodule.Services.UI.ScreenService
{
    public sealed class ScreenService : MonoBehaviour
    {
        private readonly Dictionary<int, View> _screens = new Dictionary<int, View>();
        
        public void Initialise()
        {
            var potentialScreens = FindObjectsOfType<View>(true);
            for (var i = 0; i < potentialScreens.Length; i++)
            {
                if (potentialScreens[i] is IAccessibleToScreenService)
                {
                    _screens.Add(potentialScreens[i].GetType().Name.GetHashCode(), potentialScreens[i]);
                }
            }

            foreach (var screen in _screens)
            {
                screen.Value.Close();
            }
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
            var screen = _screens[screenID];
            if (!screen.IsOpen && !screen.IsOpening)
            {
                _screens[screenID].Open(delay);
            
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
        {
            var screenID = typeof(TScreen).Name.GetHashCode();
            var screen = _screens[screenID];
            
            if (screen.IsOpen || screen.IsOpening)
            {
                _screens[screenID].Close(delay);
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