using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockInventoryWebApi.Entities.GenericRepo;
using StockInventoryWebApi.Middleware.Configuration;
using StockInventoryWebApi.Services.IService;
using StockInventoryWebApi.Services.Service;
using StockInventoryWebApi.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockInventoryWebApi.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                {
                    options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                });
            });
            services.AddSwaggerGen(c =>
                   {
                       c.SwaggerDoc(
                           name: "v1",
                           new Microsoft.OpenApi.Models.OpenApiInfo
                           {
                               Title ="Stock Inventory Web API",
                               Version = "v1"
                           });
                   }

                );
            // get the connection string from appsettings.json
            var connectionString = Configuration.GetConnectionString("StockInv_DB");
            services.AddDbContext<ClothingStock_DBContext>(options =>
            options.UseSqlServer(connectionString));

            services.AddScoped<GenericUnitOfWork<ClothingStock_DBContext>>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStockService, StockService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseRouting();

            // Setting middleware for global level exception handling.
            app.ConfigureExceptionMiddleware();
            app.UseAuthorization();
            // global CORS policy
            app.UseCors("AllowOrigin");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
