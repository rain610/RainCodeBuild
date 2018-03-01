using Common;
using Context.Interface;
using DBModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Context
{
    [MapTo(typeof(IEmployeeContext))]
    public class EmployeeContext: IEmployeeContext
    {
        public IList<EmployeeModel> GetList(string firstName, string lastName, DataPage dataPage = null)
        {
            var list = new EmployeeRepository().GetEmployees();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                list = list.Where(p => p.FirstName.Contains(firstName)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                list = list.Where(p => p.LastName.Contains(lastName)).ToList();
            }
            if (dataPage != null)
            {
                dataPage.RowCount = list.Count();
                list = list.Skip((dataPage.PageIndex - 1) * dataPage.PageSize).Take(dataPage.PageSize).ToList();
            }
            return list;
        }

        public int Add(EmployeeModel employeeModel)
        {
            return new EmployeeRepository().Add(employeeModel);
        }

        public void Delete(int id)
        {
            new EmployeeRepository().Delete(id);
        }

        public void Update(EmployeeModel employeeModel)
        {
            new EmployeeRepository().Update(employeeModel);
        }
    }
}
