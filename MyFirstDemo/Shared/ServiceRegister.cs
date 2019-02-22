using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Shared
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            return services.AddServices(typeof(ServiceRegister).GetTypeInfo().Assembly);
        }
    }
}
