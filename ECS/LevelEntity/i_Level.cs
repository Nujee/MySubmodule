using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;                               
                                                 
namespace Code.BlackCubeSubmodule.ECS.LevelEntity
{
    public sealed class i_Level : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var levelEntity = world.NewEntity();
            
            ref var c_runtimeData = ref world.GetPool<c_LevelRuntimeData>().Add(levelEntity);
            c_runtimeData.ColliderRegistry = new Dictionary<Collider, int>();
        }
    }
}