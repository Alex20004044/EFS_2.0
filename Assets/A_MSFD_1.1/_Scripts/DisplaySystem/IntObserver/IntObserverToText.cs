using TMPro;
using UnityEngine;

namespace MSFD
{
    public class IntObserverToText : FieldObserverToBase<int>
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

        public override void OnNext(int value)
        {
            text.text = prefixText + value.ToString(format) + postfixText;
        }
    }
}