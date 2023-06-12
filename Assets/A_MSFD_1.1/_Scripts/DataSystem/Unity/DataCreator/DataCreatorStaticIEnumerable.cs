using CorD.SparrowInterfaceField;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MSFD.Data
{
    public class DataCreatorStaticIEnumerable : InitBehaviour
    {
        [SerializeField]
        List<InterfaceField<IEnumerable<KeyValuePair<string, object>>>> dataSources = new List<InterfaceField<IEnumerable<KeyValuePair<string, object>>>>();

        [Inject]
        public void Init(IStaticDataContainer staticDataContainer)
        {
            CheckInit();
            dataSources.SelectMany((x) => x.i).ForEach(x => staticDataContainer.CreateData(x.Key, x.Value));
        }


#if UNITY_EDITOR
        [SerializeField]
        DataTableDisplay dataTableDisplayer = new DataTableDisplay();
        [Button]
        void DisplayData()
        {
            dataTableDisplayer.DisplayData(dataSources.SelectMany((x)=>x.i));
        }
#endif
    }
}