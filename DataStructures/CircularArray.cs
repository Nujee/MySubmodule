using System;

namespace Code.BlackCubeSubmodule.DataStructures
{
    /// <summary>
    /// NOT TESTED!!! 
    /// </summary>
    public sealed class CircularArray<T> 
    {
        private readonly T[] _innerArray;
        
        private int _currentIndex = -1;

        public CircularArray(T[] array)
        {
            if (array is null || array.Length == 0) 
                throw new ArgumentException("Empty or null array is not allowed as base for CircularArray");
            
            _innerArray = array;
        }
        
        public T GetNext()
        {
            _currentIndex = ++_currentIndex >= _innerArray.Length ? 0 : _currentIndex;
            return _innerArray[_currentIndex];
        }
    }
}