using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MSFD
{
    public static class ProjectInitConstants
    {
        /// <summary>
        /// Set it to false to prevent from loading
        /// </summary>
        public const bool IS_LOAD_PERSISTENT_SYSTEMS_SCENE = false;
        public const string PERSISTENT_SYSTEMS_SCENE_NAME = "PersistentSystems";

        /// <summary>
        /// Set it to false to prevent from loading
        /// </summary>
        public const bool IS_LOAD_DEVELOPMENT_SYSTEMS_SCENE = false;
        public const string DEVELOPMENT_SYSTEMS_SCENE_NAME = "DevelopmentSystems";

    }
}