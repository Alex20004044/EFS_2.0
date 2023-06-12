using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MSFD.Data
{
    public class DataSaverDynamic: InitBehaviour
    {
        [SerializeField]
        string saveDataPath = "Data/Save/Dynamic.txt";

        IDynamicDataContainer dataContainer;

        [Inject]
        public void Init(IDynamicDataContainer dataContainer)
        {
            CheckInit();
            this.dataContainer = dataContainer;
            Load();
        }

        void OnApplicationQuit()
        {
            Save();
        }
        [Button]
        void Load()
        {
            string rawData = FileReader.LoadString(saveDataPath);
            if (rawData == null)
                return;

            DataDriverDB.DeserializeAndSetDataToDriver(dataContainer, rawData);
        }

        [Button]
        void Save()
        {
            string data = DBSerializer.Serialize(dataContainer);
            FileReader.SaveString(saveDataPath, data);
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
            {
                string rawData = SaveCore.Load<string>(saveDataPath);
                if (rawData == null)
                {
                    Debug.Log($"No saved data at path: {saveDataPath}");
                    return;
                }
                dataTableDisplayer.DisplayData(DBSerializer.Deserialize(rawData));
            }
        }
#endif
    }
}
