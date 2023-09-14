using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.BlackCubeSubmodule.GameConfigs.AdressablesConfigs
{
    [CreateAssetMenu(fileName = "BuildConfig", menuName = "Game Configs/Build Config", order = 0)]
    public sealed class BuildConfig : ScriptableObject
    {
        [SerializeField] private int levelLoopFirstLevelNumber;
        
        [ListDrawerSettings(DefaultExpandedState = true, DraggableItems = false)]
        [field: SerializeField] public AssetReference[] Scenes { get; private set; }

        public int LevelLoopFirstIndex => levelLoopFirstLevelNumber - 1 < 0 
            ? 1
            : levelLoopFirstLevelNumber - 1;
    }
}