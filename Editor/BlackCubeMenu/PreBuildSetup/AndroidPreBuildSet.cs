#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Editor.BlackCubeMenu.PreBuildSetup
{
    public sealed class AndroidPreBuildSet
    {
        private const string ManifestDebuggable = "android:debuggable=\"true\"";
        private const string ManifestPath = "/Plugins/Android/AndroidManifest.xml";
        
        public bool SetScriptableBackEnd()
        {
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            
            var androidArchitecture = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
            PlayerSettings.Android.targetArchitectures = androidArchitecture;
            return true;
        }
        
        public bool CheckVersionNumber()
        {
            var Version = PlayerSettings.bundleVersion;
            var androidBundleVersion = PlayerSettings.Android.bundleVersionCode;
 
            return Version.Contains(androidBundleVersion.ToString());
        }

        public bool CheckDebuggableInManifest()
        {
            try
            {
                return ParseAndroidManifest();
            }
            catch
            {
                Debug.LogError(PreBuildMessages.ManifestNotFoundError);
                return true;
            }
        }

        private bool ParseAndroidManifest()
        {
            var path = Application.dataPath + ManifestPath;

            using (var stream = new StreamReader(path))
            {
                var androidManifest = stream.ReadToEnd();
                if (androidManifest.Contains(ManifestDebuggable)) return false;
            }
            
            return true;
        }
    }
}
#endif
