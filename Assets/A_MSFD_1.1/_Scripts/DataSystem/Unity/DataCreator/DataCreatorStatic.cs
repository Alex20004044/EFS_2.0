using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MSFD.Data
{
    public class DataCreatorStatic : InitBehaviour
    {
        [SerializeField]
        string dataDirectoryPath = "Data/Static";
        [Inject]
        public void Init(IStaticDataContainer dataContainer)
        {
            CheckInit();
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
            dataTableDisplayer.DisplayData(DBSerializer.Deserialize(GetRawDataTable()));
        }
#endif
    }
}