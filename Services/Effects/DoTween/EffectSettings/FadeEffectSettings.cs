namespace Code.BlackCubeSubmodule.Services.Effects.DoTween.EffectSettings
{
    [System.Serializable]
    public struct FadeEffectSettings
    {
        public float EndValue;
        public float Duration;

        public static FadeEffectSettings Default => new FadeEffectSettings
        {
            EndValue = 0f,
            Duration = 1f,
        };
    }
}