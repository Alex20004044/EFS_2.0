using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using CorD.SparrowInterfaceField;

namespace MSFD
{
    public abstract class MessengerBroadcastObserverBase<T> : MessengerBroadcastBase
    {
        [SerializeField]
        GetValueMode getValueMode = GetValueMode.staticValue;
        [ShowIf("@" + nameof(IsGetValueFromObservableMode) + "()")]
        [SerializeField]
        InterfaceField<IObservable<T>> source;
        [ShowIf("@" + nameof(IsGetValueFromObservableMode) + "()")]
        [SerializeField]
        bool isBroadcastOnce = false;

        [HideIf("@" + nameof(IsGetValueFromObservableMode) + "()")]
        [SerializeField]
        protected T staticValue = default;

        protected virtual void Awake()
        {
            if (getValueMode == GetValueMode.getFromObservable)
            {
                if (!isBroadcastOnce)
                {
                    source.i.Subscribe((x) => { SetValue(x); Broadcast(); }).AddTo(this);
                }
                else
                {
                    source.i.Single().Subscribe((x) => { SetValue(x); Broadcast(); });
                }
            }
        }
        protected override void Broadcast()
        {
            Messenger<T>.Broadcast(eventName, GetValue(), messengerMode);
        }

        protected bool IsGetValueFromObservableMode()
        {
            return getValueMode == GetValueMode.getFromObservable;
        }

        public void SetValue(T value)
        {
            staticValue = value;
        }
        public virtual T GetValue()
        {
            return staticValue;
        }
        public enum GetValueMode { staticValue, getFromObservable };
    }
}