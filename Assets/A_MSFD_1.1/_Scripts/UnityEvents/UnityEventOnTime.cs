using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class UnityEventOnTime : UnityEventBase
    {
        [SerializeField]
        float delayBeforeStart = -1f;
        [Header("If repeat rate < 0, then repeat mode won't work")]
        [SerializeField]
        float repeatRate = -1f;
        [SerializeField]
        ActivationModeStandart activationMode = ActivationModeStandart.onEnable;
        private void OnEnable()
        {
            if (activationMode == ActivationModeStandart.onEnable)
            {
                ActivateTimer();
            }
        }
        private void OnDisable()
        {
            CancelInvoke();
        }
        public void ActivateTimer()
        {
            if (repeatRate >= 0)
            {
                InvokeRepeating("Invoke", delayBeforeStart, repeatRate);
            }
            else
            {
                Invoke("Invoke", delayBeforeStart);
            }
        }

        void Invoke()
        {
            InvokeEvent();
        }
    }
}