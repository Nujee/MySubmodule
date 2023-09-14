using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.BlackCubeSubmodule.Services.UI.UiFrame
{
    [System.Serializable]
    public struct UiPoolSettings
    {
        [field: SerializeField] public AssetReference Prefab { get; private set; }
        [field: SerializeField] [Indent] public int StartingSize { get; private set; }
        [field: SerializeField] [Indent] public int RefillCount { get; private set; }
    }
}