using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    public static class DataDriverDB
    {
        static DBSerializer.DeserializeAction GetDeserializeCreateAction(IDataDriver dataDriver)
        {
            return new DBSerializer.DeserializeAction(
                (path, value) => dataDriver.TrySetData(path, value),
                (path, value) => dataDriver.TrySetData(path, value),
                (path, value) => dataDriver.TrySetData(path, value),
                (path, value) => dataDriver.TrySetData(path, value),
                (path, value) => dataDriver.TrySetData(path, value)
                );
        }

        public static void DeserializeAndSetDataToDriver(IDataDriver dataDriver, string rawDataTable)
        {
            DBSerializer.Deserialize(rawDataTable, GetDeserializeCreateAction(dataDriver));
        }
    }
}