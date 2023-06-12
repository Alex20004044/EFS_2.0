using UnityEditor;
using UnityEngine;

namespace MSFD
{
    /// <summary>
    /// Created by https://github.com/DVGen/UnityTips
    /// </summary>
    public class EditorPlayModeSettings : MonoBehaviour
    {
        public bool StayInSceneMode = false;
        public bool ReloadDomain = true;
        public bool ReloadScene = true;

        void Awake()
        {
#if UNITY_EDITOR
            if (StayInSceneMode)
            {
                EditorWindow.FocusWindowIfItsOpen(typeof(SceneView));
            }
#endif
        }

        public void UpdatePlayModeSettings()
        {
#if UNITY_EDITOR
            if (EditorSettings.enterPlayModeOptionsEnabled)
            {
                int NewSettings = 0;
                if (!ReloadDomain) { NewSettings += 1; }
                if (!ReloadScene) { NewSettings += 2; }
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