using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Context.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace WebDemo.Pages
{
    public class IocIndexModel : PageModel
    {
        private readonly IEmployeeContext _employeeContext;
        private readonly IReflectionDemo _reflectionDemo;
        public IocIndexModel(IEmployeeContext employeeContext, IHttpContextAccessor httpContextAccessor)
        {
            _employeeContext = employeeContext;
            //必须引用Microsoft.Extensions.DependencyInjection,这种方法一般不推荐
            _reflectionDemo = httpContextAccessor.HttpContext.RequestServices.GetService<IReflectionDemo>();
        }
        public void OnGet()
        {
            try
            {
                //var list = _employeeContext.GetList(string.Empty, string.Empty);
                var test1 = _reflectionDemo.Test1();
            }
            catch (Exception ex)
            {

            }
        }
    }
}