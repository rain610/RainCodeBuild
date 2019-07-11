using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    /// <summary>
    /// [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    /// [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    /// [AttributeUsage(AttributeTargets.Class)]
    /// [AttributeUsage(AttributeTargets.Method)]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MapToAttribute : Attribute
    {
        //public LifeTime LifeTime { get; }
        public ServiceLifetime LifeTime { get; }
        public Type ServiceType { get; }
        public MapToAttribute(Type serviceType, ServiceLifetime lifeTime = ServiceLifetime.Transient)
        {
            this.ServiceType = serviceType;
            this.LifeTime = lifeTime;
        }
    }

    public enum LifeTime
    {
        Transient,
        Scoped,
        Singleton
    }

    [Flags]
    public enum ServiceScope
    {
        Server = 1,
        Client = 2,
    }
}
