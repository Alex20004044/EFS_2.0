using System;

namespace MSFD.DI
{
    public class InstallerServiceLocator : InstallerBase
    {
        protected override object GetInjectArg(Type parameterType)
        {
            return ServiceLocator.Get(parameterType);
        }
    }
}
