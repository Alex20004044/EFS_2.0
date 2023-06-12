using System.Collections;
using System.Collections.Generic;
using MSFD;
using UnityEngine;

namespace EFS
{
    public class DeleteAllCharges : MonoBehaviour, IActivatable
    {
        public void Activate()
        {
            var charges = FindObjectsOfType<ChargePoint>();
            foreach(var x in charges)
                InstantiateCore.Despawn(x.gameObject);
        }

    }
}
