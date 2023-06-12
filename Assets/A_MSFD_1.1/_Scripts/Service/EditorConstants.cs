using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public static class EditorConstants
    {
        public const string editorOnly = "Editor only!";
        public const string eventsGroup = "Events";
        public const string debugGroup = "Debug";
        public static Color GetGreenColor()
        {
            return new Color(0.4901961f, 1f, 0.6352941f);
        }
        public static Color GetRedColor()
        {
            return new Color(0.91f, 0.15f, 0.15f);
        }
        public static Color GetStandartColor()
        {
            return Color.white;
        }
    }
}