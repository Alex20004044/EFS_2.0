using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    /// <summary>
    /// Set callback mode in particle system to provide script work
    /// </summary>
    public class UnityEventOnParticleSystemStopped : UnityEventBase
    {
#if UNITY_EDITOR
        [SerializeField]
        [System.Obsolete][Sirenix.OdinInspector.ReadOnly]
        string message = "Set callback mode in particle system to provide script work";
#endif
        void OnParticleSystemStopped()
        {
            InvokeEvent();
        }
    }
}
