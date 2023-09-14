using System.Collections.Generic;
using Code.BlackCubeSubmodule.Math;
using Code.BlackCubeSubmodule.Services.LifeTime;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Services.Effects
{
    public static class ParticleSystemExtensions
    {
        private static readonly Dictionary<ParticleSystem, ChildData> _originalParents = new Dictionary<ParticleSystem, ChildData>();

        [PublicAPI]
        public static void Restart(this ParticleSystem particleSystem)
        {
            particleSystem.Stop();
            particleSystem.Play();
        }
        
        /// <summary>
        /// Returns particle effect duration based on particles lifetime. NOT TESTED!!!
        /// </summary>
        [PublicAPI]
        public static float Duration(this ParticleSystem particleSystem)
        {
            return particleSystem.main.startLifetime.constantMax;
        }

        /// <summary>
        /// Start playing particle system and await for it's Duration. NOT TESTED!!!
        /// </summary>
        [PublicAPI]
        public static UniTask PlayAwaitable(this ParticleSystem particleSystem)
        {
            particleSystem.Play();
            return UniTask.Delay(particleSystem.Duration().ToMilliseconds());
        }

        /// <summary>
        /// Detaches particle system from it's parent, then starts playing it. After particle system finishes playing
        /// it will be reattached to it's original parent. 
        /// </summary>
        [PublicAPI]
        public static async UniTask<ParticleSystem> PlayDetached(this ParticleSystem particleSystem, float? duration = null)
        {
            // TODO: optimize this for use of local variables. 
            particleSystem.DetachFromParent();
            particleSystem.Play();

            var delay = duration ?? particleSystem.Duration();
            await UniTask.Delay(delay.ToMilliseconds(), cancellationToken: LifeTimeService.GetToken());
            particleSystem.ReattachToOriginalParent();

            return particleSystem;
        }

        /// <summary>
        /// Removes particle system from it's parent's child list and saves parent, so later it can be reattached
        /// to it's former parent using ReattachToOriginalParent().
        /// </summary>
        [PublicAPI]
        public static ParticleSystem DetachFromParent(this ParticleSystem particleSystem)
        {
            if (_originalParents.ContainsKey(particleSystem)) _originalParents.Remove(particleSystem);
            _originalParents.Add(particleSystem, new ChildData(particleSystem.transform));
            particleSystem.transform.parent = null;
            
            return particleSystem;
        }

        /// <summary>
        /// Reattaches particle system to its original parent. Will only work if DetachFromParent() was previously called
        /// on this particle system. NOT TESTED!!! 
        /// </summary>
        [PublicAPI]
        public static ParticleSystem ReattachToOriginalParent(this ParticleSystem particleSystem)
        {
            if (!_originalParents.ContainsKey(particleSystem)) return particleSystem;
            var childData = _originalParents[particleSystem];
            particleSystem.transform.parent = childData.Parent;
            particleSystem.transform.localPosition = childData.LocalPosition;
            particleSystem.transform.localRotation = childData.LocalRotation;

            return particleSystem; 
        }
    }
}