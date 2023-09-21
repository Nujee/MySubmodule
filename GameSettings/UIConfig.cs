using UnityEngine;

namespace Code.MySubmodule.GameSettings
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "Game Settings/UI Config", order = 4)]
    public class UIConfig : ScriptableObject
    {
        [field: SerializeField] public float DelayBetweenUIElementsAppearance { get; private set; } = 0.2f;
    }
}