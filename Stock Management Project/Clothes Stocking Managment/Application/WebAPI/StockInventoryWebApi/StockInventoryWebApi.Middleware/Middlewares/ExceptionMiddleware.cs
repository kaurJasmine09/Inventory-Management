using Microsoft.AspNetCore.Http;
using StockInventoryWebApi.BusinessModel.ErrorModel;
using System;
using System.Net;
using System.Threading.Tasks;

namespace StockInventoryWebApi.Middleware.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandlerExceptionAsync(httpContext, ex);
            }
        }
        private Task HandlerExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }.ToString());
        }

    }
}
