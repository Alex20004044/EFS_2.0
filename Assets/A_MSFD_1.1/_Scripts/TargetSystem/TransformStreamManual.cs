using Sirenix.OdinInspector;
using System;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class TransformStreamManual : MonoBehaviour, IObservable<Transform>
    {
        [SerializeField]
        ReactiveProperty<Transform> target;
        public IDisposable Subscribe(IObserver<Transform> observer)
        {
            return target.Subscribe(observer);
        }
        [Button]
        void Refresh()
        {
            target.SetValueAndForceNotify(target.Value);
        }
    }
}