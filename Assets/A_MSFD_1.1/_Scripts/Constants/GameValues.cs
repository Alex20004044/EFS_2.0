using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    /// <summary>
    /// Need refactor
    /// </summary>
    public static class GameValues
    {

        #region Tags
        public const string playerTag = "Player";
        public const string enemyTag = "Enemy";
        #endregion

        #region Layers
        public const string unitLayer = "Unit";
        public const string obstacleLayer = "Obstacle";
        public const string environmentLayer = "Environment";
        public const string displayLayer = "Display";

        //Move it to AS
        public static LayerMask ReturnUnitLayerMask()
        {
            return 1 << LayerMask.NameToLayer(unitLayer);
        }
        public static LayerMask ReturnObstacleLayerMask()
        {
            return 1 << LayerMask.NameToLayer(obstacleLayer);
        }
        public static LayerMask ReturnEnvironmentLayerMask()
        {
            return 1 << LayerMask.NameToLayer(environmentLayer);
        }
        public static LayerMask ReturnDisplayLayerMask()
        {
            return 1 << LayerMask.NameToLayer(displayLayer);
        }

        #endregion

        #region Scene Names
        public const string loadingSceneName = "Loading";
        public const string mainMenuName = "Menu";
        #endregion

        #region World Constants
        public const float zeroLevel = 0;
        #endregion
        #region Constants
        public const float damageZoneDelay = 0.1f;

        public const float clipUpdateRate = 0.1f;
        #endregion

        #region Input
        public const string moveJoystickInputControllerName = "Move";
        public const string shootJoystickInputControllerName = "Aim";

        public const string horMoveAxisName = "HorizontalMove_";
        public const string vertMoveAxisName = "VerticalMove_";

        public const string horShootAxisName = "HorizontalAim_";
        public const string vertShootAxisName = "VerticalAim_";
        #endregion

    }
}
