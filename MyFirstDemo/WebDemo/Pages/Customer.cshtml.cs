using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace WebDemo.Pages
{
    public class CustomerModel : PageModel
    {
        public DBModel.PageModel<DBModel.CustomerModel> Customers { get; set; }
        public IActionResult OnGet()
        {
            //var list = new CustomerContext().GetList();
            //Customers = new DBModel.PageModel<DBModel.CustomerModel> { total = list.Count,rows= list.Take(10).ToList() };
            //return Content(JsonConvert.SerializeObject(Customers));
            return Page();
        }

        public ContentResult OnGetInit()
        {
            //var parameterList = new List<KeyValuePair<string, StringValues>>(Request.Query);
            var page = new DataPage
            {
                PageIndex = int.Parse(Request.Query["PageIndex"][0]),
                PageSize=int.Parse(Request.Query["PageSize"][0])
            };
            var list = new CustomerContext().GetList(Request.Query["CustomerID"][0], Request.Query["ContactName"][0], page);
            Customers = new DBModel.PageModel<DBModel.CustomerModel> { total = page.RowCount, rows = list };
            return Content(JsonConvert.SerializeObject(Customers));
        }

        public void OnPost()
        {
            //var list = new CustomerContext().GetList();
            //Customers = new DBModel.PageModel<DBModel.CustomerModel> { total = list.Count, rows = list.Take(10).ToList() };
        }
    }
}