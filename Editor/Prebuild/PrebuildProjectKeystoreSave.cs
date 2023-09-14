using Code.BlackCubeSubmodule.Utility.Constants;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Code.BlackCubeSubmodule.Editor.Prebuild
{
    public class PrebuildProjectKeystoreSave : IPreprocessBuildWithReport

    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            SaveKeystore();
        }

        private void SaveKeystore()
        {
            if (PlayerSettings.Android.keystorePass != Keys.ProjectKeystore)
            {
                PlayerSettings.Android.keystorePass = Keys.ProjectKeystore;
                PlayerSettings.Android.keyaliasPass = Keys.ProjectKeystore;
            }
        }
    }
}