using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MSFD
{
    public class Vector2ToText : FieldObserverToBase<Vector2>
    {
        [SerializeField]
        TMP_Text text;
        [Header("Text before float value")]
        [SerializeField]
        string prefixText = string.Empty;
        [SerializeField]
        string postfixText = string.Empty;
        [SerializeField]
        string format = string.Empty;
        public override void OnNext(Vector2 value)
        {
            text.text = prefixText + value.ToString(format) + postfixText;
        }
    }
}