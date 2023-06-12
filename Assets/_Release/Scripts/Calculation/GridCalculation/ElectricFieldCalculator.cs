using System.Collections;
using System.Collections.Generic;
using MSFD;
using Sirenix.OdinInspector;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace EFS
{
    public class ElectricFieldCalculator : MonoBehaviour, IFieldGetter<Vector2[]>
    {
        ChargePoint[] chargePoints;
        NativeArray<Vector2> fieldIntensities;
        NativeArray<Vector3> chargeSharedData;

        [SerializeField]
        Vector2Int mapSize = new Vector2Int(100, 100);
        [SerializeField]
        int measuresPerUnit = 10;


        private void Awake()
        {
            fieldIntensities = new NativeArray<Vector2>(mapSize.x * mapSize.y * measuresPerUnit, Allocator.Persistent);
            Activate();
        }
        private void OnDestroy()
        {
            fieldIntensities.Dispose();
        }

        [Button]
        void Activate()
        {
            chargePoints = FindObjectsOfType<ChargePoint>();
            chargeSharedData = new NativeArray<Vector3>(chargePoints.Length, Allocator.Persistent);
            for (int i = 0; i < chargePoints.Length; i++)
            {
                chargeSharedData[i] = new Vector3(chargePoints[i].transform.position.x, chargePoints[i].transform.position.z, chargePoints[i].GetCharge());
            }


            var fieldIntensityJob = CalculateFieldIntensity();
            fieldIntensityJob.Complete();

            chargeSharedData.Dispose();

            ShowFieldLog();

        }
        JobHandle CalculateFieldIntensity()
        {
            var calculateJob = new CalculateJob()
            {
                chargePoints = chargeSharedData,
                fieldIntencities = fieldIntensities,
                mapHalfSize = mapSize,
                measuresPerUnit = measuresPerUnit,
            };

            return calculateJob.Schedule(fieldIntensities.Length, 0);
        }
        [Button]
        void ShowFieldLog()
        {
            string log = "";
            for (int i = 0; i < 10 && i < fieldIntensities.Length; i++)
            {
                log += fieldIntensities[i].ToString();
            }
            Debug.Log(log);
        }

        public Vector2[] GetValue()
        {
            return fieldIntensities.ToArray();
        }

        [Button]
        public Vector2 GetMeasurePointPosition(int index)
        {
            var pos = MapUtilities.ConvertMeasurePointIndexToPosition(index, mapSize, measuresPerUnit);
            Debug.Log(pos);
            return pos;
        }

        [Button]
        public int GetMeasurePointIndex(Vector2 position)
        {
            var index = MapUtilities.ConvertMeasurePointPositionToIndex(position, mapSize, measuresPerUnit);
            Debug.Log(index);
            return index;
        }
        [Button]
        public Vector2 GetMeasurePointForce(Vector2 position)
        {
            var force = fieldIntensities[MapUtilities.ConvertMeasurePointPositionToIndex(position, mapSize, measuresPerUnit)];
            Debug.Log(force);
            return force;
        }
        public Vector2Int GetMapSize()
        {
            return mapSize;
        }
        public int GetMeasuresPerUnit()
        {
            return measuresPerUnit;
        }
    }
}
