using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class InputStreamMouse : MonoBehaviour, IObservable<float>
    {
        [SerializeField]
        WorkMode workMode = WorkMode.deltaX;

        [ReadOnly]
        [ShowInInspector]
        ReactiveProperty<float> input = new ReactiveProperty<float>();

        void Update()
        {
            Vector3 delta = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            if (workMode == WorkMode.deltaX)
                input.Value = Mathf.Clamp(delta.x, -1, 1);
            else if (workMode == WorkMode.deltaY)
                input.Value = Mathf.Clamp(delta.y, -1, 1);
            else
                input.Value = Mathf.Clamp(delta.magnitude, 0, 1);
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return input.Subscribe(observer);
        }

        enum WorkMode { deltaX, deltaY, deltaDistance};
    }
}