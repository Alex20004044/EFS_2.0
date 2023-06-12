using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class UnityEventOnEnable : UnityEventBase
    {
        private void OnEnable()
        {
            InvokeEvent();
        }
    }
}