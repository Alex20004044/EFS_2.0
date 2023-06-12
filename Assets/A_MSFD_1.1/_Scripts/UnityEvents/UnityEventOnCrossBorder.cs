using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using CorD.SparrowInterfaceField;

namespace MSFD
{
    public class UnityEventOnCrossBorder : UnityEventBase
    {
        [SerializeField]
        InterfaceField<IObservable<float>> targetValue;

        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "description")]
        [SerializeField]
        BorderData[] borderData = new BorderData[1];

        float previousValue = 0;
        private void Awake()
        {
            targetValue.i.Subscribe((x) => OnValueChanged(x));
        }

        void OnValueChanged(float value)
        {
            if (!isActiveAndEnabled)
                return;
            InvokeEvent();
            float delta = value - previousValue;
            for (int i = 0; i < borderData.Length; i++)
            {
                float border = borderData[i].border;
                if (delta > 0 &&
                    value >= border && (border > previousValue
                    || borderData[i].invokeMode == OnCrossBorderInvokeMode.everyChangeInBorderRange))
                {
                    borderData[i].onIncrease.Invoke();
                }
                else if (delta < 0 &&
                    value <= border && (border < previousValue
                    || borderData[i].invokeMode == OnCrossBorderInvokeMode.everyChangeInBorderRange))
                {
                    borderData[i].onDecrease.Invoke();
                }
            }
            previousValue = value;
        }
    }

    [System.Serializable]
    struct BorderData
    {
        public string description;
        public float border;
        public OnCrossBorderInvokeMode invokeMode;
        [FoldoutGroup("Events")]
        public UnityEvent onIncrease;
        [FoldoutGroup("Events")]
        public UnityEvent onDecrease;
    }
    enum OnCrossBorderInvokeMode { onceWhenCrossBorder, everyChangeInBorderRange }
}

