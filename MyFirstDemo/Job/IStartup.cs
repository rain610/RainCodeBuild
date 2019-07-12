using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Job
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services);
        void RegisterJobs(JobRegistry registry);
        void Initialize(IServiceProvider serviceProvider);
    }
}
