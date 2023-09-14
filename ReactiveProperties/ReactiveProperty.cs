using System;
using JetBrains.Annotations;

namespace Code.BlackCubeSubmodule.ReactiveProperties
{
    /// <summary>
    /// Should be used for Views updates.
    /// View should subscribe to OnChanged event and update its labels.
    /// Avoid replacing this struct in runtime.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct ReactiveProperty<T>
    {
        private T _value;

        public event Action<T> OnChanged; 

        public ReactiveProperty(T value)
        {
            _value = value;
            OnChanged = delegate {};
        }

        /// <summary>
        /// Will invoke OnChanged event on value set.
        /// </summary>
        [PublicAPI]
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// Sets value without invoking OnChanged event.
        /// </summary>
        [PublicAPI]
        public void SetValue(T value)
        {
            _value = value;
        }
        
        public static implicit operator T(ReactiveProperty<T> property)
        {
            return property.Value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}