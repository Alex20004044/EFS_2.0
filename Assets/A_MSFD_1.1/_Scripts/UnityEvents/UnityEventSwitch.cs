using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MSFD
{
    public class UnityEventSwitch : UnityEventBase
    {
        [Min(0)]
        [MaxValue("@GetMaxState()")]
        [SerializeField]
        int defaultState = 0;
        [Header("One of this event will be called with base event depending on current state")]
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "description")]
        [SerializeField]
        SwitchEvent[] events = new SwitchEvent[2];

        [ShowInInspector]
        [ReadOnly]
        int currentState;

        private void Awake()
        {
            ResetState();
            onEvent.AddListener(InvokeCorrectSwitch);
        }
        private void OnDestroy()
        {
            onEvent.RemoveListener(InvokeCorrectSwitch);
        }
        public void ActivateSwitch()
        {
            InvokeEvent();
        }

        void InvokeCorrectSwitch()
        {
            events[currentState].stateEvent.Invoke();
        }

        [Button]
        public void SetState(int state)
        {
            if(state < 0)
            {
                Debug.LogError("Error! State is < 0");
                currentState = 0;
            }
            else if(state >= events.Length)
            {
                Debug.LogError("Error! State is >= events.Length");
                currentState = GetMaxState();
            }
            else
                currentState = state;
        }
        [Button]
        public void ResetState()
        {
            SetState(defaultState);
        }
        public void IncState()
        {
            currentState = (currentState + 1) % events.Length;
        }
        int GetMaxState()
        {
            return events.Length - 1;
        }

        [System.Serializable]
        public struct SwitchEvent
        {
            public string description;
            public UnityEvent stateEvent;
        }
    }

}