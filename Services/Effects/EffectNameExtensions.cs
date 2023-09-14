using Code.Game.Constants.GeneratedCode;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Services.Effects
{
    public static class EffectNameExtensions
    {
        public static ParticleSystem GetOn(this EffectName effectName, int entity)
        {
            return EffectService.Get(entity, effectName);
        }
        
        public static void Play(this EffectName effectName, int entity = 0)
        {
            EffectService.Get(entity, effectName).Play();
        }
        
        public static void PlayDetached(this EffectName effectName, int entity = 0)
        {
            EffectService.Get(entity, effectName).PlayDetached();
        }
        
        public static void Pause(this EffectName effectName, int entity = 0)
        {
            EffectService.Get(entity, effectName).Pause();
        }
        
        public static void Stop(this EffectName effectName, int entity = 0)
        {
            EffectService.Get(entity, effectName).Stop();
        }
    }
}