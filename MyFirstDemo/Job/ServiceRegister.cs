using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Job
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddJobServices(this IServiceCollection services)
        {
            return services.AddServices(typeof(ServiceRegister).GetTypeInfo().Assembly);
        }
    }
}
