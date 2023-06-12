using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class UnityEventOnActivate : UnityEventBase, IActivatable
    {
        [SerializeField]
        ActivationMode activateMode = ActivationMode.once;
        bool isWasActivated = false;
        /// <summary>
        /// Allows repeat activate in "once" activate mode
        /// </summary>
        public void AllowActivation()
        {
            isWasActivated = false;
        }
        public void SetActivateMode(ActivationMode _activateMode)
        {
            activateMode = _activateMode;
        }

        public void Activate()
        {
            if (activateMode == ActivationMode.once && isWasActivated)
                return;
            isWasActivated = true;
            InvokeEvent();
        }
        public enum ActivationMode { once, multiple };
    }
}