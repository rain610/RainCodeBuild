using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job.JobServices
{
    public class TeacherJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var tasks = new List<Task>();
            Console.WriteLine("老师job在执行");
        }

        private Task CyclicTask(IJobExecutionContext context, params Func<Task>[] tasks)
        {
            return Task.Factory.StartNew(async () => {
                //todo
            });
        }
    }
}
