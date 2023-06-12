using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [MessengerEventContainer]
    /// <summary>
    /// Events that can be broadcasted only by Core Scripts
    /// </summary>
    public static class SystemEvents
    {
        #region Scene Load
        public const string I_SCENE_RESTART = "I_SCENE_RESTART";
        public const string I_SCENE_LOADING_STARTED = "I_SCENE_LOADING_STARTED";
        public const string I_SCENE_LOADING_COMPLETED = "I_SCENE_LOADING_COMPLETED";
        public const string I_float_SCENE_LOADING_PROGRESS = "I_float_SCENE_LOADING_PROGRESS";
        public const string I_APPLICATION_QUIT = "I_APPLICATION_QUIT";
        #endregion
        #region Time Core
        public const string I_GAME_PAUSED = "I_GAME_PAUSED";
        public const string I_GAME_CONTINUED = "I_GAME_CONTINUED";
        public const string I_TIME_SCALE_CHANGED = "I_TIME_SCALE_CHANGED";
        #endregion

        #region INCOMPLETED
        [Obsolete]
        public static string AWAKE_LOADING = "AWAKE_LOADING";
        [Obsolete]
        public static string MAIN_MENU = "MAIN_MENU";
        [Obsolete]
        public static string AWAKE_LOADING_COMPLETE = "AWAKE_LOADING_COMPLETE";
        [Obsolete]
        /// <summary>
        /// Broadcasted by ApplicationManager when switch scenes or application fold and unfold
        /// </summary>
        public static string APPLICATION_STATE_IS_CHANGED = "APPLICATION_STATE_IS_CHANGED";
        [Obsolete]
        public static string FLOAT_INIT_LOADING_PROGRESS = "FLOAT_INIT_LOADING_PROGRESS";
        #endregion
    }
}