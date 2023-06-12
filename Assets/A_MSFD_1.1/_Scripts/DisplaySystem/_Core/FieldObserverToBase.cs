using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using CorD.SparrowInterfaceField;

namespace MSFD
{
    public abstract class FieldObserverToBase<T> : MonoBehaviour, IObserver<T>
    {
        [SerializeField]
        InterfaceField<IObservable<T>> observableSource;

        IDisposable disposable;
        protected virtual void Awake()
        {
            if (observableSource.i == null)
                Debug.LogError("Observable is not installed");
            else
                disposable = observableSource.i.Subscribe(this).AddTo(gameObject);
        }
        public virtual void OnCompleted()
        {
            disposable.Dispose();
        }

        public virtual void OnError(Exception error)
        {
            Debug.LogError(error);
        }

        public abstract void OnNext(T value);
    }
}