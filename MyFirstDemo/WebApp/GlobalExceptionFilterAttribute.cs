using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public sealed class GlobalExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public ILogger Logger;
        private Action<ILogger, string, Exception> _log;

        public GlobalExceptionFilterAttribute(
            ILogger<GlobalExceptionFilterAttribute> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _log = LoggerMessage.Define<string>(LogLevel.Error, 0, "{0}");
        }
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            _log(Logger, context.Exception.ToString(), context.Exception);
            return HandleExceptionAsync(context);
        }

        private Task HandleExceptionAsync(ExceptionContext context)
        {
            //Place your exception handling operation here.
            return base.OnExceptionAsync(context);
        }
    }
}
