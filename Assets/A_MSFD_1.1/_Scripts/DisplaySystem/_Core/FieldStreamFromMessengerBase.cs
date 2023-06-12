using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace MSFD
{
    public abstract class FieldStreamFromMessengerBase<T> : MonoBehaviour, IObservable<T>, IObserver<T>
    {
        [MessengerEvent]
        [SerializeField]
        string fieldEventName;

        [FoldoutGroup(EditorConstants.eventsGroup, order: 10)]
        [SerializeField]
        UnityEvent onNext;

        Subject<T> subject = new Subject<T>();

        private void Awake()
        {
            Messenger<T>.AddListener(fieldEventName, OnNext);
        }
        private void OnDestroy()
        {
            Messenger<T>.RemoveListener(fieldEventName, OnNext);
        }
        public void SetFieldEventName(string fieldEventName)
        {
            Messenger<T>.RemoveListener(fieldEventName, OnNext);
            this.fieldEventName = fieldEventName;
            Messenger<T>.AddListener(fieldEventName, OnNext);
        }
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return subject.Subscribe(observer);
        }
        [FoldoutGroup("Debug", order: 20)]
        [Button]
        public virtual void OnNext(T value)
        {
            onNext.Invoke();
            subject.OnNext(value);
        }
        [FoldoutGroup("Debug")]
        [Button]
        public virtual void OnCompleted()
        {
            subject.OnCompleted();
        }
        [FoldoutGroup("Debug")]
        [Button]
        public virtual void OnError(Exception error)
        {
            subject.OnError(error);
        }
    }
}