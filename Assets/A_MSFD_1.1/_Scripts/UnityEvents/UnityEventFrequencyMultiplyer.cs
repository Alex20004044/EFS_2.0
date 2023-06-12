using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class UnityEventFrequencyMultiplyer : UnityEventBase
    {
        [SerializeField]
        int callEventsCount = 2;
        [SerializeField]
        float delayBetweenEvents = 0f;

        [Sirenix.OdinInspector.Button]
        public void Activate()
        {
            StartCoroutine(FrequencyMultyply());
        }
        IEnumerator FrequencyMultyply()
        {
            for(int i = 0; i < callEventsCount; i++)
            {
                InvokeEvent();
                if(delayBetweenEvents > 0)
                {
                    yield return new WaitForSeconds(delayBetweenEvents);
                }
            }
        }
    }
}