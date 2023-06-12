using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    public class StaticDataContainer : IStaticDataContainer
    {
        Dictionary<string, object> dataTable = new Dictionary<string, object>();

        public bool TryGetData<T>(string path, out T data)
        {
            if (!dataTable.TryGetValue(path, out object value))
            {
                data = default;
                return false;

            }
            if (!(value is T))
                DataSystemUtilities.ThrowInvalidCastExeption(path, value.GetType(), typeof(T));

            data = (T)value;
            return true;
        }

        public bool TryCreateData<T>(string path, T value)
        {
            return dataTable.TryAdd(path, value);
        }
        public bool TryDeleteData(string path)
        {
            return dataTable.Remove(path);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return dataTable.GetEnumerator();
        }
    }
}
