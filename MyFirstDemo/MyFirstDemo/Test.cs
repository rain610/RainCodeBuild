using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstDemo
{
    public class Test
    {
        public Test()
        {
            Console.WriteLine("无参构造函数");
        }

        public Test(string text) : this()
        {
            Console.WriteLine(text);
            Console.WriteLine("有参构造函数");
        }
    }
}
