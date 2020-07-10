using Shared;
using Context.StagePattern;
using DBModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Context.DelegateDemo;
using Newtonsoft.Json.Linq;

namespace MyFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int t1 = 2 | 4;
            
            var falg1 = (6 & 8)>0;
            var falg2 = (6 & 1) > 0;
            Console.WriteLine(falg1);
            Console.WriteLine(falg2);
            Console.WriteLine(new EnumerableExample().ContainTest());
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

            //委托
            Leader leader = new Leader();
            TeamA teamA = new TeamA(leader);
            TeamB teamB = new TeamB(leader);
            leader.Fall();

            var listArr = new List<int> { 0, 1, 3, 4, 12 };
            var testAr = 2;
            var aa11 = listArr.Any(p => p == testAr);
            var aa22 = listArr.Where(p => p == testAr);
            if (listArr.Contains(testAr))
            {
                Console.WriteLine("存在");
            }
            else
            {
                Console.WriteLine("不存在");
            }


            SortedSet<int> sortSet = new SortedSet<int>();
            var sortSetArr = new List<int> { 1, 23, 4, 9, 5 };
            foreach (var item in sortSetArr)
            {
                sortSet.Add(item);
            }
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
