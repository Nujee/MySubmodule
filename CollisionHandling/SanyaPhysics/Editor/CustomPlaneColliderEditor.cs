#if UNITY_EDITOR
using Code.MySubmodule.CollisionHandling.SanyaPhysics.Colliders;
using UnityEditor;
using UnityEngine;

namespace Code.MySubmodule.CollisionHandling.SanyaPhysics.Editor
{
    [CustomEditor(typeof(CustomPlaneCollider))]
    public sealed class CustomPlaneColliderEditor : UnityEditor.Editor
    {
        private float _scaleX = 1f;
        private float _scaleY = 1f;
        private bool _isEditingBounds;
        
        private void OnSceneGUI()
        {
            if (target is CustomPlaneCollider plane)
            {
                DrawCollider(plane, plane.Color);
                EditBounds(plane);
            }
        }
        
        public override void OnInspectorGUI()
        {
            if (target is CustomPlaneCollider plane)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Edit Bounds", new[]{GUILayout.Width(120), GUILayout.Height(60)}))
                {
                    _isEditingBounds = !_isEditingBounds;
                }
                if(GUILayout.Button("Reset Bounds", new[]{GUILayout.Width(120), GUILayout.Height(60)}))
                {
                    _isEditingBounds = true;
                    plane.Size = new Vector2(plane.transform.localScale.x, plane.transform.localScale.y) * 10;
                }
                _scaleX = plane.Size.x;
                _scaleY = plane.Size.y;
                GUILayout.EndHorizontal();
                base.OnInspectorGUI();
            }
        }
        
        private void DrawCollider(CustomPlaneCollider plane, Color color)
        {
            Handles.color = color;
            Handles.matrix = plane.transform.localToWorldMatrix;
            Handles.DrawWireCube(plane.transform.position, new Vector3(plane.Size.x, 0,plane.Size.y));
        }

        private void EditBounds(CustomPlaneCollider plane)
        {
            if (!_isEditingBounds) return;
            
            Handles.color = Color.red;
            
            EditorGUI.BeginChangeCheck();
            
            var upPoint = plane.Center + Vector3.forward/2 * _scaleY;
            var downPoint = plane.Center - Vector3.forward/2 * _scaleY;
            var rightPoint = plane.Center + Vector3.right/2 * _scaleX;
            var leftPoint = plane.Center - Vector3.right/2 * _scaleX;
            
            _scaleY = ScaleValueHandle(_scaleY, upPoint);
            _scaleY = ScaleValueHandle(_scaleY, downPoint);
            _scaleX = ScaleValueHandle(_scaleX, rightPoint);
            _scaleX = ScaleValueHandle(_scaleX, leftPoint);
          
            if (EditorGUI.EndChangeCheck())
            {
                if (_scaleY < 0) _scaleY = 0.1f;
                if (_scaleX < 0) _scaleX = 0.1f;
               
                Undo.RecordObject(plane, "Scaled");
                plane.Size = new Vector3(_scaleX, _scaleY, plane.transform.localScale.z);
            }
        }

        private float ScaleValueHandle(float scale, Vector3 point)
        {
            return Handles.ScaleValueHandle(scale,
                point, Quaternion.identity, 0.5f,
                Handles.CubeHandleCap, 0.1f);
        }
    }
}

#endif
