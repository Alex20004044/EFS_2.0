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
        [SerializeField]
        ElectricFieldCalculator electricFieldCalculator;

        [SerializeField]
        Transform arrow;

        ChargePoint[] chargePoints;

        bool isActivated;
        [Button]
        private void Activate()
        {
            chargePoints = FindObjectsOfType<ChargePoint>();
            isActivated = true;
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
