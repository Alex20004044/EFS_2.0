using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MSFD
{
    public class BoolObserverToText : FieldObserverToBase<bool>
    {
        [SerializeField]
        TMP_Text text;
        [Header("Text before value")]
        [SerializeField]
        string prefixText = string.Empty;
        [SerializeField]
        string postfixText = string.Empty;

        public override void OnNext(bool value)
        {
            text.text = prefixText + value.ToString() + postfixText;
        }
    }
}