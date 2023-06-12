namespace MSFD.Data
{
    public interface IProviderDispatcher: IDataStreamProvider
    {
        void RegisterDataProvider(IDataProvider dataProvider);
        void UnregisterDataProvider(IDataProvider dataProvider);

        void RegisterDataProvider(IDataStreamProvider dataStreamProvider);
        void UnregisterDataProvider(IDataStreamProvider dataStreamProvider);
    }
}
