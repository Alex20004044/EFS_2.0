using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
namespace MSFD
{
    public class IntConverterToFloat : FieldConverterToBase<int, float>
    {
        Subject<float> subject = new Subject<float>();
        public override void OnNext(int value)
        {
            subject.OnNext(value);
        }

        public override IDisposable Subscribe(IObserver<float> observer)
        {
            return subject.Subscribe(observer);
        }
    }
}