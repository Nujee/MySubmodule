using DG.Tweening;

namespace Code.BlackCubeSubmodule.Services.Effects.DoTween.EffectSettings
{
    [System.Serializable]
    public struct AppearEffectSettings
    {
        public float StartingScale;
        public float Duration;
        public Ease Ease;
        
        public static AppearEffectSettings Default => new AppearEffectSettings
        {
            StartingScale = 0f,
            Duration = 1f,
            Ease = Ease.OutElastic
        };
    }
}