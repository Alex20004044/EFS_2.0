using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class StringStreamFromFloatStream : FieldConverterToBase<float, string>
    {
        Subject<string> subject = new Subject<string>();
        public override void OnNext(float value)
        {
            subject.OnNext(value.ToString());
        }

        public override IDisposable Subscribe(IObserver<string> observer)
        {
            return subject.Subscribe(observer);
        }
    }
}