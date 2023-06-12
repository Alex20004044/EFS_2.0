using MSFD.Data;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MSFD
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "DataTableString_", menuName = "Data/DataTableString")]
    public class DataTableString : ScriptableObject, IFieldGetter<string>, IDataStreamProvider
    {
        [HideLabel]
        [InlineProperty]
        [SerializeField]
        DataTableBase<string> table;

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)table).GetEnumerator();
        }

        public string GetValue()
        {
            return ((IFieldGetter<string>)table).GetValue();
        }

        public bool TryGetData<T>(string path, out T data)
        {
            return ((IDataProvider)table).TryGetData(path, out data);
        }

        public bool TryGetDataStream<T>(string path, out IObservable<T> dataStream)
        {
            return ((IDataStreamProvider)table).TryGetDataStream(path, out dataStream);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)table).GetEnumerator();
        }
    }
}