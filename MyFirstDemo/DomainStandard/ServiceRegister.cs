using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DomainStandard
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services.AddServices(typeof(ServiceRegister).GetTypeInfo().Assembly);
        }
    }
}
