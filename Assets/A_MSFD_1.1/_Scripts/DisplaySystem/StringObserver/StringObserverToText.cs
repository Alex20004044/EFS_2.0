using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MSFD.UI
{
    public class StringObserverToText : FieldObserverToBase<string>
    {
        [SerializeField]
        TMP_Text text;
        [Header("Text before string value")]
        [SerializeField]
        string prefixText = string.Empty;
        [SerializeField]
        string postfixText = string.Empty;

        public override void OnNext(string value)
        {
            text.text = prefixText + value + postfixText;
        }
    }
}
