using System;
using System.Collections;
using System.Collections.Generic;
using CorD.SparrowInterfaceField;
using UnityEngine;

namespace EFS
{
    public class CameraMover : MonoBehaviour, ICameraMover
    {
        [SerializeField]
        InterfaceField<IObservable<Vector2>> desktopInput;
        [SerializeField]
        InterfaceField<IObservable<Vector2>> mobileInput;

        public IDisposable Subscribe(IObserver<Vector2> observer)
        {
            if (Application.isMobilePlatform)
                return mobileInput.i.Subscribe(observer);
            return desktopInput.i.Subscribe(observer);
        }
    }
}
