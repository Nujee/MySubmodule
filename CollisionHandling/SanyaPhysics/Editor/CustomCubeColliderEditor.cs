#if UNITY_EDITOR
using Code.BlackCubeSubmodule.CollisionHandling.SanyaPhysics.Colliders;
using UnityEditor;
using UnityEngine;

namespace Code.BlackCubeSubmodule.CollisionHandling.SanyaPhysics.Editor
{
    [CustomEditor(typeof(CustomCubeCollider))]
    public sealed class CustomCubeColliderEditor : UnityEditor.Editor
    {
        private float _scaleX = 1f;
        private float _scaleY = 1f;
        private float _scaleZ = 1f;
        private bool _isEditingBounds;

        private void OnSceneGUI()
        {
            if (target is CustomCubeCollider cube)
            {
                DrawCollider(cube, cube.Color);
                EditBounds(cube);
            }
        }

        public override void OnInspectorGUI()
        {
            if (target is CustomCubeCollider cube)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Edit Bounds", new[] { GUILayout.Width(120), GUILayout.Height(60) }))
                {
                    _isEditingBounds = !_isEditingBounds;
                }

                if (GUILayout.Button("Reset Bounds", new[] { GUILayout.Width(120), GUILayout.Height(60) }))
                {
                    _isEditingBounds = true;
                    cube.Size = cube.transform.localScale;
                    ResetScales(cube);
                }

                ResetScales(cube);
                GUILayout.EndHorizontal();
                base.OnInspectorGUI();
            }
        }

        private void ResetScales(CustomCubeCollider cube)
        {
            _scaleX = cube.Size.x;
            _scaleY = cube.Size.y;
            _scaleZ = cube.Size.z;
        }

        public void DrawCollider(CustomCubeCollider box, Color color)
        {
            Handles.color = color;
            Handles.matrix = box.transform.localToWorldMatrix;
            Handles.DrawWireCube(box.Center, box.Size);
        }

        public void EditBounds(CustomCubeCollider cube)
        {
            if (!_isEditingBounds) return;

            Handles.color = Color.red;

            EditorGUI.BeginChangeCheck();

            var upPoint = cube.Center + Vector3.up / 2 * _scaleY;
            var downPoint = cube.Center - Vector3.up / 2 * _scaleY;
            var rightPoint = cube.Center + Vector3.right / 2 * _scaleX;
            var leftPoint = cube.Center - Vector3.right / 2 * _scaleX;
            var forwardPoint = cube.Center + Vector3.forward / 2 * _scaleZ;
            var backwardPoint = cube.Center - Vector3.forward / 2 * _scaleZ;

            _scaleY = ScaleValueHandle(_scaleY, upPoint);
            _scaleY = ScaleValueHandle(_scaleY, downPoint);
            _scaleX = ScaleValueHandle(_scaleX, rightPoint);
            _scaleX = ScaleValueHandle(_scaleX, leftPoint);
            _scaleZ = ScaleValueHandle(_scaleZ, forwardPoint);
            _scaleZ = ScaleValueHandle(_scaleZ, backwardPoint);

            if (EditorGUI.EndChangeCheck())
            {
                if (_scaleY < 0) _scaleY = 0.1f;
                if (_scaleX < 0) _scaleX = 0.1f;
                if (_scaleZ < 0) _scaleZ = 0.1f;

                Undo.RecordObject(cube, "Scaled");
                cube.Size = new Vector3(_scaleX, _scaleY, _scaleZ);
            }

            Handles.color = cube.Color;

            // TODO: Rewrite to to a simples form. 
            Handles.DrawDottedLines(new[]
            {
                upPoint + Vector3.right / 2 * _scaleX, upPoint - Vector3.right / 2 * _scaleX,
                downPoint + Vector3.right / 2 * _scaleX, downPoint - Vector3.right / 2 * _scaleX,
                rightPoint + Vector3.up / 2 * _scaleY, rightPoint - Vector3.up / 2 * _scaleY,
                leftPoint + Vector3.up / 2 * _scaleY, leftPoint - Vector3.up / 2 * _scaleY,
                forwardPoint + Vector3.up / 2 * _scaleY, forwardPoint - Vector3.up / 2 * _scaleY,
                backwardPoint + Vector3.up / 2 * _scaleY, backwardPoint - Vector3.up / 2 * _scaleY,
                forwardPoint + Vector3.up / 2 * _scaleY, backwardPoint + Vector3.up / 2 * _scaleY,
                forwardPoint - Vector3.up / 2 * _scaleY, backwardPoint - Vector3.up / 2 * _scaleY,
            }, 5);
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