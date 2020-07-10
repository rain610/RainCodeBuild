using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstDemo
{
    public class EnumerableExample
    {
        public static List<string> Test = new List<string> { "1", "12", "5", "15" };
        public int ContainTest() 
        {
            var list = Test.Where(x => x.Contains("1")).ToList();
            return list.Count;
        }
    }
}
