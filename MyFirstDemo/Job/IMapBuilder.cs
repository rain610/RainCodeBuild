using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Job
{
    public interface IMapBuilder
    {
        void BuildMap(IMapperConfigurationExpression cfg, IServiceProvider serviceProvider);
    }
}
