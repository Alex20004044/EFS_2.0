using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MSFD
{
    public class BoolObserverToUnityEvent : FieldObserverToBase<bool>
    {
        [SerializeField]
        UnityEvent onTrue;        
        [SerializeField]
        UnityEvent onFalse;

        public override void OnNext(bool value)
        {
            if (value)
                onTrue.Invoke();
            else
                onFalse.Invoke();
        }
    }
}