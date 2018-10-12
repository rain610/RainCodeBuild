using Repository;
using System;

namespace MyFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //构造函数继承
            var test = new Test("lalal");
            //var dalRepository = new CustomerRepository();
            //var list = dalRepository.GetCustomers();

            //索引器
            var indexer = new IndexerExample();
            var aa = indexer[0];
            var indexer6 = new IndexerExampleFor6<string>();
            indexer6.Add("666");
            var bb = indexer6[0];
            var indexer7 = new IndexerExample7<string>();
            indexer7[0] = "Hello world";
            var cc = indexer7[0];



            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
