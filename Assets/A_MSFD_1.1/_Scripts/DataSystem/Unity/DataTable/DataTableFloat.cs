using MSFD.Data;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MSFD
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "DataTableFloat_", menuName = "Data/DataTableFloat")]
    public class DataTableFloat : ScriptableObject, IFieldGetter<float>, IDataStreamProvider
    {
        [HideLabel]
        [InlineProperty]
        [SerializeField]
        DataTableBase<float> table = new DataTableBase<float>();

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)table).GetEnumerator();
        }

        public float GetValue()
        {
            return ((IFieldGetter<float>)table).GetValue();
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