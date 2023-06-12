using MSFD;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessengerBroadcastDeltaRange : MessengerBroadcastObserverBase<IDeltaRange<float>>
{
    [HideIf("@" + nameof(IsGetValueFromObservableMode) + "()")]
    [SerializeField]
    DeltaRange value = new DeltaRange();

    protected override void Awake()
    {
        base.Awake();
        SetValue(value);
    }
}
