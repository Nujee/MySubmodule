using System.Collections.Generic;
using UnityEngine;

namespace Code.BlackCubeSubmodule.DataStructures
{
    public sealed class PriorityQueue<T>
    {
        private readonly LinkedList<(T data, float priority)> _queue = new LinkedList<(T data, float priority)>();
        private readonly (T data, float priority) _mockupData = (default, float.MaxValue);

        public int Count => _queue.Count;

        public void Enqueue(T data, float priority)
        {
            _queue.AddFirst(_mockupData);
            
            for (var n = _queue.First; n != null; n = n.Next)
            {
                if (n.Next == null)
                {
                    _queue.AddAfter(n, (data, priority));
                    break;
                }
                if (priority <= n.Value.priority && priority >= n.Next.Value.priority)
                {
                    _queue.AddAfter(n, (data, priority));
                    break;
                }
            }
            
            _queue.Remove(_mockupData);
        }

        /// <summary>
        /// Removes from queue and returns item with highest priority.
        /// </summary>
        public T DequeueMax()
        {
            var first = PeekMax();
            _queue.RemoveFirst();

            return first;
        }
        
        /// <summary>
        /// Removes from queue and returns item with lowest priority.
        /// </summary>
        public T DequeueMin()
        {
            var last = PeekMin();
            _queue.RemoveLast();

            return last;
        }

        /// <summary>
        /// Returns item with highest priority without removing it from the queue.
        /// </summary>
        public T PeekMax() => _queue.First.Value.data;

        /// <summary>
        /// Returns item with lowest priority without removing it from the queue.
        /// </summary>
        public T PeekMin() => _queue.Last.Value.data;

        public void Clear() => _queue.Clear();

        public void Print()
        {
#if UNITY_EDITOR
            for (var n = _queue.First; n != null; n = n.Next)
            {
                Debug.Log($"({n.Value.data}, {n.Value.priority}), previous {n.Previous?.Value}");
            }
#endif
        }
    }
}