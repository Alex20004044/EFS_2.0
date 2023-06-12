using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MSFD
{
    public class UnityEventOnDestroy : UnityEventBase
    {
        private void OnDestroy()
        {
            //Use it instead OnEvent() because of unity OnDestroy principles of work 
            onEvent.Invoke();
        }
    }
}
