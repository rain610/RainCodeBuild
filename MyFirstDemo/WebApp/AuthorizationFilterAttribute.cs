using DomainStandard.Interface;
using DomainStandard.Model.Authority;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizationFilterAttribute:Attribute, IAsyncAuthorizationFilter
    {
        private readonly IAuthorityRepository _authorityRepository;

        public AuthorizationFilterAttribute(IAuthorityRepository authorityRepository)
        {
            _authorityRepository = authorityRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(filter => filter is AllowAnonymousAttribute))
            {
                return;
            }

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (context.Filters.Any(filter => filter is AllowLoginInAttribute))
                {
                    return;
                }
                var userClaims = context.HttpContext.User.Claims.ToArray();
                var userId = int.Parse(userClaims[0].Value);
                var menus = await _authorityRepository.GetMenuList(new GetMenuListRequest { UserId = userId });
                var path = context.HttpContext.Request.Path;
                if (path.Equals("/"))
                {
                    return;
                }
                if (menus.Menus.Any(t => t.Address.ToLower().Contains(path) || t.ChildMenus != null && t.ChildMenus.Any(sub => sub.Address.ToLower().Contains(path))))
                {
                    return;
                }
                context.HttpContext.Response.Redirect("/home/index");
            }
            else
            {
                context.HttpContext.Response.Redirect("/home/login");
            }
            return;
        }

    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AllowAnonymousAttribute : Attribute, IFilterMetadata
    {

    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AllowLoginInAttribute : Attribute, IFilterMetadata { }
}
