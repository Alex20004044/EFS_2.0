using Sirenix.OdinInspector;
using System;
using UniRx;
using UnityEngine;
namespace MSFD.Data
{
    [System.Serializable]
    public class DataField<T>
    {
        
        [LabelWidth(50)]
        [HorizontalGroup("DataField", MinWidth = 100, MaxWidth = 200)]
        [SerializeField]
        string path;
        [LabelWidth(50)]
        [HorizontalGroup("DataField")]
        [SerializeField]
        T value;
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        public DataField(T value, string path)
        {
            this.value = value;
            this.path = path;
        }
        public DataField(string path)
        {
            this.path = path;
        }
    }

/*    public static class DataFieldExtension
    {
        public static System.IObservable<T> ConnectToProvider<T>(this DataField<T> dataField, IStreamProvider dataStreamProvider)
        {
            return dataStreamProvider.GetStreamAndSubscibeDataField(dataField);
        }
        public static System.IDisposable SubscribeToProvider<T>(this DataField<T> dataField, IStreamProvider dataStreamProvider)
        {
            return dataStreamProvider.GetStreamAndSubscibeDataField(dataField).Subscribe();
        }        
        public static void SubscribeToProviderAddTo<T>(this DataField<T> dataField, IStreamProvider dataStreamProvider, Component thisTargetComponent)
        {
            dataStreamProvider.GetStreamAndSubscibeDataField(dataField).Subscribe().AddTo(thisTargetComponent);
        }
    }*/
}