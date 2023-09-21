using UnityEngine;

namespace Code.MySubmodule.Unity.Components
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

