using Code.BlackCubeSubmodule.ECS.DependencyInjection;
using Code.BlackCubeSubmodule.Services.Effects.DoTween.EffectSettings;
using UnityEngine;

namespace Code.BlackCubeSubmodule.UI.VictoryScreen
{
    [DisallowMultipleComponent]
    public sealed class VictoryViewConfig : MonoBehaviour, IShouldBeInjected
    {
        [field: SerializeField] public float ShowDelay { get; private set; } = 1f;
        [field: SerializeField] public float DelayBetweenElementsPopup { get; private set; } = 0.2f;
        [field: SerializeField] public float AutoCloseDelay { get; private set; } = 5f;
        [field: SerializeField] public AppearEffectSettings AppearEffect { get; private set; } = AppearEffectSettings.Default;
    }
}