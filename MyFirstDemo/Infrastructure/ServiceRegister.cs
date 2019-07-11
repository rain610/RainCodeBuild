using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services.AddServices(typeof(ServiceRegister).GetTypeInfo().Assembly);
        }
    }
}
