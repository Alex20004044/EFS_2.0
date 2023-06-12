using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MSFD
{
    public static class InitSceneLoader
    {
        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void OnGameStart()
        {
            LoadPersistentScene();
#if UNITY_EDITOR
            LoadDevelopmentScene();
#endif
        }
        static void LoadScene(string name)
        {   
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }

        static void LoadPersistentScene()
        {
            if(ProjectInitConstants.IS_LOAD_PERSISTENT_SYSTEMS_SCENE)
                LoadScene(ProjectInitConstants.PERSISTENT_SYSTEMS_SCENE_NAME);
        }
#if UNITY_EDITOR
        static void LoadDevelopmentScene()
        {
            if (ProjectInitConstants.IS_LOAD_DEVELOPMENT_SYSTEMS_SCENE)
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(ProjectInitConstants.DEVELOPMENT_SYSTEMS_SCENE_NAME)[0]);
                Scene scene = EditorSceneManager.LoadSceneInPlayMode(scenePath, new LoadSceneParameters(LoadSceneMode.Additive, LocalPhysicsMode.None));
            }
        }
#endif


        public static void AddToPersistentScene(GameObject gameObject)
        {
            if (!ProjectInitConstants.IS_LOAD_PERSISTENT_SYSTEMS_SCENE)
            {
                Object.DontDestroyOnLoad(gameObject);
                return;
            }

            var persistentScene = SceneManager.GetSceneByName(ProjectInitConstants.PERSISTENT_SYSTEMS_SCENE_NAME);
            SceneManager.MoveGameObjectToScene(gameObject, persistentScene);           
        }
    }
}