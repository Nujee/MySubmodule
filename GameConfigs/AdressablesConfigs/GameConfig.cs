using UnityEngine;

namespace Code.BlackCubeSubmodule.GameConfigs.AdressablesConfigs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Configs/Game Config", order = 2)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public CursorLockMode EditorCursorMode { get; private set; }
        [field: SerializeField] public int TweenCapacity { get; private set; } = 200;
        [field: SerializeField] public int SequenceCapacity { get; private set; } = 50;
        [field: SerializeField] public float TimeScale { get; private set; } = 1f;
    }
}