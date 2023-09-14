using DG.Tweening;

namespace Code.BlackCubeSubmodule.Services.Effects.DoTween.EffectSettings
{
    [System.Serializable]
    public struct ShrinkEffectSettings
    {
        public float TargetScale;
        public float Duration;
        public Ease Ease;
        
        public static ShrinkEffectSettings Default => new ShrinkEffectSettings
        {
            TargetScale = 0f,
            Duration = 1f,
            Ease = Ease.InBack
        };
    }
}