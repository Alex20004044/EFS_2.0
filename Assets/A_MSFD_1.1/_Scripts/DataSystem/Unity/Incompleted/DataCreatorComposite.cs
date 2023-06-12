using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    public class DataCreatorComposite : InitBehaviour
    {
        [SerializeField]
        string dataDirectoryPath = "Data/Composite";

        ICompositeDataContainer dataContainer;
        [Inject]
        public void Init(ICompositeDataContainer dataContainer)
        {
            CheckInit();
            this.dataContainer = dataContainer;

            Register();
        }
        Dictionary<string, string> GetRawDataTable()
        {
            return FileReader.GetRawDataTable(dataDirectoryPath);
        }

        void Register()
        {
            Dictionary<string, string> dataTable = GetRawDataTable();
            foreach (var x in dataTable)
            {
                dataContainer.TryCreateCompositeData(x.Key.Replace("Path/", ""), dataTable[x.Key + "/" + "Left"], dataTable[x.Key + "/" + "Right"]);
            }
        }

#if UNITY_EDITOR
        [SerializeField]
        DataTableDisplay dataTableDisplay = new DataTableDisplay();
        [Button]
        void DisplayData()
        {
/*            if (dataContainer != null)
                dataTableDisplay.DisplayData(dataContainer);
            else*/
                dataTableDisplay.DisplayData(DBSerializer.Deserialize(GetRawDataTable()));
        }

#endif
    }
}
