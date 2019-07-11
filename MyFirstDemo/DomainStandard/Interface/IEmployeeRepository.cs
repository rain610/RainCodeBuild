using DomainStandard.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainStandard.Interface
{
    public interface IEmployeeRepository
    {
        Task<IList<EmployeeModel>> GetList(string firstName, string lastName, DataPage dataPage = null);
    }
}
