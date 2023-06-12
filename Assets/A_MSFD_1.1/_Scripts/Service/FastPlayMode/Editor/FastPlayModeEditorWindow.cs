using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MSFD
{
    [System.Serializable]
    public class FastPlayModeEditorWindow : OdinEditorWindow
    {
        [InfoBox(@"Require this window opening for correct stayInSceneMode work after every reload assemblies.
            Use EditorPlayModeSettings for more comfortable experience")]
        [OnValueChanged("@" + nameof(OnStayInSceneModeChanged) +"()")]
        [ShowInInspector]
        bool stayInSceneMode = false;
        [ShowInInspector]
        bool reloadDomain = true;
        [ShowInInspector]
        bool reloadScene = true;

        bool isRegisteredInPlayModeChanged = false;
        const string savePath = Service.ServiceConstants.editorDataPath + "FastPlayMode/";
        
        [MenuItem("MSFD/Fast Play Mode")]
        private static void OpenWindow()
        {
            GetWindow<FastPlayModeEditorWindow>().Show();
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        protected override void OnEnable()
        {
            base.OnEnable();
            reloadDomain = !EditorSettings.enterPlayModeOptions.HasFlag(EnterPlayModeOptions.DisableDomainReload);
            reloadScene = !EditorSettings.enterPlayModeOptions.HasFlag(EnterPlayModeOptions.DisableSceneReload);
            stayInSceneMode = SaveCore.Load(savePath + nameof(stayInSceneMode), stayInSceneMode);
            OnStayInSceneModeChanged();         
        }
        private void OnDestroy()
        {
            Save();
        }
        void OnStayInSceneModeChanged()
        {
            //Attention! Hack for safety!

            EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged; 
            EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
            if (stayInSceneMode)
                EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
            else
                EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
        }
        void Save()
        {
            SaveCore.Save(savePath + nameof(stayInSceneMode), stayInSceneMode);
        }

        private void EditorApplication_playModeStateChanged(PlayModeStateChange obj)
        {
#if UNITY_EDITOR
            if (stayInSceneMode)
            {
                EditorWindow.FocusWindowIfItsOpen(typeof(SceneView));
            }
#endif
        }

        [Button]
        public void UpdatePlayModeSettings()
        {
#if UNITY_EDITOR
            if (EditorSettings.enterPlayModeOptionsEnabled)
            {
                int NewSettings = 0;
                if (!reloadDomain) { NewSettings += 1; }
                if (!reloadScene) { NewSettings += 2; }
                EditorSettings.enterPlayModeOptions = (EnterPlayModeOptions)NewSettings;
                Debug.Log("Enter Play Mode Settings changed to: " + EditorSettings.enterPlayModeOptions.ToString());
            }
            else
            {
                Debug.Log("Enter Play Mode Settings not enabled.");
            }
#endif

        }
    }
}