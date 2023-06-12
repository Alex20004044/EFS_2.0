using CorD.SparrowInterfaceField;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class DamageInZone : MonoBehaviour
    {
        [SerializeField]
        InterfaceField<IZoneObserver> zoneObserver;

        [ShowInInspector]
        public float DamagePerSecond
        {
            get
            {
                return __damagePerSecond;
            }
            set
            {
                __damagePerSecond = value;
                damagePortion = __damagePerSecond * damageDelay;
            }
        }

        [Header("Delay < 0 => disable auto damage")]
        [SerializeField]
        float damageDelay = GameValues.damageZoneDelay;
        [HideInInspector]
        [SerializeField]
        [Obsolete]
        float __damagePerSecond = 10f;

        [PropertyOrder(10)]
        [ReadOnly]
        [ShowInInspector]
        float damagePortion;

        bool isDamageActive = false;
        private void Awake()
        {
            DamagePerSecond = __damagePerSecond;
            zoneObserver.i.onTriggerEnterTransform += OnTargetEnter;
            zoneObserver.i.onAllTargetsExit += OnAllTargetsExit;
        }

        private void OnDestroy()
        {
            zoneObserver.i.onTriggerEnterTransform -= OnTargetEnter;
            zoneObserver.i.onAllTargetsExit -= OnAllTargetsExit;
        }
        private void OnTargetEnter(Transform target)
        {
            if (!isDamageActive)
            {
                isDamageActive = true;
                if(damageDelay >=0 )
                    InvokeRepeating(nameof(Damage), 0, damageDelay);
            }
        }
        void OnAllTargetsExit()
        {
            CancelInvoke();
            isDamageActive = false;
        }
        public void Damage()
        {
            List<Transform> targets = zoneObserver.i.GetTargetsInZone();
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].GetComponent<IHP>().GetHP().Decrease(damagePortion);
            }
        }
        public void SetDamage(float damagePerSecond)
        {
            DamagePerSecond = damagePerSecond;
        }
    }
}