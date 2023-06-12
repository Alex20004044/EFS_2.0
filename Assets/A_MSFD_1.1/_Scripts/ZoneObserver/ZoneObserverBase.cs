using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
namespace MSFD
{
    [RequireComponent(typeof(Collider))]
    public class ZoneObserverBase : MonoBehaviour, IZoneObserver, IObservable<Transform[]>
    {
        [SerializeField]
        DetectInfo detectInfo;

        [SerializeField]
        UnityEvent onTriggerEnterUE;
        [SerializeField]
        UnityEvent onTriggerExitUE;
        [SerializeField]
        UnityEvent onAllTargetsExitUE;

        public event Action<Transform> onTriggerEnterTransform;
        public event Action<Transform> onTriggerExitTransform;
        public event Action onAllTargetsExit;

        [ShowInInspector]
        [ReadOnly]
        List<Transform> targetsInZone = new List<Transform>();


        Subject<Transform[]> subject = new Subject<Transform[]>();
        protected virtual void Awake()
        {
            //Observable.FromEvent(x => onAllTargetsExit += x, x => onAllTargetsExit -= x).
            //    Subscribe(_ => onAllTargetsExitUE.Invoke());
            //
            //Observable.FromEvent<Transform>(x => onTriggerEnterTransform += x, x => onTriggerEnterTransform -= x).
            //    Subscribe(_ => onTriggerEnterUE.Invoke());
            //
            //Observable.FromEvent<Transform>(x => onTriggerExitTransform += x, x => onTriggerExitTransform -= x).
            //    Subscribe(_ => onTriggerExitUE.Invoke());

            onTriggerEnterTransform += (x) => onTriggerEnterUE.Invoke();
            onTriggerExitTransform += (x) => onTriggerExitUE.Invoke();
            onAllTargetsExit += () => onAllTargetsExitUE.Invoke();
        }
           
        protected virtual void OnDisable()
        {
            ResetTargetsList();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (detectInfo.IsTargetCorrect(other) && TriggerEnterAdditionalChecks(other))
            {
                targetsInZone.Add(other.transform);
                onTriggerEnterTransform?.Invoke(other.transform);
                //onTriggerEnterUE.Invoke();
            }
        }
        /// <summary>
        /// There can be added additional cheks or actions
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected virtual bool TriggerEnterAdditionalChecks(Collider other)
        {
            return true;
        }
        private void OnTriggerExit(Collider other)
        {
            TryRemoveUnit(other);
        }
        protected void TryRemoveUnit(Collider other)
        {
            int targetIndex = targetsInZone.IndexOf(other.transform);
            if (targetIndex >= 0)
            {
                targetsInZone.RemoveAt(targetIndex);
                onTriggerExitTransform?.Invoke(other.transform);
                //onTriggerExitUE.Invoke();

                if (targetsInZone.Count == 0)
                {
                    onAllTargetsExit?.Invoke();
                    //onAllTargetsExitUE.Invoke();
                }
            }
        }
        [FoldoutGroup("Debug")]
        [Button]
        public List<Transform> GetTargetsInZone()
        {
            for (int i = 0; i < targetsInZone.Count; i++)
            {
                if (targetsInZone[i] == null || !targetsInZone[i].gameObject.activeSelf)
                {
                    targetsInZone.RemoveAt(i);
                }
            }
            return targetsInZone;
        }
        [FoldoutGroup("Debug")]
        [Button]
        public void ResetTargetsList()
        {
            targetsInZone.Clear();
        }

        public IDisposable Subscribe(IObserver<Transform[]> observer)
        {
            observer.OnNext(targetsInZone.ToArray());
            return subject.Subscribe(observer);
        }
    }
}