using UnityEngine;

namespace Code.BlackCubeSubmodule.Unity.Components
{
    [DisallowMultipleComponent]
    public sealed class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    } 
}

