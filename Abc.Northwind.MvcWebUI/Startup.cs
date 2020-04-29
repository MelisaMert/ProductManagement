using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Northwind.Business.Abstract;
using Abc.Northwind.Business.Concrete;
using Abc.Northwind.DataAccess.Abstact;
using Abc.Northwind.DataAccess.Concrete.EntityFramework;
using Abc.Northwind.MvcWebUI.Entities;
using Abc.Northwind.MvcWebUI.Middlewares;
using Abc.Northwind.MvcWebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Abc.Northwind.MvcWebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddSingleton<ICartSessionService, CartSessionService>();
            services.AddSingleton<ICartService,CartService>();
            services.AddDbContext<CustomIdentityDbContext>(options => options.UseSqlServer("Server=DESKTOP-GARUU39;Database=Northwind;Trusted_Connection=True;"));
            services.AddIdentity<CustomIdentityUser, CustomIdentityRole>()
                    .AddEntityFrameworkStores<CustomIdentityDbContext>()
                    .AddDefaultTokenProviders();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Middleware - 1 (Developer Hataları)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Middleware - 2 (Static File Yönetimi)
            app.UseStaticFiles();
            // Middleware Klasik static dosyaları yönetir.
            app.UseFileServer();
            // Custom Middleware
            app.UseNodeModules(env.ContentRootPath);
            app.UseAuthentication();
            app.UseSession();
            // Middleware - 3 (Mvc Routing)
            // app.UseMvcWithDefaultRoute();
            app.UseMvc(ConfigureRoutes);
        }
        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // Home/Product/1
            routeBuilder.MapRoute("Default", "{controller=Product}/{action=Index}");
        }
    }
}
