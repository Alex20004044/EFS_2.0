using UnityEngine;
using UnityEngine.UI;

namespace MSFD
{
    public class DeltaRangeToSlider : FieldObserverToBase<IDeltaRange<float>>
    {
        [SerializeField]
        Slider slider;
        public override void OnNext(IDeltaRange<float> value)
        {
            slider.minValue = value.MinBorder;
            slider.maxValue = value.MaxBorder;
            slider.value = value.Value;
        }
    }
}