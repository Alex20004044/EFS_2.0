using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace MSFD.DebugTool
{
    [Serializable]
    public class DebugEvent
    {

        [HorizontalGroup]
        public string activationKey;
        [HorizontalGroup]
        public bool isActivate;
        [FoldoutGroup("Events")]
        public string name;
        [FoldoutGroup("Events")]
        public UnityEvent unityEvent;

    }
}