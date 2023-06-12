using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

namespace MSFD
{
    public class TransformsStreamManual : MonoBehaviour, IObservable<Transform[]>
    {
        [SerializeField]
        Transform[] transforms = new Transform[1];

        Subject<Transform[]> subject = new Subject<Transform[]>();
        public IDisposable Subscribe(IObserver<Transform[]> observer)
        {
            observer.OnNext(transforms);
            return subject.Subscribe(observer);
        }

        [Button]
        void Refresh()
        {
            subject.OnNext(transforms);
        }
    }
}