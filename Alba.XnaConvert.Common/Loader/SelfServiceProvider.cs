using System;
using Alba.Framework.Sys;
using Alba.Framework.Text;

namespace Alba.XnaConvert.Common
{
    public class SelfServiceProvider : IServiceProvider
    {
        public object GetService (Type serviceType)
        {
            if (GetType().IsAssignableTo(serviceType))
                return this;
            throw new InvalidOperationException("Service '{0}' not implemented.".Fmt(serviceType));
        }
    }
}