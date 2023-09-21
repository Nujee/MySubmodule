using System;
using JetBrains.Annotations;

namespace Code.MySubmodule.ReactiveProperties
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
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// Replaces current value with new one without invoking OnChanged event.
        /// </summary>
        /// <returns></returns>
        [PublicAPI]
        public void SetValue(T newValue)
        {
            _value = newValue;
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