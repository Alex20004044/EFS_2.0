using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace MSFD
{
    public abstract class FieldConverterToBase<T, U> : FieldObserverToBase<T>, IObservable<U>
    {
        public abstract IDisposable Subscribe(IObserver<U> observer);
    }
}