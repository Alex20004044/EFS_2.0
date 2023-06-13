using System.Collections;
using System.Collections.Generic;
using MSFD.AS;
using UnityEngine;

namespace EFS
{
    public static class MapUtilities
    {
        public static Vector2 ConvertMeasurePointIndexToPosition(int index, Vector2Int mapSize, int measuresPerUnit)
        {
            Vector2 originPoint = new Vector2(-mapSize.x / 2, -mapSize.y / 2);

            float x = (float)((index % (mapSize.x * measuresPerUnit))) / measuresPerUnit;
            float y = (float)((index / (mapSize.y * measuresPerUnit))) / measuresPerUnit;

            return originPoint + new Vector2(x, y);
        }
        public static int ConvertMeasurePointPositionToIndex(Vector2 position, Vector2Int mapSize, int measuresPerUnit)
        {
            Vector2 originPoint = new Vector2(-mapSize.x / 2, -mapSize.y / 2);
            Vector2 normalizedPosition = position - originPoint;

            return (int)(normalizedPosition.x + normalizedPosition.y * mapSize.y) * measuresPerUnit;
        }

        public static Vector3 CalculateIntesityV3(Vector3 position, ChargePoint[] charges)
        {
            position = position.SetYAxis(0);
            Vector3 electricVector = new Vector3();
            for (int i = 0; i < charges.Length; i++)
            {
                Vector3 delta = position - charges[i].transform.position;
                float rangeSqr = delta.sqrMagnitude;

                if (rangeSqr < SimulationConstants.INFINITY_INTENSITY_TRESHOLD_DISTANCE)
                {
                    if (charges[i].GetCharge() > 0)
                        return Vector3.positiveInfinity.SetYAxis(0);
                    else if (charges[i].GetCharge() > 0)
                        return Vector3.negativeInfinity.SetYAxis(0);
                    else 
                        continue;
                }
                

                electricVector += delta.normalized * charges[i].GetCharge() / rangeSqr;
            }

            return electricVector.SetYAxis();
        }

/*        public static Vector2 CalculateIntesity(Vector2 position, ChargePoint[] charges)
        {
            Vector2 electricVector = new Vector2();
            for (int i = 0; i < charges.Length; i++)
            {
                Vector2 delta = position - Coordinates.ConvertVector3ToVector2(charges[i].transform.position);
                float rangeSqr = delta.sqrMagnitude;
                electricVector += delta.normalized * charges[i].GetCharge() / rangeSqr;
            }

            return electricVector;
        }*/

        public static int GetLinePointIndex(int lineIndex, int pointIndex, int numSteps)
        {
            return pointIndex + lineIndex * numSteps;
        }

        public static Vector3 GetElectricForce(Vector3 currentPosition, ChargePoint[] chargePoints)
        {
            Vector3 electricForce = new Vector3();

            for (int i = 0; i < chargePoints.Length; i++)
            {
                Vector3 delta = currentPosition - chargePoints[i].transform.position;
                electricForce += chargePoints[i].GetCharge() * delta.normalized / delta.sqrMagnitude;
            }

            return electricForce;
        }
    }
}
