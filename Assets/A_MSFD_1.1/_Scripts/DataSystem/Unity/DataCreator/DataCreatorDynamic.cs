using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MSFD.Data
{
    public class DataCreatorDynamic : InitBehaviour
    {
        [SerializeField]
        string dataDirectoryPath = "Data/Dynamic";

        IDynamicDataContainer dataContainer;
        [Inject]
        public void Init(IDynamicDataContainer dataContainer)
        {
            CheckInit();
            this.dataContainer = dataContainer;
            DataContainerDB.DeserializeAndRegister(dataContainer, GetRawDataTable());
        }
        Dictionary<string, string> GetRawDataTable()
        {
            return FileReader.GetRawDataTable(dataDirectoryPath);
        }

#if UNITY_EDITOR
        [SerializeField]
        DataTableDisplay dataTableDisplayer = new DataTableDisplay();
        [Button]
        void DisplayData()
        {
            if (dataContainer != null)
                dataTableDisplayer.DisplayData(dataContainer);
            else
                dataTableDisplayer.DisplayData(DBSerializer.Deserialize(GetRawDataTable()));
        }

#endif
    }
}