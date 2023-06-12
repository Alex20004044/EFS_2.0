using UnityEngine;
using TMPro;

namespace MSFD.UI
{
    public class FloatObserverToText : FieldObserverToBase<float>
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
       
        public override void OnNext(float value)
        {
            text.text = prefixText + value.ToString(format) + postfixText;
        }
    }
}