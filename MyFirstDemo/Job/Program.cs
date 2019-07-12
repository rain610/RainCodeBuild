using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Options;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace Job
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += OnUnhandledException;
            currentDomain.ProcessExit += OnProcessExit;
            ThreadPool.GetMaxThreads(out var workerThreads, out var ioThreads);
            Console.WriteLine("MaxThreads \t workerThreads:{0} \t ioThreads:{1}", workerThreads, ioThreads);
            ThreadPool.GetMinThreads(out workerThreads, out ioThreads);
            Console.WriteLine("MinThreads \t workerThreads:{0} \t ioThreads:{1}", workerThreads, ioThreads);
            ThreadPool.SetMinThreads(1024, ioThreads);
            ThreadPool.GetMinThreads(out workerThreads, out ioThreads);
            Console.WriteLine("MinThreads \t workerThreads:{0} \t ioThreads:{1}", workerThreads, ioThreads);
            ServicePointManager.DefaultConnectionLimit = 512;

            new WebHostBuilder().ConfigureAppConfiguration((context, builder) => 
            {
                var env = context.HostingEnvironment.EnvironmentName;
                builder.AddEnvironmentVariables()
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env}.json", optional: true)
                      ;
            })
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureServices((context,srvs)=> {
                Configuration = context.Configuration;
                new JobServerBuilder()
                    .ConfigureServices(svcs => svcs
                        .AddOptions()
                        )
                    .UseJobFactory<ServicePrviderJobFactory>()
                    .UseStartup<Startup>()
                    .Build(context.Configuration)
                    .StartAsync()
                    .Wait();
            })
            .Configure(_ => { })
            .Build()
            .Run();

            //new JobServerBuilder()
            //    .ConfigureServices(svcs => svcs
            //        .AddOptions()
            //        )
            //    .UseJobFactory<ServicePrviderJobFactory>()
            //    .UseStartup<Startup>()
            //    .Build()
            //    .StartAsync()
            //    .Wait();
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs eventArgs)
        {
            var ex = (Exception)eventArgs.ExceptionObject;
            try
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Program is being shutdown ......");
        }

        public static IConfiguration Configuration { get; private set; }
    }
}
