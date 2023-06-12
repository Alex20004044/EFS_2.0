using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EFS
{
    public class ChargePoint : MonoBehaviour
    {
        [SerializeField]
        int charge = 1;
        [SerializeField]
        UnityEvent onChargeChanged;
        private void Awake()
        {
            onChargeChanged.Invoke();
        }
        public void SetCharge(int value)
        {
            charge = value;
            onChargeChanged.Invoke();
        }
        public int GetCharge()
        {
            return charge;
        }
    }
}
