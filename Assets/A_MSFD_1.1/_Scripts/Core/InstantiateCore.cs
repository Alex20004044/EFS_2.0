using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MSFD
{
    public static class InstantiateCore
    {

        static PriorityListAction<SpawnProcessorInfo> spawnProcessors = new PriorityListAction<SpawnProcessorInfo>();
        static PriorityListAction<DespawnProcessorInfo> despawnProcessors = new PriorityListAction<DespawnProcessorInfo>();

        #region Spawn Overload
        public static GameObject SpawnAtPrefabPose(GameObject prefab, Transform parent = null, bool isActive = true) =>
            Spawn(prefab, prefab.transform.position, prefab.transform.rotation, parent, isActive);
        public static GameObject SpawnAtParentPose(GameObject prefab, Transform parent, bool isActive = true) =>
            Spawn(prefab, parent.position, parent.rotation, parent, isActive);
        public static GameObject Spawn(GameObject prefab, Pose pose, Transform parent = null, bool isActive = true) =>
            Spawn(prefab, pose.position, pose.rotation, parent, isActive);
        #endregion
        public static GameObject Spawn(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null, bool isActive = true)
        {
            //Whether this check is neccessary?
            Quaternion correctRotation = default(Quaternion).Equals(rotation) ? Quaternion.identity : rotation;

            if (spawnProcessors.Count() == 0)
                return DefaultSpawn(prefab, position, correctRotation, parent, isActive);

            SpawnProcessorInfo spawnProcessorInfo = new SpawnProcessorInfo(prefab, position, correctRotation, parent, isActive);
            return spawnProcessors.Calculate(spawnProcessorInfo).spawnedGameObject;
        }
        //if time < 0 means requset for immediate destroy
        public static void Despawn(GameObject gameObject, float time = 0)
        {
            if (despawnProcessors.Count() == 0)
            {
                DefaultDespawn(gameObject, time);
                return;
            }
            DespawnProcessorInfo despawnProcessorInfo = new DespawnProcessorInfo(gameObject, time);
            despawnProcessors.Calculate(despawnProcessorInfo);
        }

        #region SpawnProcessors

        /// <summary>
        /// Use for actions when instantiate GameObject
        /// Lower priority values will be processed before higher values.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="priority"></param>
        public static IDisposable AddSpawnProcessor(Action<SpawnProcessorInfo> spawnProcessor, int priority)
        {
            return spawnProcessors.Add(spawnProcessor, priority);
        }
        /// <summary>
        /// Use for actions when destroy GameObject
        /// Lower priority values will be processed before higher values.</summary>
        /// <param name="despawnProcessor"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public static IDisposable AddDespawnProcessor(Action<DespawnProcessorInfo> despawnProcessor, int priority)
        {
            return despawnProcessors.Add(despawnProcessor, priority);
        }

        [Obsolete("Experimental API. Can be changed in future")]
        public static IPriorityList<Action<SpawnProcessorInfo>> GetSpawnProcessor() => spawnProcessors;
        [Obsolete("Experimental API. Can be changed in future")]
        public static IPriorityList<Action<DespawnProcessorInfo>> GetDepawnProcessor() => despawnProcessors;
        #endregion
        #region Default
        static GameObject DefaultSpawn(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null, bool isActive = true)
        {
            var go = Object.Instantiate(prefab, position, rotation, parent);
            go.SetActive(isActive);
            return go;
        }
        static void DefaultDespawn(GameObject gameObject, float time = 0)
        {
            if (time < 0)
                Object.DestroyImmediate(gameObject);
            else
                Object.Destroy(gameObject, time);
        }

        #endregion
    }
    #region Processor Info
    public class SpawnProcessorInfo
    {
        public GameObject spawnedGameObject;

        public GameObject prefab;
        public Vector3 position;
        public Quaternion rotation;
        public Transform parent;
        public bool isActive;

        //public IDataProvider infoProvider;

        public SpawnProcessorInfo(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent, bool isActive)//, IDataProvider infoProvider)
        {
            this.prefab = prefab;
            this.position = position;
            this.rotation = rotation;
            this.parent = parent;
            this.isActive = isActive;
            //this.infoProvider = infoProvider;
        }
    }
    public class DespawnProcessorInfo
    {
        public GameObject despawnedGameObject;
        public float despawnTime;

        public DespawnProcessorInfo(GameObject despawnedDameObject, float despawnTime)
        {
            this.despawnedGameObject = despawnedDameObject;
            this.despawnTime = despawnTime;
        }
    }
    #endregion
}