using DG.Tweening;

namespace Code.BlackCubeSubmodule.Services.Effects.DoTween.EffectSettings
{
    [System.Serializable]
    public struct ExpandEffectSettings
    {
        public float StartingScale;
        public float Duration;
        public Ease Ease;
        
        public static ExpandEffectSettings Default => new ExpandEffectSettings
        {
            StartingScale = 0f,
            Duration = 1f,
            Ease = Ease.Linear
        };
    }
}