using System;
using System.Collections.Generic;

namespace MSFD.Data
{
    public interface IDataProvider : IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// Return false if data not found. If type is incorrect => throw Exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        bool TryGetData<T>(string path, out T data);
    }
    public static class DataProviderExtension
    {
        public static T GetData<T>(this IDataProvider dataProvider, string path)
        {
            if (dataProvider.TryGetData(path, out T data))
                return data;
            else
                throw new KeyNotFoundException($"Data from {path} is not found");
        }
    }
}
