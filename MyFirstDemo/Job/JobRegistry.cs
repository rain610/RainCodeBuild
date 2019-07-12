using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace Job
{
    public class JobRegistry
    {
        public IDictionary<Type, Func<ITrigger>> Jobs { get; }

        public JobRegistry()
        {
            this.Jobs = new Dictionary<Type, Func<ITrigger>>();
        }

        public JobRegistry AddJob<TJob>(Action<TriggerBuilder> configureTrigger) where TJob : IJob
        {

            this.Jobs.Add(typeof(TJob), () => {
                var tiggerBuilder = TriggerBuilder.Create();
                configureTrigger(tiggerBuilder);
                return tiggerBuilder.Build();
            });
            return this;
        }
    }
}
