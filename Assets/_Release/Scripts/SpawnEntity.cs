using System;
using System.Collections;
using System.Collections.Generic;
using MSFD;
using UnityEngine;

namespace EFS
{
    public class SpawnEntity : MonoBehaviour, IActivatable
    {
        [SerializeField]
        GameObject prefab;

        [SerializeField]
        float checkOffset = 2;

        [SerializeField]
        LayerMask obstacleLayerMask;
        public void Activate()
        {
            Vector3 spawnPosition = GetSpawnPosition();

            GameObject entity = InstantiateCore.Spawn(prefab, spawnPosition);
        }

        Vector3 GetSpawnPosition()
        {
            Vector3 spawnPosition = Vector3.zero;
            while(true)
            {
                if (!Physics.CheckSphere(spawnPosition, 1, obstacleLayerMask))
                    return spawnPosition;
                spawnPosition.x += checkOffset;
            }
        }
    }
}
