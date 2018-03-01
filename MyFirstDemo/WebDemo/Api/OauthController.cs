using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebDemo.Api
{
    [Produces("application/json")]
    [Route("api/Oauth")]
    public class OauthController : Controller
    {
        //public ActionResult AuthorizationCodeCallback(string state, string code, string returnUri)
        //{
        //    var request = HttpContext.Request;
        //    var cookie = request.Cookies["stage"];

        //}
        [Route("AuthorizationCodeCallback")]
        [HttpGet]
        public async Task<ActionResult> AuthorizationCodeCallback()
        {
            //var user = new JObject();
            //user.Add("userId", 10810);
            //user.Add("username", "魏中雨");
            //var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user["userId"].Value<string>()));
            //identity.AddClaim(new Claim(ClaimTypes.Name, user["username"].Value<string>()));
            //var principal = new ClaimsPrincipal(identity);
            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { ExpiresUtc = DateTimeOffset.Now.AddHours(12) });
            Response.Cookies.Append("access_token","08277",new CookieOptions { Expires=DateTime.UtcNow.AddHours(12)});
            return Redirect("/index");
        }
    }
}