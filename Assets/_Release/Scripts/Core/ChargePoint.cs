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
            Messenger.Broadcast(GameEvents.I_CHARGE_UPDATED, MessengerMode.DONT_REQUIRE_LISTENER);
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

        public int GetLinesCount(int linesCoefficient)
        {
            return linesCoefficient * SimulationConstants.LINES_PER_CHARGE * Math.Abs(GetCharge());
        }
        public LineStartPosition[] GetLinesStartPositions(int linesCoefficient)
        {
            ChargePoint[] targetChargePoints = FindObjectsOfType<ChargePoint>();
            Vector2 mainForceDir;
            if (targetChargePoints.Sum(x=> Math.Abs(x.GetCharge())) > 1)
            {
                targetChargePoints = targetChargePoints.Where(x => !x.Equals(this)).ToArray();
                mainForceDir = Coordinates.ConvertVector3ToVector2(MapUtilities.GetElectricForce(transform.position, targetChargePoints)).normalized;
            }
            else
                mainForceDir = new Vector2(0, 1);

            int linesCount = GetLinesCount(linesCoefficient);
            LineStartPosition[] linesStartPosition = new LineStartPosition[linesCount];

            Vector3 startPoint = Coordinates.ConvertVector2ToVector3(mainForceDir) * SimulationConstants.LINE_START_OFFSET;
            for (int i = 0; i < linesCount; i++)
            {
                linesStartPosition[i].position = transform.position + Quaternion.AngleAxis(360f / linesCount * i, Vector3.up) * startPoint;
                linesStartPosition[i].isChargePositive = GetCharge() > 0;
            }
            return linesStartPosition;

        }
    }
}
