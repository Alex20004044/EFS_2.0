using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class UnityEventOnAwake : UnityEventBase
    {
        private void Awake()
        {
            InvokeEvent();
        }
    }
}