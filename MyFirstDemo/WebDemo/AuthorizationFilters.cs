using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo
{
    public class AuthorizationFilters: Attribute,IAuthorizationFilter
    {
        /// <summary>  
        ///  请求验证，当前验证部分不要抛出异常，ExceptionFilter不会处理  
        /// </summary>  
        /// <param name="context">请求内容信息</param>  
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var aa = context.HttpContext.Request.Cookies["access_token"];
            context.HttpContext.Response.Redirect("/login");
            var bb = context.HttpContext.Request.Path;
            return;   
        }
    }
}
