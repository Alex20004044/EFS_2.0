using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
namespace MSFD.DebugTool
{
    /// <summary>
    /// You can use this script in every place where yo need some debug possibilities. Also you can add it to Managers
    /// </summary>
    public class DebugController : MonoBehaviour
    {
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "name")]
        [SerializeField]
        DebugEvent[] debugEvents;
        private void Update()
        {
            foreach (DebugEvent x in debugEvents)
            {
                if (x.isActivate || (!string.IsNullOrEmpty( x.activationKey) && Input.GetKeyDown(x.activationKey)))
                {
                    x.unityEvent.Invoke();
                    x.isActivate = false;
                }
            }
        }
       
    }
}
#endif