using Code.MySubmodule.Math;
using Code.MySubmodule.Services.LifeTime;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.MySubmodule.Services.UI.Views
{
    public class View : MonoBehaviour, IEcsInitSystem
    {
        /// <summary>
        /// Wrapper for gameObject.activeInHierarchy.
        /// </summary>
        [PublicAPI]
        public bool IsOpen => gameObject.activeInHierarchy;
        
        /// <summary>
        /// Was async Open() method been called.
        /// In the frame when view gameObject becomes active in hierarchy this flag is set to false.
        /// </summary>
        [PublicAPI]
        public bool IsOpening { get; private set; }
        
        // Should be left empty to avoid base.Init() calls. 
        public virtual void Init(IEcsSystems systems) { }

        [PublicAPI]
        public async void Open(float delay = 0f)
        {
            IsOpening = true;
            await UniTask.Delay(delay.ToMilliseconds(), cancellationToken: LifeTimeService.GetToken());

            IsOpening = false;
            gameObject.SetActive(true);
            OnOpen();
        }

        [PublicAPI]
        public async void Close(float delay = 0f)
        {
            IsOpening = false;
            await UniTask.Delay(delay.ToMilliseconds(), cancellationToken: LifeTimeService.GetToken());

            gameObject.SetActive(false);
            OnClose();
        }
        
        // Should be left empty to avoid base.OnOpen() calls.
        /// <summary>
        /// Method is called in the frame when view gameObject becomes active in hierarchy.
        /// Should be used instead of OnEnable() method.
        /// </summary>
        [PublicAPI]
        protected virtual void OnOpen(){ }
        
        // Should be left empty to avoid base.OnOpen() calls.
        /// <summary>
        /// Method is called in the frame when view gameObject becomes inactive in hierarchy.
        /// Should be used instead of OnDisable() method.
        /// </summary>
        [PublicAPI]
        protected virtual void OnClose(){ }
    }
}