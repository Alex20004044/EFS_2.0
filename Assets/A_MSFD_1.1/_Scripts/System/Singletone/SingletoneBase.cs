using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public abstract class SingletoneBase<T> : StaticInstanceBase<T> where T : class
    {
        protected override void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if(isPersistent)
                    InitSceneLoader.AddToPersistentScene(gameObject);
                AwakeInitialization();
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }
    }
}