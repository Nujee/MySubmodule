using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Object = System.Object;

namespace Code.BlackCubeSubmodule.DataStructures
{
    /// <summary>
    /// Simple container that can not create new types. 
    /// </summary>
    [Serializable]
    public sealed class SimpleContainer
    {
        private readonly Dictionary<Type, Object> _container = new Dictionary<Type, object>();

        /// <summary>
        /// Returns true if container contains instance of T.
        /// </summary>
        [PublicAPI]
        public bool Contains<T>()
        {
            return _container.ContainsKey(typeof(T));
        }
        
        /// <summary>
        /// Warning! On adding duplicate type old value will be overwritten. 
        /// </summary>
        [PublicAPI]
        public void RegisterInstance<T>(T instance)
        // where T: class
        {
            var key = instance.GetType();
            if (_container.ContainsKey(key))
            {
                _container.Remove(key);
            }
            
            _container.Add(key, instance);
        }
        
        /// <summary>
        /// Removes instance from container. 
        /// </summary>
        [PublicAPI]
        public void UnregisterInstance<T>() 
        // where T: class
        {
            var type = typeof(T);
            if (_container.ContainsKey(type))
            {
                _container.Remove(type);
            }
        }

        /// <summary>
        /// Returns requested type from container. If Key is missing KeyNotFoundException will be thrown. 
        /// </summary>
        [PublicAPI]
        [CanBeNull]
        public T GetInstance<T>() 
        // where T: class
        {
            if (!_container.ContainsKey(typeof(T))) throw new NullReferenceException("Container does not contain requested type.");
            var result = (T)_container[typeof(T)];
            return result;
        }
    }
}