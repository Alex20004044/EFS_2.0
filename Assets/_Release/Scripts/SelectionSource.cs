using System.Collections;
using System.Collections.Generic;
using Lean.Common;
using UnityEngine;

namespace EFS
{
    public class SelectionSource : MonoBehaviour, ISelectionSource
    {
        [SerializeField]
        LeanSelect leanSelect;

        GameObject selection;
        public void OnSelect(LeanSelectable selectable)
        {
            selection = selectable.gameObject;
        }

        public void OnDeselect(LeanSelectable selectable)
        {
            selection = null;
        }

        public GameObject GetSelection()
        {
            return selection;
        }

        public void DeselectAll()
        {
            leanSelect.DeselectAll();
        }
    }
}
