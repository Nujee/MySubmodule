﻿using Code.Game.Constants.GeneratedCode;
using Code.MySubmodule.DebugTools.MyLogger;
using Code.MySubmodule.ECS.LevelEntity;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.MySubmodule.CollisionHandling.UnityPhysics.Detectors
{
    [DisallowMultipleComponent]
    public sealed class UnityPhysicsCollisionEnterDetector : MonoBehaviour, IUnityPhysicsCollisionDetector
    {
        [SerializeField] private bool _showDebugInfo;
        
        private EcsWorld _world;
        private int _entity;
        private Collider _collider;

        [PublicAPI]
        public void Init(EcsWorld world, int entity)
        {
            _world = world;
            _entity = entity;
            _collider = GetComponent<Collider>();
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            var otherCollider = collision.collider;
            var colliderRegistry = _world.GetPool<c_LevelRuntimeData>().Get(0).ColliderRegistry;
            
            var otherEntity = -1;
            if (colliderRegistry.ContainsKey(otherCollider))
            {
                otherEntity = colliderRegistry[otherCollider];
                if (otherEntity == _entity) return;
            }
#if UNITY_EDITOR
            else
            {
                PrintFailedCollisionInfo(otherCollider);
            }
#endif
            
            var dto = new UnityPhysicsCollisionDTO(_entity, _collider, otherEntity, otherCollider);
            
#if UNITY_EDITOR
            PrintCollisionInfo(dto);
#endif

            ref var c_registeredCollisions = ref _world.GetPool<c_RegisteredCollisions>().Get(_entity);
            c_registeredCollisions.Enter[(Layers)otherCollider.gameObject.layer].Enqueue(dto);
        }

        private void PrintFailedCollisionInfo(Collider otherCollider)
        {
            if (_showDebugInfo) $"Failed to find {otherCollider.gameObject} in Collider Registry".Log(gameObject);
        }
        
        private void PrintCollisionInfo(UnityPhysicsCollisionDTO dto)
        {
            if (_showDebugInfo) dto.Log(dto.OtherCollider.gameObject);
        }
    }
}