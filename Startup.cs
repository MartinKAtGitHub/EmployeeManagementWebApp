using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
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
            services.AddDbContextPool<AppDdContext>(
                options => 
                {
                    options.UseSqlServer(_config.GetConnectionString("EmployeeDbConnection"));
                });

            //65
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                //68
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;

            }).AddEntityFrameworkStores<AppDdContext>();
            // 68 -- we can make it its own thing if you want to change the options of diffrent things but since we we only are op Password setting we can do it above 
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 5;
            //    options.Password.RequiredUniqueChars = 0;
            //});

           // Action<MvcOptions> optionSettings = op => op.EnableEndpointRouting = false;
            services.AddMvc(options => {
                //71 Sets the whole site. only for Authorized users only
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                            

                options.EnableEndpointRouting = false;
                options.Filters.Add(new AuthorizeFilter(policy));
                });


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
            else
            {

                // Uses the ErrorController 
                //60
                app.UseExceptionHandler("/Error");
                //59
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }


            //app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();

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
