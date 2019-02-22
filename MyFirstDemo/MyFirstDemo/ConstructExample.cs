using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstDemo
{
    public class ConstructExample
    {
        public string a = "a未初始化";
        public string b = "b未初始化";
        public ConstructExample()
        {
            this.a = "a";
        }

        public ConstructExample(string bb):this()
        {
            this.b = bb;
        }
    }
}
