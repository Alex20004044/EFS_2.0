using MSFD.DI;
using Sirenix.Utilities;
using UnityEngine;

namespace MSFD.Data
{
    public class DSInit : MonoBehaviour
    {
        [SerializeField]
        bool isInvokeInjectDependenciesInChildren = true;

        void Awake()
        {
            var dataDispatcher = new DataDispatcher();
            StaticDataContainer staticDataContainer = new StaticDataContainer();
            dataDispatcher.RegisterDataProvider(staticDataContainer);

            var dynamicDataContainer = new DynamicDataContainer();
            dataDispatcher.RegisterDataProvider(dynamicDataContainer);

            var compositeDataContainer = new CompositeDataContainer(dataDispatcher);
            dataDispatcher.RegisterDataProvider(compositeDataContainer);

            ServiceLocator.Register<IProviderDispatcher>(dataDispatcher);
            ServiceLocator.Register<IDataStreamProvider>(dataDispatcher);
            ServiceLocator.Register<IDataProvider>(dataDispatcher);

            ServiceLocator.Register<IDataDriver>(dynamicDataContainer);


            ServiceLocator.Register<IStaticDataContainer>(staticDataContainer);
            ServiceLocator.Register<IDynamicDataContainer>(dynamicDataContainer);
            ServiceLocator.Register<ICompositeDataContainer>(compositeDataContainer);


            if (isInvokeInjectDependenciesInChildren)
                GetComponentsInChildren<InstallerBase>().ForEach(x => x.Activate());
        }
    }
}
