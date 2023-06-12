using MSFD;
using MSFD.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
namespace HS
{
    public class ToggleDriver : MonoBehaviour
    {
        [SerializeField]
        string path;
        [SerializeField]
        Toggle toggle;

        IDataDriver dataDriver;
        IDataStreamProvider dataStreamProvider;
        [Inject]
        void Init(IDataStreamProvider dataStreamProvider, IDataDriver dataDriver)
        {
            this.dataDriver = dataDriver;
            this.dataStreamProvider = dataStreamProvider;
        }

        private void Awake()
        {
            toggle.onValueChanged.AddListener(SetData);

            dataStreamProvider.GetDataStream<bool>(path).Subscribe((x) => toggle.isOn = x).AddTo(this);
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveListener(SetData);
        }
        void SetData(bool value)
        {
            dataDriver.SetData(path, value);
        }
    }
}
