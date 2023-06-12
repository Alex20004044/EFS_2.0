using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class UnityEventOnStart : UnityEventBase
    {
        private void Start()
        {
            InvokeEvent();
        }
    }
}