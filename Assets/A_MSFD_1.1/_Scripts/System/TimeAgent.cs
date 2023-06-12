using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
namespace MSFD
{
    public class TimeAgent : MonoBehaviour
    {
#if UNITY_EDITOR

        [OnValueChanged("@" + nameof(InspectorSetTimeScale) + "()")]
        [Range(0, 10)]
        [System.Obsolete]
        [SerializeField]
        float setTimeScale = 1;
        [PropertyOrder(-9)]
        [ShowInInspector]
        [System.Obsolete]
        float currentTimeScale { get { return TimeCore.GetTimeScale(); } }
        [HorizontalGroup(order:-10)]
        [ShowInInspector]
        [System.Obsolete]
        bool IsGamePaused { get { return TimeCore.IsGamePaused(); } }
        [HorizontalGroup]
        [ShowInInspector]
        [System.Obsolete]
        int PauseCount { get { return TimeCore.GetPauseCount(); } }

        void InspectorSetTimeScale()
        {
            SetTimeScale(setTimeScale);
        }
#endif
        [SerializeField]
        bool isDebugLogErrors = true;
        [FoldoutGroup("Events")]
        public UnityEvent onPauseGame;
        [FoldoutGroup("Events")]
        public UnityEvent onContinueGame;
        [FoldoutGroup("Events")]
        public UnityEvent onTimeScaleChanged;
        void Awake()
        {
            Messenger.AddListener(SystemEvents.I_GAME_PAUSED, OnPauseGame);
            Messenger.AddListener(SystemEvents.I_GAME_CONTINUED, OnContinueGame);
            Messenger.AddListener(SystemEvents.I_TIME_SCALE_CHANGED, OnTimeScaleChanged);
        }
        void OnDestroy()
        {
            Messenger.RemoveListener(SystemEvents.I_GAME_PAUSED, OnPauseGame);
            Messenger.RemoveListener(SystemEvents.I_GAME_CONTINUED, OnContinueGame);
            Messenger.RemoveListener(SystemEvents.I_TIME_SCALE_CHANGED, OnTimeScaleChanged);
        }
        [HorizontalGroup("ManageButtons")]
        [Button]
        public void PauseGame()
        {
            TimeCore.PauseGame();
        }
        public void ContinueGameIfPossible()
        {
            if (TimeCore.IsGamePaused())
                ContinueGame();
        }
        public void PauseGameIfNotPaused()
        {
            if (!TimeCore.IsGamePaused())
                PauseGame();
        }
        [HorizontalGroup("ManageButtons")]
        [Button]
        public void ContinueGame()
        {
            TimeCore.ContinueGame(isDebugLogErrors);
        }
        public void DisableAllPauses()
        {
            TimeCore.DisableAllPauses();
        }
        public void FullReset()
        {
            TimeCore.FullReset();
        }
        public void SetTimeScale(float timeScale)
        {
            TimeCore.SetTimeScale(timeScale);
        }
        public float GetTimeScale()
        {
            return TimeCore.GetTimeScale();
        }
        [FoldoutGroup("Debug")]
        [Button]
        void OnPauseGame()
        {
            onPauseGame.Invoke();
        }
        [FoldoutGroup("Debug")]
        [Button]
        void OnContinueGame()
        {
            onContinueGame.Invoke();
        }
        [FoldoutGroup("Debug")]
        [Button]
        void OnTimeScaleChanged()
        {
            onTimeScaleChanged.Invoke();
        }
    }
}