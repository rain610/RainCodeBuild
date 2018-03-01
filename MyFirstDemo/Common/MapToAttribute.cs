using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MapToAttribute: Attribute
    {
        public LifeTime LifeTime { get; }
        public Type ServiceType { get; }
        public MapToAttribute(Type serviceType, LifeTime lifeTime = LifeTime.Transient)
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
}
