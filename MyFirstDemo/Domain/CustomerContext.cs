using Common;
using DBModel;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Context
{
    public class CustomerContext
    {
        public IList<CustomerModel> GetList(string customerId, string contactName, DataPage dataPage = null)
        {
            var list = new CustomerRepository().GetCustomers();
            if (!string.IsNullOrWhiteSpace(customerId))
            {
                list = list.Where(p => p.CustomerID.Contains(customerId)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(contactName))
            {
                list = list.Where(p => p.ContactName.Contains(contactName)).ToList();
            }
            dataPage.RowCount = list.Count();
            if (dataPage != null)
            {
                list = list.Skip((dataPage.PageIndex - 1) * dataPage.PageSize).Take(dataPage.PageSize).ToList();
            }
            return list;
        }
    }
}
