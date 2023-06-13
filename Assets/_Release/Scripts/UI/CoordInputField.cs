using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using MSFD;
using MSFD.AS;
using TMPro;
using UnityEngine;


namespace EFS
{
    public class CoordInputField : MonoBehaviour
    {
        [SerializeField]
        GameObject inputFieldsParent;
        [SerializeField]
        TMP_InputField xInputField;
        [SerializeField]
        TMP_InputField yInputField;

        [SerializeField]
        TMP_InputField chargeInputField;


        ISelectionSource selectionSource;

        bool isSelected;

        [Inject]
        public void Init(ISelectionSource selectionSource)
        {
            this.selectionSource = selectionSource;
        }

        private void Awake()
        {
            xInputField.onSelect.AddListener(x => OnSelect());
            xInputField.onDeselect.AddListener(x => OnDeselect());
            xInputField.onSubmit.AddListener(x => OnEnterTextX(x));

            yInputField.onSelect.AddListener(x => OnSelect());
            yInputField.onDeselect.AddListener(x => OnDeselect());
            yInputField.onSubmit.AddListener(x => OnEnterTextY(x));

            chargeInputField.onSelect.AddListener(x => OnSelect());
            chargeInputField.onDeselect.AddListener(x => OnDeselect());
            chargeInputField.onSubmit.AddListener(x => OnEnterTextCharge(x));
        }


        private void OnDestroy()
        {
            xInputField.onSelect.RemoveListener(x => OnSelect());
            xInputField.onDeselect.RemoveListener(x => OnDeselect());
            xInputField.onSubmit.RemoveListener(x => OnEnterTextX(x));

            yInputField.onSelect.RemoveListener(x => OnSelect());
            yInputField.onDeselect.RemoveListener(x => OnDeselect());
            yInputField.onSubmit.RemoveListener(x => OnEnterTextY(x));


            chargeInputField.onSelect.RemoveListener(x => OnSelect());
            chargeInputField.onDeselect.RemoveListener(x => OnDeselect());
            chargeInputField.onSubmit.RemoveListener(x => OnEnterTextCharge(x));
        }

        private void OnEnterTextX(string x)
        {
            if (float.TryParse(x, NumberStyles.Any, CultureInfo.InvariantCulture, out float coord))
                selectionSource.GetSelection().transform.position = selectionSource.GetSelection().transform.position.SetXAxis(coord);
        }
        private void OnEnterTextY(string x)
        {
            if (float.TryParse(x, NumberStyles.Any, CultureInfo.InvariantCulture, out float coord))
                selectionSource.GetSelection().transform.position = selectionSource.GetSelection().transform.position.SetZAxis(coord);
        }
        private void OnEnterTextCharge(string x)
        {
            if (int.TryParse(x, NumberStyles.Any, CultureInfo.InvariantCulture, out int input))
            {
                GameObject selectedObject = selectionSource.GetSelection();

                if (selectedObject.TryGetComponent<ChargePoint>(out var chargePoint))
                    chargePoint.SetCharge(input);
            }
        }

        private void OnSelect()
        {
            isSelected = true;
        }
        void OnDeselect()
        {
            isSelected = false;
        }
        void LateUpdate()
        {
            GameObject selection = selectionSource.GetSelection();
            if (selection == null)
            {
                inputFieldsParent.SetActive(false);
                return;
            }
            if (isSelected)
                return;

            inputFieldsParent.SetActive(true);

            xInputField.text = "X: " + selection.transform.position.x;
            yInputField.text = "Y: " + selection.transform.position.z;
            ChargePoint chargePoint = selection.GetComponent<ChargePoint>();
            if (chargePoint != null)
                chargeInputField.text = "Charge: " + chargePoint.GetCharge();
            else if(selection.TryGetComponent<TesterForce>(out var testerForce))
                chargeInputField.text = testerForce.GetForce().magnitude.ToString();
            else
                chargeInputField.text = "";
        }
    }
}
