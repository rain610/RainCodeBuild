using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, Assembly assembly)
        {
            var registrations =
                 from type in assembly.GetTypes()
                 let attribtues = type.GetTypeInfo().GetCustomAttributes<MapToAttribute>().ToArray()
                 where attribtues.Length > 0
                 select new Tuple<Type, MapToAttribute[]>(type, attribtues);
            foreach (var item in registrations)
            {
                Type implType = item.Item1;
                if (item.Item1.GetTypeInfo().IsGenericType)
                {
                    implType = item.Item1.GetGenericTypeDefinition();
                }

                foreach (var attribute in item.Item2)
                {
                    switch (attribute.LifeTime)
                    {
                        case LifeTime.Scoped:
                            {
                                services.AddScoped(attribute.ServiceType, implType);
                                break;
                            }
                        case LifeTime.Singleton:
                            {
                                services.AddSingleton(attribute.ServiceType, implType);
                                break;
                            }
                        default:
                            {
                                services.AddTransient(attribute.ServiceType, implType);
                                break;
                            }
                    }
                }
            }
            return services;
        }
    }
}
