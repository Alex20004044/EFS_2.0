using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [RequireComponent(typeof(Collider))]
    public class UnityEventOnTriggerEnter : UnityEventBase
    {
        [SerializeField]
        DetectInfo detectInfo;
        void OnTriggerEnter(Collider other)
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
