using System.Net;
using Force.Ccc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HightechAngular.Web.Filters
{
    public class ExceptionsFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var code = HttpStatusCode.InternalServerError;
            if (context.Exception is IHasUserFrendlyMessage)
            {
                code = HttpStatusCode.UnprocessableEntity;
            }
            
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int) code;
            context.Result = new JsonResult(new
            {
                error = new[] {context.Exception.Message},
                stackTrace = context.Exception.StackTrace
            });
        }
    }
}