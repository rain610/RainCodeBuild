using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Job
{
    public class ServicePrviderJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServicePrviderJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)ActivatorUtilities.CreateInstance(_serviceProvider, bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
