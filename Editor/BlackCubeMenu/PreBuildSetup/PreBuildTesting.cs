#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Editor.BlackCubeMenu.PreBuildSetup
{
    public sealed class PreBuildTesting: MonoBehaviour
    {
        private static readonly List<bool> AllResults = new List<bool>();
        
        private static AndroidPreBuildSet _androidPreBuildSet;
        private static GeneralPreBuildSet _generalPreBuildSet;

        [MenuItem("BlackCube/Run prebuild tests", priority = 3)]
        public static void StartPreBuildTest()
        {
            var platform = EditorUserBuildSettings.activeBuildTarget;
            
            Debug.Log($"{PreBuildMessages.TestingStartedMessage.ToUpper()}, TARGET PLATFORM: {platform}");
            
            RunGeneralTestSet();

            if (platform == BuildTarget.Android) RunAndroidTestSet();
            else if (platform == BuildTarget.iOS) RunIOSTestSet();
            else Debug.LogError(PreBuildMessages.WrongPlatformMessage);

            DisplayFinalResult();
        }

        private static void RunGeneralTestSet()
        {
            _generalPreBuildSet = new GeneralPreBuildSet();
            
            RunTest(_generalPreBuildSet.SetInterfaceOrientation, string.Empty);
            RunTest(_generalPreBuildSet.CheckCompanyName, PreBuildMessages.CompanyNameError);
            RunTest(_generalPreBuildSet.CheckGameName, PreBuildMessages.GameNameError);
            RunTest(_generalPreBuildSet.CheckScenesInTheBuildSettings, PreBuildMessages.LevelsError);
        }

        private static void RunAndroidTestSet()
        {
            _androidPreBuildSet = new AndroidPreBuildSet();

            RunTest(_androidPreBuildSet.SetScriptableBackEnd, string.Empty);
            RunTest(_androidPreBuildSet.CheckVersionNumber, PreBuildMessages.VersionError);
            RunTest(_androidPreBuildSet.CheckDebuggableInManifest, PreBuildMessages.ManifestError);
        }

        private static void RunIOSTestSet()
        {
            
        }
        
        private static void RunTest(Func<bool> test, string failMessage)
        {
            var testResult = test.Invoke();
            AllResults.Add(testResult);
            if (!testResult) Debug.LogError(failMessage);
        }
        
        private static void DisplayFinalResult()
        {
            var areAllTestSuccessful = AllResults.All(i => i);
            
            if (areAllTestSuccessful) Debug.Log(PreBuildMessages.SuccessMessage.ToUpper());
            else Debug.LogError(PreBuildMessages.FailMessage.ToUpper());
        }
    }
}
#endif
