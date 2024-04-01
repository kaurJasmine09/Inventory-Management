using Microsoft.AspNetCore.Builder;
using StockInventoryWebApi.Middleware.Middlewares;

namespace StockInventoryWebApi.Middleware.Configuration
{
    public static class ConfigureCustomExceptionMiddleware
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
