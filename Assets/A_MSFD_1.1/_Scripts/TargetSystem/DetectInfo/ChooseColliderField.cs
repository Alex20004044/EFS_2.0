using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MSFD
{
    [System.Serializable]
    public struct ChooseColliderField
    {
        [SerializeField]
        List<Collider> colliders;
        [GUIColor("@" + nameof(GetColor) + "()")]
        [SerializeField]
        bool invertSelection;

        //[Button]
        public bool IsCorrect(Collider collider)
        {
            return colliders.Count == 0 || ((colliders.IndexOf(collider) >= 0) != invertSelection);
        }
        #region UNITY_EDITOR
        Color GetColor()
        {
            if (invertSelection)
                return EditorConstants.GetRedColor();
            else
                return EditorConstants.GetGreenColor();
        }
        #endregion
    }
}