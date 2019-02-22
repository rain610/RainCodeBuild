using Shared;
using Context.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Context
{
    [MapTo(typeof(IReflectionDemo))]
    public class ReflectionDemo: IReflectionDemo
    {
        public string Sex = "男";
        private string ClassName = "四年级";
        private static string Name = "反射测试";
        private string Name1 { get; set; }
        public ReflectionDemo()
        {

        }

        [SOA(typeof(IReflectionDemo),Shared.ServiceScope.Client)]
        public ReflectionDemo(string name)
        {
            Name1 = name;
        }

        [SOA(typeof(IReflectionDemo), Shared.ServiceScope.Server)]
        public string Test1()
        {
            return "Test1";
        }

        public void Test2()
        {
            Name1 = "Test2";
        }

        public string EnglishName { get; set; }
    }
}
