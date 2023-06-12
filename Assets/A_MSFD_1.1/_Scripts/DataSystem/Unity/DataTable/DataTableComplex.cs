using MSFD.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
namespace MSFD
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "DataTableComplex_", menuName = "Data/DataTableCommon")]
    public class DataTableComplex : ScriptableObject, IDataStreamProvider, IFieldGetter<string>, IFieldGetter<float>, IFieldGetter<int>, IFieldGetter<bool>, IFieldGetter<GameObject>
    {
        [SerializeField]
        DataTableBase<string> tableString;
        [SerializeField]
        DataTableBase<float> tableFloat;
        [SerializeField]
        DataTableBase<int> tableInt;
        [SerializeField]
        DataTableBase<bool> tableBool;
        [SerializeField]
        DataTableBase<GameObject> tableGameObject;

        public bool TryGetData<T>(string path, out T data)
        {
            return tableString.TryGetData(path, out data)
                || tableFloat.TryGetData(path, out data)
                || tableGameObject.TryGetData(path, out data)
                || tableInt.TryGetData(path, out data)
                || tableBool.TryGetData(path, out data);
        }

        public bool TryGetDataStream<T>(string path, out IObservable<T> dataStream)
        {
            if (!TryGetData(path, out T data))
            {
                dataStream = default;
                return false;
            }

            dataStream = Observable.Return(data);
            return true;
        }
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return tableString.Concat(tableFloat).Concat(tableGameObject).Concat(tableInt).Concat(tableBool).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



        public string GetValue()
        {
            return ((IFieldGetter<string>)tableString).GetValue();
        }

        float IFieldGetter<float>.GetValue()
        {
            return ((IFieldGetter<float>)tableFloat).GetValue();
        }

        int IFieldGetter<int>.GetValue()
        {
            return ((IFieldGetter<int>)tableInt).GetValue();
        }
        bool IFieldGetter<bool>.GetValue()
        {
            return ((IFieldGetter<bool>)tableBool).GetValue();
        }

        GameObject IFieldGetter<GameObject>.GetValue()
        {
            return ((IFieldGetter<GameObject>)tableGameObject).GetValue();
        }
    }
}