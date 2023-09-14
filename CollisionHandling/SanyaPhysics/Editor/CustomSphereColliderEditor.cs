#if UNITY_EDITOR
using Code.BlackCubeSubmodule.CollisionHandling.SanyaPhysics.Colliders;
using UnityEditor;
using UnityEngine;

namespace Code.BlackCubeSubmodule.CollisionHandling.SanyaPhysics.Editor
{
    [CustomEditor(typeof(CustomSphereCollider))]
    public sealed class CustomSphereColliderEditor : UnityEditor.Editor
    {
        private float _radius = 1f;
        private bool _isEditingBounds;
        
        private void OnSceneGUI()
        {
            if (target is CustomSphereCollider sphere)
            {
                DrawCollider(sphere, sphere.Color);
                EditBounds(sphere);
            }
        }
        
        public override void OnInspectorGUI()
        {
            if (target is CustomSphereCollider sphere)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Edit Bounds", new[]{GUILayout.Width(120), GUILayout.Height(60)}))
                {
                    _isEditingBounds = !_isEditingBounds;
                }
                if(GUILayout.Button("Reset Bounds", new[]{GUILayout.Width(120), GUILayout.Height(60)}))
                {
                    _isEditingBounds = true;
                    sphere.Radius = sphere.transform.localScale.x / 2;
                }
                _radius = sphere.Radius;
                GUILayout.EndHorizontal();
                base.OnInspectorGUI();
            }
        }
        
        public void DrawCollider(CustomSphereCollider sphere, Color color)
        {
            Handles.color = color;
            
            Handles.DrawWireDisc(sphere.transform.TransformPoint(sphere.Center), sphere.transform.up, sphere.Radius);
            Handles.DrawWireDisc(sphere.transform.TransformPoint(sphere.Center), sphere.transform.right, sphere.Radius);
            Handles.DrawWireDisc(sphere.transform.TransformPoint(sphere.Center), sphere.transform.forward, sphere.Radius);
        }

        public void EditBounds(CustomSphereCollider sphere)
        {
            if (!_isEditingBounds) return;
            
            Handles.color = Color.red;

            EditorGUI.BeginChangeCheck();
            _radius = Handles.ScaleValueHandle(_radius,
                sphere.transform.TransformPoint(sphere.Center + sphere.transform.up * _radius), Quaternion.identity, 0.5f,
                Handles.CubeHandleCap, 0.1f);
            _radius = Handles.ScaleValueHandle(_radius,
                sphere.transform.TransformPoint(sphere.Center + sphere.transform.right * _radius), Quaternion.identity, 0.5f,
                Handles.CubeHandleCap, 0.1f);
            _radius = Handles.ScaleValueHandle(_radius,
                sphere.transform.TransformPoint(sphere.Center + sphere.transform.forward * _radius), Quaternion.identity, 0.5f,
                Handles.CubeHandleCap, 0.1f);
                
            if (EditorGUI.EndChangeCheck())
            {
                if (_radius < 0) _radius = 0.1f;
                Undo.RecordObject(sphere, "Scaled");
                sphere.Radius = _radius;
            }
        }
    }
}
#endif