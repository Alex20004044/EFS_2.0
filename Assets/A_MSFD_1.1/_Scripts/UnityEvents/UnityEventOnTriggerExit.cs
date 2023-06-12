using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [RequireComponent(typeof(Collider))]
    public class UnityEventOnTriggerExit : UnityEventBase
    {
        [SerializeField]
        DetectInfo detectInfo;
        void OnTriggerExit(Collider other)
        {
            if (isActiveAndEnabled)
            {
                if (detectInfo.IsTargetCorrect(other))
                {
                    InvokeEvent();
                }
            }
        }
    }
}