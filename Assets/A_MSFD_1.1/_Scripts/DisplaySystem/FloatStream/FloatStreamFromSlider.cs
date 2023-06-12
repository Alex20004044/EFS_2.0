using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace MSFD
{
    public class FloatStreamFromSlider : MonoBehaviour, IObservable<float>
    {
        [SerializeField]
        Slider slider;

        Subject<float> subject = new Subject<float>();
        void Awake()
        {
            slider.onValueChanged.AsObservable().Subscribe(x => subject.OnNext(x)).AddTo(this);
        }
        public IDisposable Subscribe(IObserver<float> observer)
        {
            observer.OnNext(slider.value);
            return subject.Subscribe(observer);
        }
    }
}
