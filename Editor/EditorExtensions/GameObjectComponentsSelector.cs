#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Code.BlackCubeSubmodule.Editor.EditorExtensions
{
    public static class GameObjectComponentsSelector
    {
        [MenuItem("GameObject/BlackCube/Select Rigidbodies", false, 0)]
        private static void SelectAllRigidbodies(MenuCommand menuCommand)
        {
            if(menuCommand.context == null) return;
            
            Select(typeof(Rigidbody), menuCommand.context as GameObject);
        }
        
        [MenuItem("GameObject/BlackCube/Select Renderers", false, 1)]
        private static void SelectAllRenders(MenuCommand menuCommand)
        {
            if(menuCommand.context == null) return;
            
            Select(typeof(Renderer), menuCommand.context as GameObject);
        }

        private static void Select(Type type, GameObject gameObject)
        {
            Selection.objects = gameObject.GetComponentsInChildren(type)
                .Select(x => x.gameObject).ToArray();
        }
        
        [MenuItem("GameObject/BlackCube/Hide &h", false, 2)]
        private static void HideGameobjects()
        {
            var gameObjects = Selection.gameObjects;
            if(gameObjects.Length == 0) return;
            
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(!gameObject.activeSelf);
            }
        }
        
        // //Uncomment to use on Rigidbodies scripts
        // [MenuItem("CONTEXT/Rigidbody/DO SOMETHING", false, 1)]
        // static void DoSomethingWithRigidbody(MenuCommand menuCommand)
        // {
        //     Rigidbody body = (Rigidbody)menuCommand.context;
        //     Debug.Log($"DO SOMETHING WITH RIGIDBODY{body}",body);
        // }
    }
}
#endif