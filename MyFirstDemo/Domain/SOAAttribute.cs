using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Context
{
    public class SOAAttribute: Attribute
    {
        public Type ServiceType { get; }
        public ServiceScope Scope { get; }
        public SOAAttribute(Type serviceType, ServiceScope scope = ServiceScope.Server)
        {
            this.ServiceType = serviceType;
            this.Scope = scope;
        }
    }
}
