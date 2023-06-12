using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MSFD.Data
{
    public class DSDisplayer : InitBehaviour
    {

        IDataStreamProvider dataStreamProvider;

        [Inject]
        public void Inject(IDataStreamProvider dataStreamProvider)
        {
            this.dataStreamProvider = dataStreamProvider;
        }
        [Button]
        public object GetData(string path)
        {
            return dataStreamProvider.GetData<object>(path);
        }

#if UNITY_EDITOR
        [SerializeField]
        DataTableDisplay dataTableDisplayer = new DataTableDisplay();
        [Button]
        void DisplayData()
        {
            if (dataStreamProvider == null)
                throw new InvalidOperationException("Can't display data in editor mode. Works only in runtime");

            dataTableDisplayer.DisplayData(dataStreamProvider);
        }
#endif
    }
}