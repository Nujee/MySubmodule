using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CameraType = Code.Game.Constants.GeneratedCode.CameraName;

namespace Code.MySubmodule.Services.Camera
{
    [DisallowMultipleComponent]
    public sealed class CamerasTargetsSettings : MonoBehaviour, ICamerasTargetsSettings
    {
        [SerializeField] private CameraTargetData[] _targetsData = Array.Empty<CameraTargetData>();

        private Dictionary<CameraType, (Transform follow, Transform lookAt)> _targetsDictionary;

        /// <summary>
        /// Returns targetsData as dictionary, with CameraType used as a key. 
        /// </summary>
        public Dictionary<CameraType, (Transform follow, Transform lookAt)> GetTargetsDataAsDictionary()
        {
            return _targetsDictionary ??= 
                _targetsData.ToDictionary(d => d.Type, d => (d.Follow, d.LookAt));
        }

        /// <summary>
        /// Return targetsData array. 
        /// </summary>
        public CameraTargetData[] GetTargetsData => _targetsData;
    }
}