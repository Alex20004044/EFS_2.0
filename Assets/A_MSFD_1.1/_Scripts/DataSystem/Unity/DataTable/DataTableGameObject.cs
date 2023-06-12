using MSFD.Data;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MSFD
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "DataTableGameObject_", menuName = "Data/DataTableGameObject")]
    public class DataTableGameObject : ScriptableObject, IFieldGetter<GameObject>, IDataStreamProvider
    {
        [HideLabel]
        [InlineProperty]
        [SerializeField]
        DataTableBase<GameObject> table = new DataTableBase<GameObject>();

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)table).GetEnumerator();
        }

        public GameObject GetValue()
        {
            return ((IFieldGetter<GameObject>)table).GetValue();
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