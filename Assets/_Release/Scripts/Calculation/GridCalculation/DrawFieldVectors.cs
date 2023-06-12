using System;
using System.Collections;
using System.Collections.Generic;
using CorD.SparrowInterfaceField;
using MSFD;
using MSFD.AS;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EFS
{
    public class DrawFieldVectors : MonoBehaviour
    {
        [SerializeField]
        InterfaceField<IFieldGetter<Vector2[]>> fieldIntensitySource;

        [SerializeField]
        GameObject vectorArrowPrefab;

        [SerializeField]
        int distanceBetweenVectors = 5;
        [SerializeField]
        int mapCoefficient = 10;
        [SerializeField]
        float noInstencitySqrTreshold = 0.000001f;
        [SerializeField]
        Vector2Int mapSize = new Vector2Int(100, 100);

        List<GameObject> vectorArrows = new List<GameObject>();

        [Button]
        void Activate()
        {
            Clear();
            Vector2[] fieldIntensities = fieldIntensitySource.i.GetValue();

            for (int i = -mapSize.x / 2; i < mapSize.x / 2; i += distanceBetweenVectors)
            {
                for (int j = - mapSize.y / 2; j < mapSize.y / 2; j += distanceBetweenVectors)
                {
                    Vector2 electricForce = fieldIntensities[MapUtilities.ConvertMeasurePointPositionToIndex(new Vector2(i, j), mapSize, mapCoefficient)];
                    if (electricForce.sqrMagnitude < noInstencitySqrTreshold)
                        continue;

                    Vector2 position = new Vector2(i, j);
                    Quaternion rotation = Quaternion.AngleAxis(Coordinates.AngleFromDirection(electricForce), Vector3.up);
                    //Quaternion rotation = Quaternion.AngleAxis(Coordinates.AngleFromDirection(electricForce), Vector3.up);

                    GameObject arrow = InstantiateCore.Spawn(vectorArrowPrefab, Coordinates.ConvertVector2ToVector3(position), rotation);
                    vectorArrows.Add(arrow);
                }
            }
        }

        private Vector2 GetOriginPoint()
        {
            return -GetMapSize() / 2f;
        }

        private Vector2 GetMapSize()
        {
            return mapSize;
        }

        [Button]
        void Clear()
        {
            vectorArrows.ForEach(x => InstantiateCore.Despawn(x));
            vectorArrows.Clear();
        }
    }
}
