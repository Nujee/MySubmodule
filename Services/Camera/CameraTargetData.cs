using Code.Game.Constants.GeneratedCode;
using UnityEngine;
using CameraType = Code.Game.Constants.GeneratedCode.CameraName;

namespace Code.MySubmodule.Services.Camera
{
    [System.Serializable]
    public sealed class CameraTargetData
    {
        [field: SerializeField] public CameraType Type { get; private set; }
        [field: SerializeField] public Transform Follow { get; private set; }
        [field: SerializeField] public Transform LookAt { get; private set; }

        public CameraTargetData(CameraName type, Transform follow, Transform lookAt)
        { 
            Type = type;
            Follow = follow;
            LookAt = lookAt;
        }
    }
}