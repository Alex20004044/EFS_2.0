using CorD.SparrowInterfaceField;
using Sirenix.OdinInspector;
using System;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class TransformStreamFirstFromTransformsStream : MonoBehaviour, IObservable<Transform>, IObserver<Transform[]>
    {
        [SerializeField]
        InterfaceField<IObservable<Transform[]>> transformsSource;

        [ShowInInspector]
        [ReadOnly]
        ReactiveProperty<Transform> currentTarget = new ReactiveProperty<Transform>();
        private void Start()
        {
            transformsSource.i.Subscribe(this).AddTo(this);
        }
        public void OnCompleted()
        {
            currentTarget.Dispose();
        }
        public void OnError(Exception error)
        {
            throw error;
        }
        public void OnNext(Transform[] value)
        {
            if (value == null)
                currentTarget.Value = null;
            else
                currentTarget.Value = value[0];
        }
        public IDisposable Subscribe(IObserver<Transform> observer)
        {
            return currentTarget.Subscribe(observer);
        }
    }
}