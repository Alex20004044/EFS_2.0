using UniRx;
using UnityEngine;
namespace MSFD.Data
{
    public static class DataFieldExtensions
    {
        public static System.IDisposable Subscribe<T>(this DataField<T> dataField, IDataStreamProvider dataStreamProvider)
        {
            return dataStreamProvider.GetDataStream<T>(dataField.Path).Subscribe(x => dataField.Value = x);
        }        
        public static void SubscribeAndAddTo<T>(this DataField<T> dataField, IDataStreamProvider dataStreamProvider, Component gameObjectComponent)
        {
            dataStreamProvider.GetDataStream<T>(dataField.Path).Subscribe(x => dataField.Value = x).AddTo(gameObjectComponent);
        }
        public static System.IObservable<T> SubscribeAndGetDataStream<T>(this DataField<T> dataField, IDataStreamProvider dataStreamProvider)
        {
            return dataStreamProvider.GetDataStream<T>(dataField.Path).Do(x => dataField.Value = x);
        }

        public static void GetDataFrom<T>(this DataField<T> dataField, IDataProvider dataProvider)
        {
            dataField.Value = dataProvider.GetData<T>(dataField.Path);
        }
    }
}
