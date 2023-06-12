using System.Collections;
using System.Collections.Generic;
using MSFD;
using UnityEngine;

namespace EFS
{
    public class SetChargeIcon : MonoBehaviour, IActivatable
    {
        [SerializeField]
        SpriteRenderer spriteRenderer;
        [SerializeField]
        ChargePoint chargePoint;

        [SerializeField]
        Sprite positive;
        [SerializeField]
        Sprite negative;



        public void Activate()
        {
            spriteRenderer.enabled = true;
            if (chargePoint.GetCharge() > 0)
                spriteRenderer.sprite = positive;
            else if (chargePoint.GetCharge() < 0)
                spriteRenderer.sprite = negative;
            else
                spriteRenderer.enabled = false;
        }


    }
}
