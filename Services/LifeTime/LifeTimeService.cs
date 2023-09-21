using System;
using System.Collections.Generic;
using System.Threading;
using Code.MySubmodule.DebugTools.MyLogger;
using Code.MySubmodule.Utility.Constants;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Code.MySubmodule.Services.LifeTime
{
    [DisallowMultipleComponent]
    public sealed class LifeTimeService : MonoBehaviour
    {
        private readonly Dictionary<int, CancellationTokenSource> _tokenSources = new();
        private readonly CompositeDisposable _disposable = new();

        private static LifeTimeService _instance;

        public void Init()
        {
            _instance = this;
            $"{Names.Submodule}: {nameof(LifeTimeService)} has been initialized".Colored(Color.green).Log();
        }

        /// <summary>
        /// Returns CompositeDisposable that will be disposed on scene destroy.
        /// </summary>
        [PublicAPI]
        public static CompositeDisposable GetDisposable()
        {
            return _instance._disposable;
        }
        
        /// <summary>
        /// Adds disposable to CompositeDisposable that will be disposed on scene destroy.
        /// </summary>
        /// <param name="disposable"></param>
        [PublicAPI]
        public static void AddToDisposable(IDisposable disposable)
        {
            _instance._disposable.Add(disposable);
        }

        /// <summary>
        /// Returns token, which source will be cancelled on Scene destroy. 
        /// </summary>
        [PublicAPI]
        public static CancellationToken GetToken() => GetTokenFor(0);

        /// <summary>
        /// Returns CancellationTokenSource tied to signature. Can be cancelled anytime during runtime, or will be canceled on Scene destroy.
        /// Don't use 0 as signature. 
        /// </summary>
        [PublicAPI]
        public static CancellationToken GetTokenFor(int signature)
        {
            if (!_instance._tokenSources.ContainsKey(signature))
            {
                _instance._tokenSources.Add(signature, new CancellationTokenSource());
            }
            
            return _instance._tokenSources[signature].Token;
        }

        /// <summary>
        /// Cancels CancellationTokenSource tied to caller.
        /// Don't use 0 as signature.
        /// </summary>
        [PublicAPI]
        public static void CancelFor(int signature)
        {
            if (!_instance._tokenSources.ContainsKey(signature)) return;
            _instance._tokenSources[signature].Cancel();
            
            _instance._tokenSources.Remove(signature);
        }

        private void OnDestroy()
        {
            foreach (var keyValuePair in _tokenSources)
            {
                _tokenSources[keyValuePair.Key].Cancel();
            }
            _tokenSources.Clear();

            _disposable.Dispose();
        }
    }
}