using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Repository
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            return services.AddServices(typeof(ServiceRegister).GetTypeInfo().Assembly);
        }
    }
}
