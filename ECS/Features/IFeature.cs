using System.Collections.Generic;
using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Code.BlackCubeSubmodule.ECS.Features
{
    public interface IFeature
    {
        [PublicAPI]
        public List<IEcsSystem> Systems { get; }
        
        [PublicAPI]
        public IFeature Init(IEcsSystems systems);
    }
}