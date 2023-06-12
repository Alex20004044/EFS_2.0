using System.Collections;
using System.Collections.Generic;
using Lean.Common;
using UnityEngine;

namespace EFS
{
    public class SelectMachine : MonoBehaviour
    {
        [SerializeField]
        RectTransform chooseWheelPrefab;

        RectTransform chooseWheel;

        LeanSelectable selectable;
        private void Awake()
        {
            chooseWheel = Instantiate(chooseWheelPrefab, transform);
            chooseWheel.gameObject.SetActive(false);
        }

        public void Select(LeanSelectable leanSelectable)
        {
            selectable = leanSelectable;
            chooseWheel.gameObject.SetActive(true);
        }

        public void Deselect(LeanSelectable leanSelectable)
        {
            selectable = null;
            chooseWheel.gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            if (selectable == null) return;
            chooseWheel.position = Camera.main.WorldToScreenPoint(selectable.transform.position);
        }
    }
}
