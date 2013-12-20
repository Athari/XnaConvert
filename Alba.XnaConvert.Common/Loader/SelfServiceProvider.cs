using System;
using Alba.Framework.Reflection;
using Alba.Framework.Text;

namespace Alba.XnaConvert.Common
{
    public class SelfServiceProvider : IServiceProvider
    {
        public object GetService (Type serviceType)
        {
            if (GetType().Is(serviceType))
                return this;
            throw new InvalidOperationException("Service '{0}' not implemented.".Fmt(serviceType));
        }
    }
}