using Code.BlackCubeSubmodule.Unity.Components;
using UnityEditor;
using UnityEditor.UI;

namespace Code.BlackCubeSubmodule.Editor.EditorExtensions
{
    [CanEditMultipleObjects, CustomEditor(typeof(NonDrawingGraphic), false)]
    public class NonDrawingGraphicEditor : GraphicEditor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_Script);
            serializedObject.ApplyModifiedProperties();
        }
    }
}