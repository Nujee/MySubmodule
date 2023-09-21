using System;
using UniRx;
using UnityEngine;

namespace Code.MySubmodule.DebugTools
{
    public static class UniRxDebugExtension
    {
        public static IObservable<T> Print<T>(this IObservable<T> source, string name = "")
        {
            return Observable.Create<T>(o =>
            {
                if (name.Length > 0) name += " ";
                return source.Subscribe(
                    i =>
                    {
                        Debug.Log($"Sequence {name}value: {i}");
                        o.OnNext(i);
                    },
                    ex => Debug.LogError($"Sequence {name}completed with exception: {ex.Message}"),
                    () => Debug.Log($"Sequence {name}completed"));
            });
        }
    }
}
