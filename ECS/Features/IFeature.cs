using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Code.MySubmodule.ECS.Features
{
    public interface IFeature
    {
        [PublicAPI]
        public void Init(EcsSystems systems);
    }
}