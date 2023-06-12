using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [Serializable]
    public class StandartActivator
    {
        [SerializeField]
        ActivationMode activationMode = ActivationMode.awake;

        public StandartActivator(ActivationMode activationMode)
        {
            this.activationMode = activationMode;
        }

        public bool TryActivateOnAwake(Action action = null)
        {
            return TryActivate(ActivationMode.awake, action);
        }
        public bool TryActivateOnStart(Action action = null)
        {
            return TryActivate(ActivationMode.start, action);
        }
        public bool TryActivateOnEnable(Action action = null)
        {
            return TryActivate(ActivationMode.onEnable, action);
        }
        public bool TryActivateExternal(Action action = null)
        {
            return TryActivate(ActivationMode.external, action);
        }

        private bool TryActivate(ActivationMode targetMode, Action action)
        {
            if (targetMode == activationMode)
            {
                action?.Invoke();
                return true;
            }
            return false;
        }
    }
}
