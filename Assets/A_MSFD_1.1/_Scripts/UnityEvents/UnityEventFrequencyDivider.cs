using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace MSFD
{
    public class UnityEventFrequencyDivider : UnityEventBase
    {
        [SerializeField]
        int divideValue = 2;

        [ReadOnly]
        [ShowInInspector]
        int recievedSingnals = 0;
        public void RecieveEvent(int value = 1)
        {
            recievedSingnals += value;
            if(recievedSingnals >= divideValue)
            {
                recievedSingnals %= divideValue;
                InvokeEvent();
            }
        }
    }
}
