using UnityEngine;
using UnityEngine.UI;

namespace MSFD.UI
{
    public class FloatObserverToSlider : FieldObserverToBase<float>
    {
        [SerializeField]
        Slider slider;

        public override void OnNext(float value)
        {
            slider.value = value;
        }
    }
}
