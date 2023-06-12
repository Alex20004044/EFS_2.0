using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public abstract class UnityEventOnMessengerEventBase<T> : UnityEventBase
    {
        [MessengerEvent]
        [Header("Activate unityEvent when messenger event is recieved if script is active and argument is equal targetValue")]
        [SerializeField]
        string eventName;

        [SerializeField]
        T targetValue;

        private void Awake()
        {
            Messenger<T>.AddListener(eventName, OnRecieveMessengerEvent);
        }
        private void OnDestroy()
        {
            Messenger<T>.RemoveListener(eventName, OnRecieveMessengerEvent);
        }
        public void OnRecieveMessengerEvent(T arg)
        {
            if (arg.Equals(targetValue))
            {
                InvokeEvent();
            }
        }
    }
}