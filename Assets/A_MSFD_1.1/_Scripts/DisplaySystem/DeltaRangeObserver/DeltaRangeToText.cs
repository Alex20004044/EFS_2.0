using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

namespace MSFD
{
    public class DeltaRangeToText : FieldObserverToBase<IDeltaRange<float>>
    {
        [SerializeField]
        TMP_Text text;
        [SerializeField]
        DisplayMode displayMode = DisplayMode.number;
        [ShowIf("@displayMode == DisplayMode.number")]
        [SerializeField]
        bool isHideZeroMinValue = true;
        [ShowIf("@displayMode == DisplayMode.number")]
        [SerializeField]
        string numberSeparator = "/";
        [ShowIf("@displayMode == DisplayMode.number")]
        [SerializeField]
        string format = string.Empty;
        [HideIf("@displayMode == DisplayMode.number")]
        [SerializeField]
        string percentagePostfix = "%";

        [Header("Text before float value")]
        [SerializeField]
        string prefixText = string.Empty;
        [SerializeField]
        string postfixText = string.Empty;

        public override void OnNext(IDeltaRange<float> value)
        {
            string output = prefixText;
            if (displayMode == DisplayMode.number)
            {
                if (!isHideZeroMinValue || value.MinBorder != 0)
                    output += value.MinBorder.ToString(format) + numberSeparator;
                output += value.Value.ToString(format) + numberSeparator + value.MaxBorder.ToString(format);
            }
            else
            {
                output = Mathf.RoundToInt(AS.Calculation.Map(value.Value, value.GetMinBorderModField().Value, value.GetMaxBorderModField().Value,
                    0, 100)) + percentagePostfix;
            }
            text.text = output + postfixText;
        }
        //Is this correct?
        enum DisplayMode { number, percent };
    }
}