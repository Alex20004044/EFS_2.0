using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using static MSFD.Data.DataSystemUtilities;
namespace MSFD.Data
{
    public class DynamicDataContainer : IDynamicDataContainer
    {
        Dictionary<string, object> dataTable = new Dictionary<string, object>();

        public bool TryGetDataStream<T>(string path, out IObservable<T> dataStream)
        {
            if (!IsDataTypeSupported(typeof(T)) || !dataTable.TryGetValue(path, out object source))
            {
                dataStream = null;
                return false;
            }
            var reactiveProperty = source as ReactiveProperty<T>;
            if(reactiveProperty == null)
                ThrowInvalidCastExeption(path, source.GetType().GenericTypeArguments.First(), typeof(T));

            dataStream = reactiveProperty;
            return true;
        }

        public bool TryCreateData<T>(string path, T value)
        {
            return IsDataTypeSupported(typeof(T)) && dataTable.TryAdd(path, new ReactiveProperty<T>(value));
        }

        public bool TryDeleteData(string path)
        {
            return dataTable.Remove(path);
        }

        public bool TrySetData<T>(string path, T value)
        {
            if (!dataTable.TryGetValue(path, out object source)) 
                return false;

            ReactiveProperty<T> data = source as ReactiveProperty<T>;
            if (data == null)
                ThrowInvalidCastExeption(path, source.GetType().GenericTypeArguments.First(), typeof(T));
            data.Value = value;
            return true;
        }

        public bool TryGetData<T>(string path, out T data)
        {
            if (!IsDataTypeSupported(typeof(T)) || !dataTable.TryGetValue(path, out object source))
            {
                data = default;
                return false;
            }
            ReactiveProperty<T> dataStream = source as ReactiveProperty<T>;
            if (dataStream == null)
                ThrowInvalidCastExeption(path, source.GetType().GenericTypeArguments.First(), typeof(T));
            data = ((ReactiveProperty<T>)source).Value;
            return true;
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return dataTable.Select((x) => new KeyValuePair<string, object>(x.Key, x.Value)).GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }

        bool IsDataTypeSupported(Type type)
        {
            return DBSerializer.IsDataTypeCorrect(type);
        }
    }
}
