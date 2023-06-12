using System;
using System.Collections.Generic;

namespace MSFD.Data
{
    public interface IDataStreamProvider : IDataProvider
    {
        /// <summary>
        /// Return false if data not found. If type is incorrect => throw Exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="dataStream"></param>
        /// <returns></returns>
        bool TryGetDataStream<T>(string path, out IObservable<T> dataStream);
    }
    public static class DataStreamProviderExtension
    {
        public static IObservable<T> GetDataStream<T>(this IDataStreamProvider dataStreamProvider, string path)
        {
            if (dataStreamProvider.TryGetDataStream(path, out IObservable<T> dataStream))
                return dataStream;
            else
                throw new KeyNotFoundException($"Data with path {path} is not found");
        }
    }
}
