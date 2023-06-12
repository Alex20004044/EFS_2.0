using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MSFD;
using MSFD.AS;
using MSFD.Data;
using Sirenix.OdinInspector;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace EFS
{
    public class LineCalculation : MonoBehaviour
    {
        [SerializeField]
        DataField<float> stepDistance = new DataField<float>("Settings/StepDistance");
        [SerializeField]
        int numSteps = 1000;
        [SerializeField]
        int linesCountCoeff = 1;

        ChargePoint[] chargePoints;

        NativeArray<Vector3> chargeSharedData;
        NativeArray<Vector2> lineStartPositions;
        NativeArray<Vector2> lineField;

        [SerializeField]
        GameObject lineRendererPrefab;
        [SerializeField]
        float lineYOffset = 0.005f;

        List<GameObject> lines = new List<GameObject>();

        [Inject]
        void Init(IDataStreamProvider streamProvider)
        {
            stepDistance.SubscribeAndAddTo(streamProvider, this);
        }

        private void OnDestroy()
        {
            Clean();
        }

        [Button]
        public void Activate()
        {
            Clean();

            chargePoints = FindObjectsOfType<ChargePoint>();

            if (chargePoints.Length == 0)
                return;
            chargeSharedData = new NativeArray<Vector3>(chargePoints.Length, Allocator.Persistent);
            for (int i = 0; i < chargePoints.Length; i++)
            {
                chargeSharedData[i] = new Vector3(chargePoints[i].transform.position.x, chargePoints[i].transform.position.z, chargePoints[i].GetCharge());
            }

            lineField = new NativeArray<Vector2>(GetLinesCount() * numSteps, Allocator.Persistent);
            var startPositions = GetLinesStartPositions().Select(x => x.ConvertVector3ToVector2()).ToArray();
            lineStartPositions = new NativeArray<Vector2>(startPositions, Allocator.Persistent);

            JobHandle lineJob = CalculateLinesIntensity();

            lineJob.Complete();
            DrawLines();
        }

        int GetLinesCount()
        {
            return GetLinesStartPositions().Length;
        }
        private Vector3[] GetLinesStartPositions()
        {
            IEnumerable<Vector3> result = Enumerable.Empty<Vector3>();
            foreach (var x in chargePoints)
            {
                result = result.Concat(x.GetLinesStartPositions(linesCountCoeff));
            }
            return result.ToArray();
        }

        JobHandle CalculateLinesIntensity()
        {
            var calculateJob = new LineCalculateJob()
            {
                numSteps = numSteps,
                stepDistance = stepDistance.Value,

                chargePoints = chargeSharedData,
                lineStartPositions = lineStartPositions,
                lineField = lineField
            };

            return calculateJob.Schedule(GetLinesCount(), 0);
        }


        void DrawLines()
        {
            for (int i = 0; i < GetLinesCount(); i++)
            {
                GameObject line = InstantiateCore.Spawn(lineRendererPrefab.gameObject, Coordinates.ConvertVector2ToVector3(lineStartPositions[i]));
                LineRenderer lineRenderer = line.GetComponentInChildren<LineRenderer>();
                lineRenderer.positionCount = numSteps;
                lineRenderer.SetPositions(LinePositions(i));

                lines.Add(line);
            }

            ShowFieldLog();
        }
        Vector3[] LinePositions(int lineIndex)
        {
            Vector3[] positions = new Vector3[numSteps];
            for (int i = 0; i < numSteps; i++)
            {
                positions[i] = Coordinates.ConvertVector2ToVector3(lineField[MapUtilities.GetLinePointIndex(lineIndex, i, numSteps)], lineYOffset);
            }
            return positions;
        }

        [Button]
        void ShowFieldLog()
        {
            string log = "";
            var pos = LinePositions(0);
            for (int i = 0; i < 10 && i < pos.Length; i++)
            {
                log += pos[i].ToString();
            }
            Debug.Log(log);
        }

        [Button]
        public void Clean()
        {
            if (chargeSharedData.IsCreated)
            {
                chargeSharedData.Dispose();
                lineStartPositions.Dispose();
                lineField.Dispose();
            }

            foreach (var x in lines)
                InstantiateCore.Despawn(x);

            lines = new List<GameObject>();
        }
    }
}
