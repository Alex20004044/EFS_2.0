/*using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MSFD.Data
{
    public class DataCreatorRuntime : InitBehaviour
    {
        [SerializeField]
        string dataDirectoryPath = "Data/Runtime";
        [Inject]
        public void Init(IRuntimeDataContainer dataContainer)
        {
            CheckInit();
            DataRegistrar.DeserializeAndRegister(dataContainer, GetRawDataTable());
        }

        Dictionary<string, string> GetRawDataTable()
        {
            return FileReader.GetRawDataTable(dataDirectoryPath);
        }

#if UNITY_EDITOR
        [SerializeField]
        DataTableDisplayer dataTableDisplayer = new DataTableDisplayer();
        [Button]
        void DisplayData()
        {
            dataTableDisplayer.DisplayData(DBSerializer.Deserialize(GetRawDataTable()));
        }
#endif
    }
}*/