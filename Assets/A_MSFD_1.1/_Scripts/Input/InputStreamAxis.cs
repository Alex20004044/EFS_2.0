using System;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

namespace MSFD
{
    public class InputStreamAxis: MonoBehaviour, IObservable<float>
    {
        [SerializeField]
        string axisName = "Horizontal";

        [ReadOnly]
        [ShowInInspector]
        ReactiveProperty<float> input = new ReactiveProperty<float>();

        void Update()
        {
            input.Value = Input.GetAxis(axisName);
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return input.Subscribe(observer);
        }
    }
}