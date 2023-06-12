using UnityEngine;
using UnityEngine.Events;
using System;
namespace MSFD
{
    public abstract class UnityEventBase : MonoBehaviour
    {
        [Sirenix.OdinInspector.FoldoutGroup("Event")]
        [Header("Script activates unityEvent on condition. Works only when activeAndEnabled")]
        [SerializeField]
        protected UnityEvent onEvent = new UnityEvent();

        protected void InvokeEvent()
        {
            if (this.isActiveAndEnabled)
            {
                onEvent.Invoke();
            }
        }
        public void AddListener(Action action)
        {
            onEvent.AddListener(new UnityAction(() => action.Invoke()));
        }
        public void RemoveListener(Action action)
        {
            onEvent.RemoveListener(new UnityAction(() => action.Invoke()));
        }

        private void Start()
        {
            //Need for enable/disable checkbox
        }

#if UNITY_EDITOR
        [Sirenix.OdinInspector.Button()]
        void ManualEventActivation()
        {
            onEvent.Invoke();
        }
#endif
    }
}