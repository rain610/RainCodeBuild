using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Shared
{
    [MapTo(typeof(IHashPayloadProvider))]
    public class HashPayloadProvider: IHashPayloadProvider
    {
        private readonly static IDictionary<Type, Func<object, byte[]>> _payloadAccessors;
        private static object _syncHelper;

        static HashPayloadProvider()
        {
            _payloadAccessors = new Dictionary<Type, Func<object, byte[]>>();
            _syncHelper = new object();
        }

        public byte[] GetPayload(object value)
        {
            //Null
            if (null == value)
            {
                return new byte[0];
            }

            //Scalar type
            if (typeof(ValueType).IsAssignableFrom(value.GetType()) || value is string)
            {
                return Encoding.UTF8.GetBytes(value.ToString());
            }

            //Collection
            IEnumerable collection = value as IEnumerable;
            if (null != collection)
            {
                List<byte> bytes = new List<byte>();

                //Dictionary
                if (this.IsDictionary(value))
                {
                    var dictionary = new Dictionary<object, object>();
                    foreach (var pair in collection)
                    {
                        var keyOfItem = FastAccessor.GetPropertyValue(pair, "Key");
                        var valueOfItem = FastAccessor.GetPropertyValue(pair, "Value");
                        dictionary.Add(keyOfItem, valueOfItem);
                    }
                    foreach (var key in dictionary.Keys.OrderBy(it => it))
                    {
                        bytes.AddRange(Encoding.UTF8.GetBytes("Key"));
                        bytes.AddRange(this.GetPayload(key));
                        bytes.AddRange(Encoding.UTF8.GetBytes("Value"));
                        bytes.AddRange(this.GetPayload(dictionary[key]));
                    }
                    return bytes.ToArray();
                }
                foreach (var item in collection)
                {
                    bytes.AddRange(this.GetPayload(item));
                }
                return bytes.ToArray();
            }

            //Normal type
            Func<object, byte[]> accessor;
            if (_payloadAccessors.TryGetValue(value.GetType(), out accessor))
            {
                return accessor(value);
            }

            lock (_syncHelper)
            {
                if (_payloadAccessors.TryGetValue(value.GetType(), out accessor))
                {
                    return accessor(value);
                }
                _payloadAccessors[value.GetType()] = accessor = this.CreatePayloadAccessor(value.GetType());
            }
            return accessor(value);
        }

        protected virtual Func<object, byte[]> CreatePayloadAccessor(Type targetType)
        {
            //Guard.ArgumentNotNull(nameof(targetType), targetType);
            var properties = targetType.GetProperties()
                .Where(it => it.GetCustomAttributes<HashMemberAttribute>().Any())
                .OrderBy(it => it.Name)
                .ToArray();

            if (!properties.Any())
            {
                return _ => new byte[0];
            }

            return target =>
            {
                List<byte> bytes = new List<byte>();
                foreach (var property in properties)
                {
                    bytes.AddRange(Encoding.UTF8.GetBytes(property.Name));
                    var propertyValue = FastAccessor.GetPropertyValue(target, property.Name);
                    bytes.AddRange(this.GetPayload(propertyValue));
                }
                return bytes.ToArray();
            };
        }

        private bool IsDictionary(object value)
        {
            if (!value.GetType().IsGenericType)
            {
                return false;
            }
            var genericDefinition = value.GetType().GetGenericTypeDefinition();
            return null != genericDefinition &&
                genericDefinition.GetTypeInfo().ImplementedInterfaces.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == typeof(IDictionary<,>));
        }

        private static class FastAccessor
        {
            private static Dictionary<string, Func<object, object>> _propertyValueAccessors;
            private static object _lockHelper;

            static FastAccessor()
            {
                _propertyValueAccessors = new Dictionary<string, Func<object, object>>();
                _lockHelper = new object();
            }

            public static object GetPropertyValue(object target, string propertyName)
            {
                var targetType = target.GetType();
                var key = BuildKey(targetType, propertyName);
                Func<object, object> accessor;
                if (_propertyValueAccessors.TryGetValue(key, out accessor))
                {
                    return accessor(target);
                }
                lock (_lockHelper)
                {
                    if (_propertyValueAccessors.TryGetValue(BuildKey(targetType, propertyName), out accessor))
                    {
                        return accessor(target);
                    }
                    var targetExp = Expression.Parameter(typeof(object));
                    var castToTargetExp = Expression.Convert(targetExp, targetType);
                    var getPropertyValue = Expression.Call(castToTargetExp, targetType.GetProperty(propertyName).GetMethod);
                    var castToObject = Expression.Convert(getPropertyValue, typeof(object));
                    _propertyValueAccessors[key] = accessor = Expression.Lambda<Func<object, object>>(castToObject, targetExp).Compile();
                }
                return accessor(target);
            }

            private static string BuildKey(Type type, string propertyName)
            {
                return $"{type.AssemblyQualifiedName}=>{propertyName}";
            }
        }
    }
}
