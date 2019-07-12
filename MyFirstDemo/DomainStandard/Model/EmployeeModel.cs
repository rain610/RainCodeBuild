using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainStandard.Model
{
    public class EmployeeModel
    {
        public long EmployeeID { get; set; }
        [HashMember]
        public string LastName { get; set; }
        [HashMember]
        public string FirstName { get; set; }
        public byte[] Hash { get; set; }
    }
}
