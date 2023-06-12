using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.AS
{
    public static class UIUtilities
    {
        /// <summary>
        /// Convert euler angle to runtime inspector default representation of angle
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float AlignAngle(float value)
        {
            float angle = value - 180;
            if (angle > 0)
                return angle - 180;
            return angle + 180;
        }
        /// <summary>
        /// Use it to scale sensitivity with screen size
        /// </summary>
        public static float ScreenFactor
        {
            get
            {
                var size = Mathf.Min(Screen.width, Screen.height);
                // If it's 0 or less, it's invalid, so return the default scale of 1.0
                if (size <= 0)
                    return 1.0f;
                // Return reciprocal for easy multiplication
                return 1.0f / size;
            }
        }

    }
}
