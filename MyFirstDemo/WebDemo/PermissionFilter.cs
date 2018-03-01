using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    
    public class PermissionFilter: Attribute, IAsyncAuthorizationFilter
    {
        public PermissionFilter(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var aa = context.HttpContext.Request.Cookies["userInfo"];
            if (string.IsNullOrEmpty(aa))
            {
                context.HttpContext.Response.Redirect("/login");
            }
            //var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            //var authorizationResult = await authorizationService.AuthorizeAsync(context.HttpContext.User, null, new PermissionAuthorizationRequirement(Name));
            //if (!authorizationResult.Succeeded)
            //{
            //    context.Result = new ForbidResult();
            //}
        }
    }
}
