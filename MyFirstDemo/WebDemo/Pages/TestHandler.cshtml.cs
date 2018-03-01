using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDemo.Pages
{
    public class TestHandlerModel : PageModel
    {
        public void OnGet()
        {

        }

        //public async Task OnGetAsync()
        //{

        //}

        public void OnPost(string id)
        {
        }

        [IgnoreAntiforgeryToken]
        public void OnPostFirst(string id)
        {

        }

        public void OnPostSecond(string id)
        {

        }

        public void OnPostThird()
        {
            var aa = Request;
        }

        public void OnGetFirst123()
        {
            var aa = Request;
        }
    }
}