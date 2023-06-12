using CorD.SparrowInterfaceField;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    [Serializable]
    public class InterfaceFieldObs<T> : IObservable<T>
    {
        [SerializeField]
        Mode mode = Mode.dynamic;
        [ShowIf("@" + nameof(IsDynamicMode) + "()")]
        [SerializeField]
        InterfaceField<IObservable<T>> dynamicVariable;
        [HideIf("@" + nameof(IsDynamicMode) + "()")]
        [SerializeField]
        T staticVariable;
        public IDisposable Subscribe(IObserver<T> observer)
        {
            if(IsDynamicMode())
            {
                return dynamicVariable.i.Subscribe(observer);
            }
            else
            {
                observer.OnNext(staticVariable);
                return Disposable.Empty;
            }
        }

        bool IsDynamicMode()
        {
            return mode == Mode.dynamic;
        }
        enum Mode { dynamic, @static };
    }
}