using System;
using System.Collections.Generic;
using System.Globalization;

namespace MSFD.Data
{
    public static class DataContainerDB
    {
        public static void DeserializeAndRegister(IDataContainer dataContainer, string rawDataTable)
        {
            DBSerializer.Deserialize(rawDataTable, GetDeserializeCreateAction(dataContainer));
        }
        public static void DeserializeAndRegister(IDataContainer dataContainer, IEnumerable<KeyValuePair<string, string>> rawDataTable)
        {
            DBSerializer.Deserialize(rawDataTable, GetDeserializeCreateAction(dataContainer));
        }
        static DBSerializer.DeserializeAction GetDeserializeCreateAction(IDataContainer dataContainer)
        {
            return new DBSerializer.DeserializeAction(
                (path, value) => CreateData(dataContainer, path, value),
                (path, value) => CreateData(dataContainer, path, value),
                (path, value) => CreateData(dataContainer, path, value),
                (path, value) => CreateData(dataContainer, path, value),
                (path, value) => CreateData(dataContainer, path, value)
                );
        }
        static void CreateData<T>(IDataContainer dataContainer, string path, T value)
        {
            dataContainer.CreateData(path, value);
        }
    }

}