using UnityEngine;
using System.Collections;
using UniRx;
using System;

namespace UniRxWorkBook
{
    public static class ObservableExtension
    {
        public static IObservable<T> _____<T>(this IObservable<T> source)
        {
            return source;
        }
    }
}
