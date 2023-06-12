using UnityEngine;

namespace MSFD
{
    /// <summary>
    /// Similiar to singltone, but don't destroy another instance of this class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class StaticInstanceBase<T> : MonoBehaviour where T : class
    {
        [SerializeField]
        protected bool isPersistent = true;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("There is no " + typeof(T) + " in current scene. Add " + typeof(T) + " to current scene to get correct access");

                    Debug.LogError("Autocreation of " + typeof(T) + "...");
                    GameObject go = new GameObject(typeof(T).Name + "_AUTOCREATED");
                    go.AddComponent(typeof(T));
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }
        protected static T _instance;
        /// <summary>
        /// Don't forget call base.Awake() on overriding!
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if (isPersistent)
                    InitSceneLoader.AddToPersistentScene(gameObject);
                AwakeInitialization();
            }
        }
        /// <summary>
        /// Use it instead of Awake
        /// </summary>
        protected virtual void AwakeInitialization()
        {

        }
    }
}