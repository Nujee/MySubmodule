using Cinemachine;
using Code.Game.Constants.GeneratedCode;
using TriInspector;
using UnityEngine;

namespace Code.MySubmodule.Services.Camera
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public sealed class CameraNameMarker : MonoBehaviour
    {
        [field: SerializeField] [field: OnValueChanged("OnIdentityChange")] public CameraName Name { get; private set; }
        
        private void OnIdentityChange()
        {
            gameObject.name = $"VCam {Name}";
        }
    }
}