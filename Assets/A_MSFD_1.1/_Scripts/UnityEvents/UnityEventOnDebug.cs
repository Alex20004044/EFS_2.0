using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class UnityEventOnDebug : UnityEventBase
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        private void Start()
        {
            InvokeEvent();
        }
#endif
    }
}
