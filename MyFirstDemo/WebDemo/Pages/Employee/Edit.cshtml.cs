using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using DBModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDemo.Pages.Employee
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public EmployeeModel Employee { get; set; }
        public void OnGet(int id)
        {
            var dataList= new EmployeeContext().GetList(string.Empty, string.Empty, null);
            Employee = dataList.FirstOrDefault(p => p.EmployeeID == id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            new EmployeeContext().Update(Employee);
            return RedirectToPage("./Index");
        }
    }
}