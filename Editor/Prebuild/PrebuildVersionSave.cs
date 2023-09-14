using Code.BlackCubeSubmodule.Utility.Constants;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Editor.Prebuild
{
    public sealed class PrebuildVersionSave : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;
        
        public void OnPreprocessBuild(BuildReport report)
        {
            var gameVersion = PlayerSettings.bundleVersion;
            PlayerPrefs.SetString(Keys.GameVersion, gameVersion);
            
            var bundleNumber = PlayerSettings.Android.bundleVersionCode;
            PlayerPrefs.SetInt(Keys.BundleVersionCode, bundleNumber);
        }
    }
}