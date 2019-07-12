using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace Job
{
    public class JobServer
    {
        private ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;
        private IServiceProvider _serviceProvider;
        private IDictionary<Type, Func<ITrigger>> _jobs;

        public JobServer(IServiceProvider serviceProvider, IDictionary<Type, Func<ITrigger>> jobs, NameValueCollection settings)
        {
            _serviceProvider = serviceProvider;
            _jobs = jobs;
            _schedulerFactory = new StdSchedulerFactory(settings);
        }

        public async Task StartAsync()
        {
            _scheduler = await _schedulerFactory.GetScheduler();
            var jobFactory = _serviceProvider.GetService<IJobFactory>();
            if (null != jobFactory)
            {
                _scheduler.JobFactory = jobFactory;
            }

            await _scheduler.Start();

            foreach (var item in _jobs)
            {
                var job = JobBuilder.Create(item.Key)
                    .WithIdentity(item.Key.FullName)
                    .Build();
                await _scheduler.ScheduleJob(job, item.Value());
            }
        }
        public async Task ShutdownAsync()
        {
            await _scheduler.Shutdown();
        }
    }
}
