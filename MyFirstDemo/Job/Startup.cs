using AutoMapper;
using DomainStandard;
using Job.JobServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Job
{
    public class Startup: IStartup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddDomainServices()
               .AddJobServices();
        }

        public void Initialize(IServiceProvider serviceProvider)
        {
            var builders = serviceProvider.GetServices<IMapBuilder>().ToArray();
            Mapper.Initialize(cfg => Array.ForEach(builders, builder => builder.BuildMap(cfg, serviceProvider)));
        }

        public void RegisterJobs(JobRegistry registry)
        {
            var timeZone = TimeZoneInfo.CreateCustomTimeZone(
                "China Standard Time",
                 TimeSpan.FromHours(8),
                 "(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi",
                 "China Standard Time");

            registry
                 .AddJob<StudentJob>(builder => builder
                     .StartNow()
                     .WithSimpleSchedule(config => config.WithIntervalInMinutes(2).RepeatForever()))
                 .AddJob<TeacherJob>(builder=>builder
                 .StartNow()
                 .WithSimpleSchedule(config => config.WithMisfireHandlingInstructionFireNow()))
                 //.AddJob<TeacherJob>(builder => builder
                 //    .WithCronSchedule("0 0 3 * * ?", b => b
                 //       .InTimeZone(timeZone)
                 //       .WithMisfireHandlingInstructionFireAndProceed())
                 //      .StartNow())
                ;
        }
    }
}
