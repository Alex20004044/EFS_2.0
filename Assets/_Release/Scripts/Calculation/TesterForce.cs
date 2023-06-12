using System.Collections;
using System.Collections.Generic;
using CorD.SparrowInterfaceField;
using MSFD;
using MSFD.AS;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EFS
{
    public class TesterForce : MonoBehaviour
    {
        ChargePoint[] chargePoints;

        bool isActivated;


        private void Start()
        {
            chargePoints = FindObjectsOfType<ChargePoint>();
            isActivated = true;

            Messenger.Subscribe(GameEvents.I_CHARGE_UPDATED, OnChargeDestroyed);
        }

        void OnChargeDestroyed()
        {
            chargePoints = FindObjectsOfType<ChargePoint>();
        }

        void Update()
        {
            if (!isActivated) return;

            //int index = MapUtilities.ConvertMeasurePointPositionToIndex(new Vector2(transform.position.x, transform.position.z),
            //     electricFieldCalculator.GetMapSize(), electricFieldCalculator.GetMeasuresPerUnit());
            Vector3 electricForce = MapUtilities.CalculateIntesityV3(transform.position, chargePoints);
            //float angle = Coordinates.AngleFromDirection(electricForce);
            Quaternion rotation = Quaternion.LookRotation(electricForce, Vector3.up);

            transform.rotation = rotation;
        }
    }
}
