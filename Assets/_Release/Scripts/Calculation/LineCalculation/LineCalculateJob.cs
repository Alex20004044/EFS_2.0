using MSFD.AS;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace EFS
{
    public struct LineCalculateJob : IJobParallelFor
    {
        public float stepDistance;
        public int numSteps;

        [ReadOnly]
        public NativeArray<Vector3> chargePoints;
        [ReadOnly]
        public NativeArray<Vector2> lineStartPositions;
        [ReadOnly]
        public NativeArray<bool> isLinesPositive;
        [NativeDisableContainerSafetyRestriction]
        public NativeArray<Vector2> lineField;
        public void Execute(int index)
        {
            Vector2 currentPosition = lineStartPositions[index];
            lineField[GetLinePointIndex(index, 0)] = currentPosition;

            for (int i = 1; i < numSteps; i++)
            {
                Vector2 electricDir = GetElectricForce(currentPosition).normalized;
                currentPosition += electricDir * stepDistance * (isLinesPositive[index] ? 1 : -1);
                lineField[GetLinePointIndex(index, i)] = currentPosition;
            }
        }


        Vector2 GetElectricForce(Vector2 currentPosition)
        {
            Vector2 electricForce = new Vector2();

            for (int i = 0; i < chargePoints.Length; i++)
            {
                Vector2 delta = currentPosition - Coordinates.ConvertVector3ToVector2(chargePoints[i], Coordinates.ConvertV3ToV2Mode.y_to_y);
                electricForce += chargePoints[i].z * delta.normalized / delta.sqrMagnitude;
            }

            return electricForce;
        }
        int GetLinePointIndex(int lineIndex, int pointIndex)
        {
            return MapUtilities.GetLinePointIndex(lineIndex, pointIndex, numSteps);
        }
    }
}
