using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class ZoneObserverUnit : ZoneObserverBase
    {
        protected override bool TriggerEnterAdditionalChecks(Collider other)
        {
            IHP unitHP = other.GetComponent<IHP>();
            if (unitHP != null)
            {
                Collider t = other;
                unitHP.GetHP().GetObsOnMinBorder().Single().Subscribe((x) => RemoveProcess(unitHP, t));
            }
            return true;
        }


        void RemoveProcess(IHP unitHP, Collider t)
        {
            TryRemoveUnit(t);
        }
    }
}
