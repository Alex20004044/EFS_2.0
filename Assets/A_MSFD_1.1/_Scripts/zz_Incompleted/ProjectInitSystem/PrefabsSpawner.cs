using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MSFD
{
    public class PrefabsSpawner : MonoBehaviour
    {
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "description")]
        [TableList()]
        [SerializeField]
        PrefabData[] onAwake;
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "description")]
        [TableList()]
        [SerializeField]
        PrefabData[] onStart;
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "description")]
        [TableList()]
        [SerializeField]
        PrefabData[] onExternalCall;


        private void Awake()
        {
            SpawnPrefabs(onAwake);
        }
        private void Start()
        {
            SpawnPrefabs(onStart);
        }
        public void SpawnOnExternalCall()
        {
            SpawnPrefabs(onExternalCall);
        }
        void SpawnPrefabs(PrefabData[] prefabDatas)
        {
            foreach(var x in prefabDatas)
            {
                if (x.isSpawn)
                    Instantiate(x.prefab, transform);
            }
        }
        [System.Serializable]
        class PrefabData
        {
            [SerializeField]
            public bool isSpawn = true;
            [SerializeField]
            public GameObject prefab;
            [SerializeField]
            public string description;
        }
    }
}