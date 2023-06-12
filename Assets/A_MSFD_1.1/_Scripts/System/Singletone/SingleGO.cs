using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class SingleGO : MonoBehaviour
    {
        [SerializeField]
        string singletoneName;
        [SerializeField]
        bool isDontDestroyOnLoad = true;

        static Dictionary<string, GameObject> singletones = new Dictionary<string, GameObject>();
        private void Awake()
        {
            GameObject go;
            if (singletones.TryGetValue(singletoneName, out go))
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                singletones.Add(singletoneName, gameObject);
                if (isDontDestroyOnLoad)
                {
                    InitSceneLoader.AddToPersistentScene(gameObject);
                }
            }
        }
    }
}