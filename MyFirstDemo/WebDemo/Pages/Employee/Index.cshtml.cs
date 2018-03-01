using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Context;
using Context.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDemo.Pages.Employee
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public DBModel.EmployeeModel SearchModel { get; set; }
        public IList<DBModel.EmployeeModel> EmployeeList { get; private set; }

        private readonly IEmployeeContext _employeeContext;

        public IndexModel(IEmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public void OnGet()
        {
            //var page = new DataPage
            //{
            //    PageIndex = string.IsNullOrWhiteSpace(Request.Query["PageIndex"][0])?1:int.Parse(Request.Query["PageIndex"][0]),
            //    PageSize = strint.Parse(Request.Query["PageSize"][0])
            //};
            EmployeeList = _employeeContext.GetList(string.Empty, string.Empty, null);
        }

        public void OnPost()
        {
            //var page = new DataPage
            //{
            //    PageIndex = string.IsNullOrWhiteSpace(Request.Query["PageIndex"][0])?1:int.Parse(Request.Query["PageIndex"][0]),
            //    PageSize = strint.Parse(Request.Query["PageSize"][0])
            //};
            EmployeeList = new EmployeeContext().GetList(SearchModel.FirstName, SearchModel.LastName, null);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int EmployeeID)
        {
            new EmployeeContext().Delete(EmployeeID);
            return RedirectToPage();
        }
    }
}