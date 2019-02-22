using Context;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace MyFirstDemo
{
    public class ReflectionExample
    {
        public void Ref1()
        {
            var reflectionDemo = new ReflectionDemo();
            var type = reflectionDemo.GetType();
            var attributes = type.GetTypeInfo().GetCustomAttribute<SOAAttribute>();
            var type1 = typeof(ReflectionDemo);
            var typeInfo = type1.GetTypeInfo();
            var assembly = typeInfo.Assembly;
            var lala = typeInfo.GetProperties();
            var methodInfo1 = typeInfo.GetMethods();
            var memeberInfo1 = typeInfo.GetMembers();
            var types = assembly.GetTypes();
            foreach (var childType in types)
            {
                var atrribute = childType.GetTypeInfo().GetCustomAttribute<SOAAttribute>();
            }
            //用于取得该类的属性的信息
            PropertyInfo[] propertyInfos = type.GetProperties();
            var entityValue = new object[propertyInfos.Length];
            for (var i = 0; i < propertyInfos.Length; i++)
            {
                entityValue[i] = propertyInfos[i].GetValue(reflectionDemo);
            }

            //用于取得该类的方法的信息
            MethodInfo[] methodInfos = type.GetMethods();
            //用于取得该类的字段（成员变量）的信息
            FieldInfo[] fields = type.GetFields();
            //用于取得该类的所有成员的信息
            MemberInfo[] memberInfos = type.GetMembers();
            MemberInfo[] memberInfo = type.GetMember("Test1");
            //用于取得该类的事件的信息
            EventInfo[] eventInfos = type.GetEvents();
            //用于取得该类的构造函数的信息
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            //用于取得该类实现的接口的信息
            var aa = type.GetInterfaces();
            foreach (var property in propertyInfos)
            {
                var p1 = property.GetMethod;
            }

            foreach (var constructor in constructorInfos)
            {
                var c1 = constructor.GetCustomAttribute(typeof(SOAAttribute));
                if (c1 != null)
                {
                    var c2 = true;
                }
            }
        }
    }
}
