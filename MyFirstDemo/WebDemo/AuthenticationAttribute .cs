using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo
{
    public class AuthenticationAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity?.IsAuthenticated == false)
            {
                filterContext.HttpContext.Response.Redirect("/Index");
                return;
                //filterContext.Result = new RedirectToRouteResult(new );
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
