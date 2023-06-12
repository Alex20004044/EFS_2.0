using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class TransformsStreamFind : MonoBehaviour, IObservable<Transform[]>
    {
        [SerializeField]
        DetectInfo detectInfo = new DetectInfo();
        [SerializeField]
        ActivationModeStandart activationMode = ActivationModeStandart.onEnable;

        [ReadOnly]
        [ShowInInspector]
        List<Transform>targets = new List<Transform>();

        Subject<Transform[]> subject = new Subject<Transform[]>();

        private void OnEnable()
        {
            if (activationMode == ActivationModeStandart.onEnable)
                FindTargets();
        }
        [Button]
        public void FindTargets()
        {
            var sceneObjects = FindObjectsOfType<Transform>();
            targets = new List<Transform>();
            foreach (var x in sceneObjects)
            {
                if (detectInfo.IsTransormCorrect(x))
                    targets.Add(x);
            }
            subject.OnNext(targets.ToArray());
        }
        public IDisposable Subscribe(IObserver<Transform[]> observer)
        {
            observer.OnNext(targets.ToArray());
            return subject.Subscribe(observer);
        }

    }
}