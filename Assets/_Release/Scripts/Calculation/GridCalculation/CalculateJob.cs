using System;
using System.Collections;
using System.Collections.Generic;
using MSFD.AS;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace EFS
{
    public struct CalculateJob : IJobParallelFor
    {
        [ReadOnly]
        public NativeArray<Vector3> chargePoints;

        [WriteOnly]
        public NativeArray<Vector2> fieldIntencities;
        public Vector2Int mapHalfSize;
        public int measuresPerUnit;
        public void Execute(int index)
        {
            Vector2 electricVector = new Vector2();
            Vector2 pointPosition = MapUtilities.ConvertMeasurePointIndexToPosition(index, mapHalfSize, measuresPerUnit);
            for (int i = 0; i < chargePoints.Length; i++)
            {
                Vector2 delta = pointPosition - Coordinates.ConvertVector3ToVector2(chargePoints[i], Coordinates.ConvertV3ToV2Mode.y_to_y);
                float rangeSqr = delta.sqrMagnitude;
                electricVector += delta * chargePoints[i].z / rangeSqr;
            }

            fieldIntencities[index] = electricVector;
        }
    }
}
