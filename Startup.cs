using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio_Website_Core.Models;

namespace Portfolio_Website_Core
{
    public class Startup
    {

        private IConfiguration _config; // can access Appsettings.json from this
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDdContex>(
                options => 
                {
                    options.UseSqlServer(_config.GetConnectionString("EmployeeDbConnection"));
                });

            Action<MvcOptions> optionSettings = op => op.EnableEndpointRouting = false;
            services.AddMvc(/*op => op.EnableEndpointRouting = false*/ optionSettings);
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>(); // <- dependency injection. If a class is using the IEmployeeRepository create a instance of MockEmployeeRepository and inject it to the class
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>(); // <- dependency injection. If a class is using the IEmployeeRepository create a instance of MockEmployeeRepository and inject it to the class
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseFileServer(); // combines both useDefault, UseStatic and UseDirectoryBrowser
            app.UseCookiePolicy();

            //app.UseMvcWithDefaultRoute();
           
            app.UseMvc(routeBuilder => {
                // same thing as the default setup above
                routeBuilder.MapRoute("Default", "{controller=Home}/{Action=index}/{id?}");
                
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
