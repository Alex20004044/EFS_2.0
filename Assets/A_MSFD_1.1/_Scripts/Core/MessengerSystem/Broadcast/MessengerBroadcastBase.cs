using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public abstract class MessengerBroadcastBase : MonoBehaviour
    {
        [MessengerEvent]
        [SerializeField]
        protected string eventName;
        [SerializeField]
        protected MessengerMode messengerMode = MessengerMode.DONT_REQUIRE_LISTENER;
        [Sirenix.OdinInspector.Button()]//"Manually Broadcast event")]
        public void SendEvent()
        {
#if UNITY_EDITOR
            if (string.IsNullOrWhiteSpace(eventName))
            {
                Debug.LogError("Event name is not installed");
            }
#endif
            Broadcast();
        }

        protected abstract void Broadcast();

        public void SetEventName(string eventName)
        {
            this.eventName = eventName;
        }
        public string GetEventName()
        {
            return eventName;
        }
    }
}