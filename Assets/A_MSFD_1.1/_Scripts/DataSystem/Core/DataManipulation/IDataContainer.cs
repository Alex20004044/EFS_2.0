using System;

namespace MSFD.Data
{
    public interface IDataContainer
    {
        bool TryCreateData<T>(string path, T value);
        bool TryDeleteData(string path);
    }

    public static class DataContainerExtensions
    {
        public static void CreateData<T>(this IDataContainer dataContainer, string path, T value)
        {
            if (!dataContainer.TryCreateData(path, value))
                throw new ArgumentException($"Data ({value}) with path {path} already exists in {dataContainer} or data container does not support this type");
        }
        /*        public static void SetOrCreateData<T>(this IDataContainer dataContainer, string path, T value)
                {
                    if(dataContainer.TrySetData<T>(path, value) || dataContainer.TryCreateData<T>(path, value))
                        throw new ArgumentException($"Data ({value}) with path {path} can't be set or created {dataContainer}");
                }*/
    }

}
