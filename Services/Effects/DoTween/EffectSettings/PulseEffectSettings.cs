using DG.Tweening;

namespace Code.BlackCubeSubmodule.Services.Effects.DoTween.EffectSettings
{
    [System.Serializable]
    public struct PulseEffectSettings
    {
        public float TargetScale;
        public float Duration;
        public Ease Ease;
        public LoopType LoopType;
        
        public static PulseEffectSettings Default => new PulseEffectSettings
        {
            TargetScale = 1.05f,
            Duration = 0.5f,
            Ease = Ease.Linear,
            LoopType = LoopType.Yoyo
        };
    }
}