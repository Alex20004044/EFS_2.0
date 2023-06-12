using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MSFD.UI
{
    public class MessengerBroadcastSlider : MessengerBroadcastBase
    {
        [SerializeField]
        Slider slider;
        [SerializeField]
        ActivationType activationType = ActivationType.onValueChanged;
        float value;
        private void Awake()
        {
            if(slider == null)
            {
                slider = GetComponent<Slider>();
            }
            slider.onValueChanged.AddListener(OnValueChanged);
        }

        void OnValueChanged(float _value)
        {
            value = _value;
            if(activationType == ActivationType.onValueChanged)
            {
                SendEvent();
            }
        }
        protected override void Broadcast()
        {
            Messenger<float>.Broadcast(eventName, value, messengerMode);
        }

        enum ActivationType { onValueChanged, manual};
    }
}
