using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    [MessengerEventContainer]
    public static class GameEvents
    {
        public const string R_BEGIN_GAME = "R_BEGIN_GAME";
        public const string I_GAME_AWAKED = "I_GAME_AWAKED";
        public const string I_GAME_STARTED = "I_GAME_STARTED";


        public const string R_END_GAME = "R_END_GAME";
        /// <summary>
        /// Called when player complete level or died, but there is no level ending screen
        /// </summary>
        public const string I_GAME_COMPLETED = "I_GAME_COMPLETED";
        public const string I_GAME_WON = "I_GAME_WON";
        public const string I_GAME_LOST = "I_GAME_LOST";
        /// <summary>
        /// Called after I_GAME_WON or I_GAME_LOST when ending screen is showing
        /// </summary>
        public const string I_GAME_ENDED = "I_GAME_ENDED";

        public const string I_PLAYER_DIED = "I_PLAYER_DIED";
        public const string I_PLAYER_REVIVED = "I_PLAYER_REVIVED";

        public static string RESTART_GAME = "RESTART_GAME";
    }
}