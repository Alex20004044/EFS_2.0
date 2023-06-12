using MSFD;
using MSFD.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;
using MSFD.AS;

namespace HS
{
    public class SliderDriver : MonoBehaviour
    {
        [SerializeField]
        string valuePath;
        [SerializeField]
        bool isGetBorderFromDataProvider = false;
        [ShowIf(nameof(isGetBorderFromDataProvider))]
        [SerializeField]
        string minValuePath;
        [ShowIf(nameof(isGetBorderFromDataProvider))]
        [SerializeField]
        string maxValuePath;

        [SerializeField]
        Slider slider;
        [SerializeField]
        bool isMapOutput = false;
        [ShowIf(nameof(isMapOutput))]
        [SerializeField]
        Vector2 ouputRange = new Vector2(0, 1);
        [Inject]
        void Init(IDataStreamProvider dataStreamProvider, IDataDriver dataDriver)
        { 
            if (isGetBorderFromDataProvider)
            {
                slider.minValue = dataStreamProvider.GetData<float>(minValuePath);
                slider.maxValue = dataStreamProvider.GetData<float>(maxValuePath);
            }
            dataStreamProvider.GetDataStream<float>(valuePath).Subscribe(x => slider.value = MapInputValue(x)).AddTo(this);

            slider.onValueChanged.AsObservable().Subscribe((x) => dataDriver.SetData(valuePath, MapOutputValue(x))).AddTo(this);
        }
        float MapInputValue(float value)
        {
            if (!isMapOutput)
                return value;
            return Calculation.Map(value, ouputRange, slider.GetRange());
        }
        float MapOutputValue(float value)
        {
            if (!isMapOutput)
                return value;
            return Calculation.Map(value, slider.GetRange(), ouputRange);
        }
    }
}
