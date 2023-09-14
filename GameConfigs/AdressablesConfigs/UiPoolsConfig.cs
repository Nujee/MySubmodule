using Code.BlackCubeSubmodule.Services.UI.UiFrame;
using UnityEngine;

namespace Code.BlackCubeSubmodule.GameConfigs.AdressablesConfigs
{
    [CreateAssetMenu(fileName = "UiPrefabsConfig", menuName = "Game Configs/Ui Prefabs Config", order = 3)]
    public sealed class UiPoolsConfig : ScriptableObject
    {
        [field: SerializeField] public UiPoolSettings[] UiPoolSettings { get; private set; }
    }
}