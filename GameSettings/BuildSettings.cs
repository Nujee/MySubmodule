using TriInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.MySubmodule.GameSettings
{
    [CreateAssetMenu(fileName = "BuildSettings", menuName = "Game Settings/Build Settings", order = 0)]
    public sealed class BuildSettings : ScriptableObject
    {
        [SerializeField] private int levelLoopFirstLevelNumber;
        
        [ListDrawerSettings(Draggable = false, AlwaysExpanded = true)]
        [field: SerializeField] public AssetReference[] Scenes { get; private set; }

        public int LevelLoopFirstIndex => levelLoopFirstLevelNumber - 1 < 0 
            ? 1
            : levelLoopFirstLevelNumber - 1;
    }
}