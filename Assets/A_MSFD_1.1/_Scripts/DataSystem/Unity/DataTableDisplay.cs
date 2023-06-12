using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    [System.Serializable]
    public class DataTableDisplay
    {
#if UNITY_EDITOR
        [SerializeField]
        DisplayDataMode displayDataStoreMode = DisplayDataMode.inspector;
        [SerializeField]
        bool isDisplayDataTypes;
        [ShowIf("@" + nameof(_IsShowDisplayString) + "()")]
        [SerializeField]
        [PropertySpace]
        [TextArea(5, 30)]
        string displayDataWindow = "";

        public void DisplayData(IEnumerable<KeyValuePair<string, object>> dataList)
        {
            if (dataList == null)
                throw new InvalidOperationException("Can't display data, because dataList is null. Probably this display works only in runtime");

            string outputDataView = FormOutputView(dataList);
            if (IsDisplayInDebugLog())
            {
                dataList.ForEach(x => Debug.Log((FormDataString(x))));
                //Debug.Log(outputDataView);
            }
            if (IsDisplayInDataWindow())
            {
                displayDataWindow = outputDataView;
            }
        }

        bool IsDisplayInDataWindow()
        {
            return (displayDataStoreMode & DisplayDataMode.inspector) > 0;
        }

        bool IsDisplayInDebugLog()
        {
            return (displayDataStoreMode & DisplayDataMode.debugLog) > 0;
        }
        string FormOutputView(IEnumerable<KeyValuePair<string, object>> dataList)
        {
            string output = String.Empty;
            foreach (var x in dataList)
            {
                output += FormDataString(x) + "\n";
            }
            return output;
        }
        string FormDataString(KeyValuePair<string, object> kv)
        {
            string output;

            if (kv.Value.GetType().IsArray)
                output = kv.Key + ": " + String.Join(CSVParseConstants.arraySeparator, (string[])kv.Value);
            else
                output = kv.Key + ": " + kv.Value;

            if (isDisplayDataTypes)
                output += " (" + kv.Value.GetType() + ")";
            return output;
        }
        public bool _IsShowDisplayString()
        {
            return (displayDataStoreMode & DisplayDataMode.inspector) > 0;
        }
        [System.Flags]
        public enum DisplayDataMode
        {
            debugLog = 1 << 1,
            inspector = 1 << 2
        };
#endif
    }
}