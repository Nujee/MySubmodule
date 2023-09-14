using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Code.BlackCubeSubmodule.CollisionHandling.UnityPhysics.Detectors
{
    /// <summary>
    /// This interface exists only for detectors initializations. 
    /// </summary>
    public interface IUnityPhysicsCollisionDetector
    {
        [PublicAPI]
        void Init(EcsWorld world, int entity);
    }
}