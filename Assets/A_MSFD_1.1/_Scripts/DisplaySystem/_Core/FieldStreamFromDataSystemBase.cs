using MSFD.Data;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace MSFD
{
    public abstract class FieldStreamFromDataSystemBase<T> : MonoBehaviour, IObservable<T>
    {
        [SerializeField]
        string dataPath;

        [FoldoutGroup(EditorConstants.eventsGroup, order: 10)]
        [SerializeField]
        UnityEvent onNext;
        [ReadOnly]
        [ShowInInspector]
        ReactiveProperty<T> value = new ReactiveProperty<T>();
        [Inject]
        public void Init(IDataStreamProvider dataProvider)
        {
             dataProvider.GetDataStream<T>(dataPath).Subscribe(x=>value.Value = x).AddTo(this);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return value.Subscribe(observer);
        }
    }
}