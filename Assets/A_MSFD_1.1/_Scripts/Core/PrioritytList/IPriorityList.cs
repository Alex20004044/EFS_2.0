using System;
using System.Collections.Generic;
using UniRx;

namespace MSFD
{
    public interface IPriorityList<T> : IEnumerable<T>
    {
        IDisposable Add(T item, int priority = 0);
        IObservable<Unit> GetObsOnItemsUpdated();
        void RaiseItemsUpdatedEvent();
        void Clear();
        IEnumerable<KeyValuePair<int, IEnumerable<T>>> GetPriorityGroupsEnumerator();
    }
}