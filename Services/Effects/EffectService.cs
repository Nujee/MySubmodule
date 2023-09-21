using System.Collections.Generic;
using Code.Game.Constants.GeneratedCode;
using UnityEngine;

namespace Code.MySubmodule.Services.Effects
{
    public static class EffectService
    {
        private static readonly Dictionary<int, Dictionary<EffectName, ParticleSystem>> BindEffects = new Dictionary<int, Dictionary<EffectName, ParticleSystem>>(4);

        public static void Init()
        {
            BindEffects.Clear();
            
            var freeEffects = Object.FindObjectsOfType<EffectNameMarker>();
            foreach (var effectNameMarker in freeEffects)
            {
                RegisterEffect(0, effectNameMarker);
            }
        }

        public static ParticleSystem Get(int entity, EffectName effectName)
        {
            return BindEffects[entity][effectName];
        }
        
        public static void RegisterAll(int entity, Transform root)
        {
            var effects = root.GetComponentsInChildren<EffectNameMarker>();
            foreach (var effectName in effects)
            {
                RegisterEffect(entity, effectName);
            }
        }

        public static void RegisterEffect(int entity, EffectNameMarker effectNameMarker)
        {
            var effect = effectNameMarker.GetComponent<ParticleSystem>();
            RegisterEffect(entity, effectNameMarker.Name, effect);
        }
        
        public static void RegisterEffect(int entity, EffectName effectName, ParticleSystem particleSystem)
        {
            if (!BindEffects.ContainsKey(entity))
            {
                BindEffects.Add(entity, new Dictionary<EffectName, ParticleSystem>());
            }

            if (entity == 0 && BindEffects.ContainsKey(0) && BindEffects[0].ContainsKey(effectName))
            {
                BindEffects[0].Remove(effectName);
            }

            if (BindEffects[entity].ContainsKey(effectName))
            {
                BindEffects[entity].Remove(effectName);
            }
            
            BindEffects[entity].Add(effectName, particleSystem);
        }
    }
}