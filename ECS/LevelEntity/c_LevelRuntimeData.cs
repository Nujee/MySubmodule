using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.BlackCubeSubmodule.ECS.LevelEntity
{
    public struct c_LevelRuntimeData
    {
        [PublicAPI]
        public Dictionary<Collider, int> ColliderRegistry;
    }
}