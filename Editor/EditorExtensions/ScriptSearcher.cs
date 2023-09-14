#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Editor.Tools
{
    public sealed class ScriptSearcher : EditorWindow
    {
        private MonoScript _script;
        private GameObject[] _gameObjects;
        
        [MenuItem("Tools/Script Searcher &s")]
        public static void Open()
        {
            GetWindow<ScriptSearcher>();
        }
        
        private void OnGUI()
        {
            _script = EditorGUILayout.ObjectField(_script, typeof(MonoScript), true) as MonoScript;
            _gameObjects = Selection.objects.Select(x => x as GameObject).ToArray();
            
            if (_script != null && _gameObjects.Length != 0)
            {
                if (_gameObjects.Length == 1)
                {
                    if (GUILayout.Button($"Find scripts on {_gameObjects[0].name}"))
                    {
                        var selection = FindScripts(_gameObjects[0]);
                        PrintResults(selection.ToList());
                    }
                }
                else
                {
                    if (GUILayout.Button($"Find scripts on {_gameObjects.Length} GameObjects"))
                    {
                        var listGo = new List<GameObject>();
                        
                        foreach (var go in _gameObjects)
                        {
                            var results = FindScripts(go);
                            listGo.AddRange(results);
                        }
                        
                        PrintResults(listGo);
                    }
                }
            }
        }

        private static void PrintResults(List<GameObject> listGo)
        {
            if (listGo.Count == 0)
            {
                Debug.Log($"No any matches!");
            }
            else
            {
                foreach (var go in listGo)
                {
                    Debug.Log($"{go.name} match!", go);
                }
                Selection.objects = listGo.ToArray();
            }
        }

        private GameObject[] FindScripts(GameObject go)
        {
            var results = go.GetComponentsInChildren(_script.GetClass(), true)
                .Select(x => x.gameObject)
                .ToArray();
            return results;
        }
    }
}
#endif
