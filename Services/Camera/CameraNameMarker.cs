using Cinemachine;
using Code.Game.Constants.GeneratedCode;
using Sirenix.OdinInspector;
using UnityEngine;
using CameraType = Code.Game.Constants.GeneratedCode.CameraName;

namespace Code.BlackCubeSubmodule.Services.Camera
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