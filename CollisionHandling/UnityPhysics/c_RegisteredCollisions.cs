using System;
using System.Collections.Generic;
using Code.Game.Constants.GeneratedCode;
using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Code.BlackCubeSubmodule.CollisionHandling.UnityPhysics
{
    public struct c_RegisteredCollisions : IEcsAutoReset<c_RegisteredCollisions>
    {
        /// <summary>
        /// All currently unhandled collisions enter.
        /// </summary>
        [PublicAPI]
        public Dictionary<Layers, Queue<UnityPhysicsCollisionDTO>> Enter;

        /// <summary>
        /// Use this method to fill this registry before adding it to any entity. 
        /// </summary>
        // TODO: How can i fill it automatically. 
        [PublicAPI]
        public void Fill()
        {
            // var layers = (Layers[])Enum.GetValues(typeof(Layers));
            // Enter = new Dictionary<Layers, Queue<UnityPhysicsCollisionDTO>>(layers.Length);
            // foreach (var layer in layers)
            // {
            //     Enter.Add(layer, new Queue<UnityPhysicsCollisionDTO>(10));
            // }
        }

        // ADDED AS A TEST. 
        public void AutoReset(ref c_RegisteredCollisions c)
        {
            var layers = (Layers[])Enum.GetValues(typeof(Layers));
            c.Enter = new Dictionary<Layers, Queue<UnityPhysicsCollisionDTO>>(layers.Length);
            foreach (var layer in layers)
            {
                c.Enter.Add(layer, new Queue<UnityPhysicsCollisionDTO>(10));
            }
        }
    }
}