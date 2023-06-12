using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class UnityEventOnDevelopmentBuild : UnityEventBase
    {
#if DEVELOPMENT_BUILD
        private void Start()
        {
            InvokeEvent();
        }
#endif
    }
}
