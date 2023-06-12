using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MSFD
{
    [System.Serializable]
    public struct ChooseLabelField
    {
        [SerializeField]
        string[] labels;
        [GUIColor("@" + nameof(GetColor) + "()")]
        [SerializeField]
        bool invertSelection;

        //[Button]
        public bool IsCorrect(string label)
        {
            return labels.Length == 0 || (AS.Utilities.CompareLabel(label, labels) != invertSelection);
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
