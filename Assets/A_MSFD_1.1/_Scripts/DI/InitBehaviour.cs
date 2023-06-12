using System;
using UnityEngine;

namespace MSFD
{
    public abstract class InitBehaviour : MonoBehaviour
    {
        bool isInited = false;
        protected void CheckInit()
        {
            if (isInited)
                throw new InvalidOperationException("Attempt to initialize already initialized class");
            isInited = true;
        }
    }
}
