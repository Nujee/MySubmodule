using Code.Game.Constants.GeneratedCode;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Services.Effects
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ParticleSystem))]
    public sealed class EffectNameMarker : MonoBehaviour
    {
        [field: SerializeField] [field: OnValueChanged("OnIdentityChange")] public EffectName Name { get; private set; }
        
        private void OnIdentityChange()
        {
            gameObject.name = Name.ToString();
        }
    }
}