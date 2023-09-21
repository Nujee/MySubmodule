using Code.Game.Constants.GeneratedCode;
using TriInspector;
using UnityEngine;

namespace Code.MySubmodule.Services.Effects
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