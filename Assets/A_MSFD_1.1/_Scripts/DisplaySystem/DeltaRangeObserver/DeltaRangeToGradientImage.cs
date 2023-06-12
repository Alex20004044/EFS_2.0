using UnityEngine;
using UnityEngine.UI;
namespace MSFD
{
    public class DeltaRangeToGradientImage : FieldObserverToBase<IDeltaRange<float>>
    {
        [SerializeField]
        Gradient gradient;
        [SerializeField]
        Image image;
        [SerializeField]
        bool isFillImage = true;

        public override void OnNext(IDeltaRange<float> value)
        {
            image.color = gradient.Evaluate(value.GetFillPercent());
            if(isFillImage)
                image.fillAmount = value.GetFillPercent();
        }
    }
}