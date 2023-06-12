using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace MSFD.Data
{
    public class CompositeDataContainer : IDataStreamProvider, ICompositeDataContainer
    {

        Dictionary<string, CompositeData> dataTable = new Dictionary<string, CompositeData>();
        IDataStreamProvider dataStreamProvider;

        public CompositeDataContainer(IDataStreamProvider dataStreamProvider)
        {
            this.dataStreamProvider = dataStreamProvider;
        }
        public bool TryGetData<T>(string path, out T data)
        {
            if (!dataTable.TryGetValue(path, out CompositeData compositeData))
            {
                data = default;
                return false;
            }
            if (!compositeData.TryGetDataValue(out data))
                DataSystemUtilities.ThrowInvalidCastExeption(path, compositeData.objectProperty.Value.GetType(), typeof(T));
            return true;
        }
        public bool TryGetDataStream<T>(string path, out IObservable<T> dataStream)
        {
            if (!dataTable.TryGetValue(path, out CompositeData compositeData))
            {
                dataStream = null;
                return false;
            }
            if (!compositeData.TryGetDataStream(out IObservable<T> reactiveProperty))
                DataSystemUtilities.ThrowInvalidCastExeption(path, compositeData.objectProperty.Value.GetType(), typeof(T));

            dataStream = reactiveProperty;
            return true;
        }

        public bool TryCreateCompositeData(string path, string baseDataPath, string indexPath)
        {
            if (dataTable.ContainsKey(path))
                return false;

            IObservable<int> indexStream = dataStreamProvider.GetDataStream<int>(indexPath);

            ReactiveProperty<object> reactiveProperty = new ReactiveProperty<object>();
            IDisposable disposable = indexStream.Subscribe(
                (i) => reactiveProperty.Value = dataStreamProvider.GetData<object>(DataSystemUtilities.PathCombine(baseDataPath, i.ToString())));

            CompositeData compositeData = new CompositeData(reactiveProperty, disposable);
            return dataTable.TryAdd(path, compositeData);
        }

        public bool TryChangeCompositeData(string path, string newBaseDataPath, string newIndexPath)
        {
            if (!dataTable.TryGetValue(path, out CompositeData compositeData))
                return false;

            var objectProperty = compositeData.objectProperty;

            IObservable<int> indexStream = dataStreamProvider.GetDataStream<int>(newIndexPath);

            compositeData.disposable.Dispose();
            IDisposable disposable = indexStream.Subscribe((i) => objectProperty.Value = dataStreamProvider.GetData<object>(DataSystemUtilities.PathCombine(newBaseDataPath, i.ToString())));
            compositeData.disposable = disposable;

            dataTable[path] = compositeData;
            return true;
        }
        public bool TryDeleteCompositeData(string path)
        {
            if (dataTable.Remove(path, out CompositeData compositeData))
                return false;
            compositeData.disposable.Dispose();
            compositeData.objectProperty.Dispose();
            return true;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return dataTable.Select((x) => new KeyValuePair<string, object>(x.Key, x.Value.objectProperty)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        struct CompositeData
        {
            public ReactiveProperty<object> objectProperty;
            public IDisposable disposable;

            public CompositeData(ReactiveProperty<object> reactiveProperty, IDisposable disposable)
            {
                this.objectProperty = reactiveProperty;
                this.disposable = disposable;
            }
            public bool TryGetDataValue<T>(out T data)
            {
                if (!(objectProperty.Value is T))
                {
                    data = default;
                    return false;
                }
                data = (T)objectProperty.Value;
                return true;
            }
            public bool TryGetDataStream<T>(out IObservable<T> dataStream)
            {
                if (!(objectProperty.Value is T))
                {
                    dataStream = default;
                    return false;
                }
                dataStream = objectProperty.Cast<object, T>();
                return true;
            }
        }
    }
}
