using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderMgmtService
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    public class GlobalExceptionHandler : ExceptionFilterAttribute
    {
        public GlobalExceptionHandler()
        {

        }
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            if (context.Exception is Exception)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            }
        }
    }
}