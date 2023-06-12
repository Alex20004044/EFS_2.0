using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    /// <summary>
    /// 
    /// </summary>
    public interface IZoneObserver: IObservable<Transform[]>
    {
        event Action<Transform> onTriggerEnterTransform;
        event Action<Transform> onTriggerExitTransform;
        event Action onAllTargetsExit;
        List<Transform> GetTargetsInZone();
        void ResetTargetsList();
    }
}
