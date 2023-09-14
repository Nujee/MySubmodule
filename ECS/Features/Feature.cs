using System.Collections.Generic;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Unity.IL2CPP.CompilerServices;

namespace Code.BlackCubeSubmodule.ECS.Features
{
    public abstract class Feature
    {
        protected readonly List<IEcsSystem> _systems = new();

        /// <summary>
        /// Implement this method to add systems to feature
        /// </summary>
        [PublicAPI]
        public abstract void Init();
        
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        public static implicit operator IEcsSystem[](Feature feature)
        {
            return feature._systems.ToArray();
        }
    }
}