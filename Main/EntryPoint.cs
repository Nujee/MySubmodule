using Code.BlackCubeSubmodule.DebugTools.BlackCubeLogger;
using Code.BlackCubeSubmodule.Utility.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.BlackCubeSubmodule.Main
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(int.MinValue)]
    public sealed class EntryPoint : MonoBehaviour
    {
        private BaseSessionCompositionRoot _sessionCompositionRoot;
        private SceneCompositionRoot _sceneCompositionRoot;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            var levelIndex = SceneManager.GetActiveScene().buildIndex;
            var sessionCompositionRoot = FindObjectOfType<BaseSessionCompositionRoot>();

            if (levelIndex == 0) 
            {
                $"{Names.Submodule}: {nameof(EntryPoint)} loading game from Main scene"
                    .Colored(Color.green)
                    .Log();
                
                sessionCompositionRoot.Init(GameLoadType.OnDeviceLoad);
            }
            else if (FindObjectsOfType<EntryPoint>().Length > 1)
            {
                $"{Names.Submodule}: {nameof(EntryPoint)} aborting due to another {nameof(EntryPoint)} present"
                    .Colored(Color.green)
                    .Log();
                
                Destroy(gameObject);
            }
            else 
            {
                $"{Names.Submodule}: {nameof(EntryPoint)} warning. Development load outside of Main scene"
                    .Colored(Color.yellow)
                    .Log();
                
                sessionCompositionRoot.Init(GameLoadType.DevelopmentLoad);
            }
        }
    }
}