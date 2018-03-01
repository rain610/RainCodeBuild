using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace WebDemo.Pages
{
    [Authorize]
    //[Authentication]
    //[AuthorizationFilters]
    public class IndexModel : PageModel
    {
        public async Task OnGet()
        {
            var cc = this.User.Identity.Name;
            if (this.User.Identity?.IsAuthenticated==true)
            {
                //Response.Cookies.Append
            }
            var aa = HttpContext.Session.GetString("userInfo");
            //var aa = HttpContext.Session.GetString("userInfo");
        }
    }
}
