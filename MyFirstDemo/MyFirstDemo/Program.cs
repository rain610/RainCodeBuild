using Repository;
using System;

namespace MyFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var dalRepository = new CustomerRepository();
            var list = dalRepository.GetCustomers();
            Console.WriteLine("Hello World!");

        }
    }
}
