using DomainStandard.Interface;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Job.JobServices
{
    public class StudentJob: IJob
    {
        private readonly IStudentAService _studentAService;

        public StudentJob(IStudentAService studentAService)
        {
            _studentAService = studentAService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var tasks = new List<Task>();
            tasks.Add(CyclicTask(context, _studentAService.SyncDataAsync));
            tasks.Add(CyclicTask(context, _studentAService.SyncOneAsync));
            await Task.WhenAll(tasks);
        }

        private Task CyclicTask(IJobExecutionContext context, params Func<Task>[] tasks)
        {
            return Task.Factory.StartNew(async () => {
                while (context.CancellationToken.IsCancellationRequested == false)
                {
                    try
                    {
                        foreach (var task in tasks)
                        {
                            await task();
                            //Console.WriteLine("学生job在执行");
                        }
                        Thread.Sleep(31);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            });
        }
    }
}
