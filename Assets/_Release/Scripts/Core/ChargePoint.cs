using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MSFD;
using MSFD.AS;
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
            Messenger.Broadcast(GameEvents.I_CHARGE_UPDATED);
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

        private void OnDestroy()
        {
            Messenger.Broadcast(GameEvents.I_CHARGE_UPDATED);
        }
        public Vector3[] GetLinesStartPositions(int linesCoefficient)
        {
            ChargePoint[] targetChargePoints = FindObjectsOfType<ChargePoint>();
            Vector2 mainForceDir;
            if (targetChargePoints.Length > 1)
            {
                targetChargePoints = targetChargePoints.Where(x => !x.Equals(this)).ToArray();
                mainForceDir = Coordinates.ConvertVector3ToVector2(MapUtilities.GetElectricForce(transform.position, targetChargePoints)).normalized;
            }
            else
                mainForceDir = new Vector2(0, 1);

            int linesCount = linesCoefficient * SimulationConstants.LINES_PER_CHARGE * Math.Abs(GetCharge());
            Vector3[] linesStartPosition = new Vector3[linesCount];

            Vector3 startPoint = Coordinates.ConvertVector2ToVector3(mainForceDir) * SimulationConstants.LINE_START_OFFSET;
            for (int i = 0; i < linesCount; i++)
            {
                linesStartPosition[i] = transform.position + Quaternion.AngleAxis(360f / linesCount * i, Vector3.up) * startPoint;
            }
            return linesStartPosition;

            /*return new Vector3[]
            {
                transform.position.IncreaseXAxis(0.1f),
                transform.position.IncreaseXAxis(-0.1f),
                transform.position.IncreaseZAxis(0.1f),
                transform.position.IncreaseZAxis(-0.1f),
            };*/
        }
    }
}
