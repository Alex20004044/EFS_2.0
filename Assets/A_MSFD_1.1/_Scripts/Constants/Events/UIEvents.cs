using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace MSFD
{
    [MessengerEventContainer]
    public static class UIEvents
    {
        #region Window Management
        /// <summary>
        /// Empty string means R_PRESS_BACK_BUTTON
        /// </summary>
        public const string R_string_OPEN_WINDOW = "R_string_OPEN_WINDOW";  
        public const string I_BACK_BUTTON_PRESSED = "I_BACK_BUTTON_PRESSED";
        public const string I_string_WINDOW_OPENED = "I_string_WINDOW_OPENED";
        public const string I_string_WINDOW_CLOSED = "I_string_WINDOW_CLOSED";

        //public const string R_string_ActionArray_
        #endregion

        #region Obsolete
        [Obsolete]
        /// <summary>
        /// This event is called when start transition animation has been completed and you can activate end transition animation
        /// </summary>
        public static string START_TRANSITION_COMPLETE = "START_TRANSITION_COMPLETE";
        [Obsolete]
        public static string SHOW_WARNING_WINDOW = "SHOW_WARNING_WINDOW";
        
        #endregion
        
    }
}