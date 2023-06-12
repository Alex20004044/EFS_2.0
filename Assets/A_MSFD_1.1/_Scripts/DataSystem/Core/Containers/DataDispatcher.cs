using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MSFD.Data
{
    public class DataDispatcher : IProviderDispatcher
    {
        HashSet<IDataProvider> dataProviders = new HashSet<IDataProvider>();
        HashSet<IDataStreamProvider> dataStreamProviders = new HashSet<IDataStreamProvider>();

        public void RegisterDataProvider(IDataProvider dataProvider)
        {
            if (!dataProviders.Add(dataProvider))
                throw new ArgumentException($"Data provider {dataProvider} already registered");
        }
        public void UnregisterDataProvider(IDataProvider dataProvider)
        {
            if (!dataProviders.Remove(dataProvider))
                throw new ArgumentException($"Data provider {dataProvider} is not registered");
        }
        public void RegisterDataProvider(IDataStreamProvider dataStreamProvider)
        {
            if (!dataStreamProviders.Add(dataStreamProvider))
                throw new ArgumentException($"Data stream provider {dataStreamProvider} already registered");
        }
        public void UnregisterDataProvider(IDataStreamProvider dataStreamProvider)
        {
            if (!dataStreamProviders.Remove(dataStreamProvider))
                throw new ArgumentException($"Data stream provider {dataStreamProvider} is not registered");
        }

        public bool TryGetData<T>(string path, out T data)
        {
            foreach (var x in dataProviders.Concat(dataStreamProviders))
            {
                if (x.TryGetData(path, out data))
                    return true;
            }
            data = default;
            return false;
        }
        public bool TryGetDataStream<T>(string path, out IObservable<T> dataStream)
        {
            foreach (var x in dataStreamProviders)
            {
                if (x.TryGetDataStream(path, out dataStream))
                    return true;
            }
            dataStream = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            IEnumerable<KeyValuePair<string, object>> enumerable = Enumerable.Empty<KeyValuePair<string, object>>();
            foreach (var x in dataProviders.Concat(dataStreamProviders))
            {
                enumerable = enumerable.Concat(x);
            }
            return enumerable.GetEnumerator();
        }
    }
}
