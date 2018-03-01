using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DBModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebDemo.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty] // Bind on Post
        public LogInModel LogInModel { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (!Validate())
                {
                    ModelState.AddModelError("", "username or password is invalid");
                    return Page();
                }
                var identity = new ClaimsIdentity("userInfo");
                identity.AddClaim(new Claim(ClaimTypes.Name,LogInModel.Username));
                identity.AddClaim(new Claim(ClaimTypes.Role,"admin"));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, 
                    new AuthenticationProperties { ExpiresUtc =DateTimeOffset.UtcNow.AddHours(12)});
                HttpContext.Session.SetString("userInfo",JsonConvert.SerializeObject(new { Username="08277" }));
                //注销授权     
                //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //return RedirectToPage("Index");
                return RedirectToPage("Index");
            }
            return Page();
        }

        private bool Validate()
        {
            if (LogInModel == null)
            {
                return false;
            }
            if (LogInModel.Username == "08277" && LogInModel.Password == "123456")
            {
                return true;
            }
            return false;
        }
    }
}