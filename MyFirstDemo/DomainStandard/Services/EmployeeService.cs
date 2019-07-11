using DomainStandard.Interface;
using DomainStandard.Model;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainStandard.Services
{
    [MapTo(typeof(IEmployeeService), ServiceLifetime.Singleton)]
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<IList<EmployeeModel>> GetList(string firstName, string lastName, DataPage dataPage = null)
        {
            var dataList = _employeeRepository.GetList(firstName, lastName, dataPage);
            return dataList;
        }
    }
}
