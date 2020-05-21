using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

public class LogAttribute : ActionFilterAttribute
{
    public LogAttribute()
    {

    }

    public override void OnActionExecuting (HttpActionContext actionContext)
    {
        Console.WriteLine("on Action Executing Strated");
    }

    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    {
        Console.WriteLine("on Action Executing Strated");
    }
}