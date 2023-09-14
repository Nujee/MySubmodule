using System.Collections.Generic;
using Cinemachine;
using Code.BlackCubeSubmodule.DebugTools.BlackCubeLogger;
using Code.BlackCubeSubmodule.Utility.Constants;
using JetBrains.Annotations;
using UnityEngine;
using CameraType = Code.Game.Constants.GeneratedCode.CameraName;

namespace Code.BlackCubeSubmodule.Services.Camera
{
    public sealed class CameraService
    {
        private readonly Dictionary<CameraType, GameObject> _cameras = new Dictionary<CameraType, GameObject>();
        
        private CameraType _currentlyActiveCamera;
        
        public CameraService(CameraType initiallyActiveCamera)
        {
            var camerasOnScene = Object.FindObjectsOfType<CameraNameMarker>(true);
            foreach (var cameraNameMarker in camerasOnScene)
            {
                _cameras.Add(cameraNameMarker.Name, cameraNameMarker.gameObject);
                cameraNameMarker.gameObject.SetActive(false);
            }
            
            ActivateCamera(initiallyActiveCamera);
            
            $"{Names.Submodule}: {nameof(CameraService)} has been initialized".Colored(Color.green).Log();
        }

        public void AddTargetSettings(ICamerasTargetsSettings targetsSettings)
        {
            var targets = targetsSettings.GetTargetsDataAsDictionary();
            foreach (var pair in targets)
            {
                var virtualCamera = _cameras[pair.Key].GetComponentInChildren<CinemachineVirtualCamera>(true);
                
                virtualCamera.Follow = pair.Value.follow;
                virtualCamera.LookAt = pair.Value.lookAt;
            }
        }

        /// <summary>
        /// Changes active came to cameraToActivate.
        /// </summary>
        [PublicAPI]
        public void ChangeCameraTo(CameraType cameraToActivate)
        {
            if (_currentlyActiveCamera == cameraToActivate) return;
            if (_cameras.ContainsKey(cameraToActivate))
            {
                _cameras[_currentlyActiveCamera].SetActive(false);
                ActivateCamera(cameraToActivate);
            }
        }

        private void ActivateCamera(CameraType cameraToActivate)
        {
            _cameras[cameraToActivate].SetActive(true);
            _currentlyActiveCamera = cameraToActivate;
        }
    }
}