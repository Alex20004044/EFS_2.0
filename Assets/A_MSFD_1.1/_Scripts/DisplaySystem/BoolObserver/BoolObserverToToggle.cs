using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MSFD
{
    public class BoolObserverToToggle : FieldObserverToBase<bool>
    {
        [SerializeField]
        Toggle toggle;
        public override void OnNext(bool value)
        {
            Debug.Log("Set tooggle to " + value);
            toggle.isOn = value;
        }
    }
}
