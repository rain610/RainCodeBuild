﻿using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBModel
{
    public class CustomerModel
    {
        public string CustomerID { get; set; }
        [HashMember]
        public string CompanyName { get; set; }
        [HashMember]
        public string ContactName { get; set; }
        [HashMember]
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
