using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public static class TimeCore
    {
        static int pauseCount = 0;
        static float previousTimeScale = 1;

        public static int PauseGame()
        {
            if(!Application.isPlaying)
            {
                Debug.LogError("Attempt to pause game when it is not running");
                return pauseCount;
            }

            if (pauseCount == 0)
            {
                previousTimeScale = UnityEngine.Time.timeScale;
                UnityEngine.Time.timeScale = 0;
                Debug.Log("Game is paused");
                Messenger.Broadcast(SystemEvents.I_GAME_PAUSED, MessengerMode.DONT_REQUIRE_LISTENER);
            }
            pauseCount++;
            return pauseCount;
        }
        public static int ContinueGame(bool isDebugLogErrors = true)
        {
            pauseCount--;
            if (pauseCount == 0)
            {
                UnityEngine.Time.timeScale = previousTimeScale;
                Messenger.Broadcast(SystemEvents.I_GAME_CONTINUED, MessengerMode.DONT_REQUIRE_LISTENER); 
                Debug.Log("Game is continued");
            }
            else if (pauseCount < 0)
            {
                if(isDebugLogErrors)
                    Debug.LogError("ContinueGame() is called when game is not paused!");
                pauseCount = 0;
            }

            return pauseCount;
        }
        public static void DisableAllPauses()
        {
            pauseCount = 0;
            UnityEngine.Time.timeScale = previousTimeScale;
            Messenger.Broadcast(SystemEvents.I_GAME_CONTINUED, MessengerMode.DONT_REQUIRE_LISTENER);
        }
        public static void FullReset()
        {
            DisableAllPauses();
            SetTimeScale(1);
        }
        public static bool IsGamePaused()
        {
            return pauseCount != 0;
        }
        public static int GetPauseCount()
        {
            return pauseCount;
        }
        public static void SetTimeScale(float timeScale)
        {
            if(!Application.isPlaying)
            {
                previousTimeScale = 1;
                UnityEngine.Time.timeScale = 1;
                Debug.LogError("Attempt to change time scale while game is not running");
                return;
            }

            if (IsGamePaused())
                previousTimeScale = timeScale;
            else
                UnityEngine.Time.timeScale = timeScale;
            Debug.Log("Time scale is changed");
            Messenger.Broadcast(SystemEvents.I_TIME_SCALE_CHANGED, MessengerMode.DONT_REQUIRE_LISTENER);
        }
        public static float GetTimeScale()
        {
            if (IsGamePaused())
                return previousTimeScale;
            else
                return UnityEngine.Time.timeScale;
        }
    }
}