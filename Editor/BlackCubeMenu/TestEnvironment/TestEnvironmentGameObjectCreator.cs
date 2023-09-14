#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Editor.BlackCubeMenu.TestEnvironment
{
    public static class TestEnvironmentGameObjectCreator
    {
        private const string Name = "Code.Game.Test.TestEnvironmentSetup";
        private const string Assembly = ",BlackCube.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
        
        [MenuItem("GameObject/BlackCube/Create Test Object", false, 50)]
        private static void CreateTestObject()
        {
            var newObject = new GameObject("TEST OBJECT");
            newObject.AddComponent<TestEnvironmentSetup>();
            var testEnvironmentSetupType = System.Type.GetType($"{Name}{Assembly}");

            newObject.AddComponent(testEnvironmentSetupType);
        }
    }
}
#endif