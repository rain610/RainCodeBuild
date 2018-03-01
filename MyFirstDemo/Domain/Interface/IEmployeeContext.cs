using Common;
using DBModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Context.Interface
{
    public interface IEmployeeContext
    {
        IList<EmployeeModel> GetList(string firstName, string lastName, DataPage dataPage = null);
        int Add(EmployeeModel employeeModel);
        void Delete(int id);
        void Update(EmployeeModel employeeModel);
    }
}
