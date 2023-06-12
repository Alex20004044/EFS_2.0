using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MSFD
{
    public class UnityEventOnMessengerEvent : UnityEventBase
    {
        [MessengerEvent]
        [Header("Broadcasted by Messenger event name")]
        [SerializeField]
        protected string eventName;

        private void Awake()
        {
            Messenger.AddListener(eventName, InvokeEvent);
        }
        private void OnDestroy()
        {
            Messenger.RemoveListener(eventName, InvokeEvent);
        }
    }
}