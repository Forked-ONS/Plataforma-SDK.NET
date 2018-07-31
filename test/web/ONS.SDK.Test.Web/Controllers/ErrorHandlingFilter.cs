using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ONS.SDK.Logger;
using ONS.SDK.Worker;

namespace ONS.SDK.Test.Web.Controllers
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ErrorHandlingFilter> _logger;

        public ErrorHandlingFilter() {
            this._logger = SDKLoggerFactory.Get<ErrorHandlingFilter>();
        }

        public override void OnException(ExceptionContext context)
        {
            HandleExceptionAsync(context);
            context.ExceptionHandled = true;
        }

        private void HandleExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            this._logger.LogError("Error raise in request.", context.Exception);

            if (exception is SDKBusinessException || exception is SDKRuntimeException)
                SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            else
                SetExceptionResult(context, exception, HttpStatusCode.InternalServerError);
        }

        private static void SetExceptionResult(
            ExceptionContext context, 
            Exception exception, 
            HttpStatusCode code)
        {
            context.Result = new JsonResult(new { Message = exception.Message, Stack = exception.ToString() })
            {
                StatusCode = (int)code
            };
        }
    }
}