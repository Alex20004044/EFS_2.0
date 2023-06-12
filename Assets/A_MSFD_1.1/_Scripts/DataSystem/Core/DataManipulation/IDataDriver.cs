using System;

namespace MSFD.Data
{
    public interface IDataDriver
    {
        bool TrySetData<T>(string path, T value);
    }
    public static class DataDriverExtensions
    { 
        public static void SetData<T>(this IDataDriver dataDriver, string path, T value)
        {
            if(!dataDriver.TrySetData(path, value))
                throw new ArgumentException($"Data {value} with type {typeof(T)} with path {path} can't be set to {dataDriver}");
        }
    }

}
