using System.Collections.Generic;
using Code.Game.Constants.GeneratedCode;
using UnityEngine;

namespace Code.MySubmodule.Services.Camera
{
    public interface ICamerasTargetsSettings
    {
        /// <summary>
        /// Returns targetsData as dictionary, with CameraType used as a key. 
        /// </summary>
        Dictionary<CameraName, (Transform follow, Transform lookAt)> GetTargetsDataAsDictionary();
    }
}