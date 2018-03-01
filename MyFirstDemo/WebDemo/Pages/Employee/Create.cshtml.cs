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
    public class CreateModel : PageModel
    {
        [BindProperty]
        public EmployeeModel Employee { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            new EmployeeContext().Add(Employee);
            return RedirectToPage("Index");
        }
    }
}