using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Job
{
    public class JobServerBuilder
    {
        private List<Action<IServiceCollection>> _addServicesActions;
        private Type _startupType;
        private NameValueCollection _settings;
        private List<Type> _jobTypes;

        public JobServerBuilder()
        {
            _addServicesActions = new List<Action<IServiceCollection>>();
            _settings = new NameValueCollection();
            _jobTypes = new List<Type>();
        }

        public JobServerBuilder ConfigureServices(Action<IServiceCollection> addServices)
        {
            _addServicesActions.Add(addServices);
            return this;
        }

        public JobServerBuilder UseStartup<T>() where T : IStartup
        {
            _startupType = typeof(T);
            return this;
        }

        public JobServerBuilder UseJobFactory<TJobFactory>()
            where TJobFactory : class, IJobFactory
        {
            _addServicesActions.Add(services => services.AddSingleton<IJobFactory, TJobFactory>());
            return this;
        }

        public JobServerBuilder AddSettings(string name, string value)
        {
            _settings.Add(name, value);
            return this;
        }

        public JobServer Build(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            foreach (var addServices in _addServicesActions)
            {
                addServices(services);
            }
            var serviceProvider = services.BuildServiceProvider();
            var startup = (IStartup)ActivatorUtilities.CreateInstance(serviceProvider, _startupType, configuration);
            startup.ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
            var jobRegistry = new JobRegistry();
            startup.RegisterJobs(jobRegistry);
            startup.Initialize(serviceProvider);
            return new JobServer(serviceProvider, jobRegistry.Jobs, _settings);
        }
    }
}
