using UnityEngine;
using UnityEngine.UI;

namespace MSFD.UI
{
    public class FloatObserverToImage : FieldObserverToBase<float>
    {
        [SerializeField]
        Image image;

        public override void OnNext(float value)
        {
            image.fillAmount = value;
        }
    }
}
