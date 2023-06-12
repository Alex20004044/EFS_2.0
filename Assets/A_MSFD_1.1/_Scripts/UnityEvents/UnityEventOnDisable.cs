using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class UnityEventOnDisable : UnityEventBase
    {
        private void OnDisable()
        {
            //Use it instead OnEvent() because of unity OnDisable principles of work 
            onEvent.Invoke();
        }
        
    }
}
