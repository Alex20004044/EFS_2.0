using MSFD.AS;
using MSFD.Data;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
namespace MSFD
{
    [System.Serializable]
    public class DataTableBase<T> : IFieldGetter<T>, IDataStreamProvider
    {
        [AssetsOnly]
        [TableList(ShowIndexLabels = true)]
        [SerializeField]
        List<DataKeyValue<T>> dataList;


        public T GetValue()
        {
            return dataList.GetRandomElement().value;
        }
        public bool TryGetData<U>(string path, out U data)
        {
            if (typeof(U) != typeof(T))
            {
                data = default;
                return false;
            }

            int index = dataList.FindIndex((x) => x.name == path);
            if (index < 0)
            {
                data = default;
                return false;
            }

            data = (U)(object)dataList[index].value;
            return true;
        }
        public bool TryGetDataStream<U>(string path, out IObservable<U> dataStream)
        {
            if(!TryGetData(path, out U data))
            {
                dataStream = default;
                return false;
            }

            dataStream = Observable.Return(data);
            return true;
        }
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return dataList.Select((x)=> new KeyValuePair<string, object>(x.name, x.value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        [System.Serializable]
        class DataKeyValue<T>
        {
            public string name;
            public T value;

            public DataKeyValue(string name, T value)
            {
                this.name = name;
                this.value = value;
            }
        }
#if UNITY_EDITOR
        [Obsolete(EditorConstants.editorOnly)]
        [Button]
        void SetDefaultNames()
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                DataKeyValue<T> x = dataList[i];
                if (String.IsNullOrWhiteSpace(x.name))
                {
                    dataList[i] = new DataKeyValue<T>(DefineDefaultName(x.value), x.value);
                }
            }
        }
        [Obsolete(EditorConstants.editorOnly)]
        protected virtual string DefineDefaultName(T value)
        {
            return value?.ToString() ?? "";
        }
#endif
    }
}