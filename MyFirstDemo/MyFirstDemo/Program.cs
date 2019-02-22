using Shared;
using Context.StagePattern;
using DBModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

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

            //反射
            new ReflectionExample().Ref1();

            //继承自己的无参构造函数
            var constructDemo = new ConstructExample("我是来测试的");
            Console.WriteLine(constructDemo.a);
            Console.WriteLine(constructDemo.b);

            var list = new List<EmployeeModel>();
            list.Add(new EmployeeModel { EmployeeID = 10001,FirstName="John",LastName="lal" });
            list.Add(new EmployeeModel { EmployeeID = 10002, FirstName = "James", LastName = "lal" });
            var table = ListToTableHelper.ListToDataTable(list);
            var filterList = list.Where(p => p.EmployeeID > 10001).ToList();

            //状态机
            try
            {
                new DoorPlus(State.Open).Process(OperationType.Push);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
