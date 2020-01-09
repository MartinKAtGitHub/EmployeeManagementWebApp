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
using Portfolio_Website_Core.Security;

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
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
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
            services.AddMvc(options =>
            {
                //71 Sets the whole site. only for Authorized users only
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();


                options.EnableEndpointRouting = false;
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            // 94
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role", "true")); //User needs both these claims to be able to use the DeleteRole policy
            });

            // 98 Claim can have 1 of these values to be true. country can have USA, UK or CANADA
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AllowedCountries", // "
            //        policy => policy.RequireClaim("Country", "USA","UK","Canada")); //User needs both these claims to be able to use the DeleteRole policy
            //});

            //99 // If you ever want to set up a custom policy where 1 OR many conditions need to be met
            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditRolePolicy",
                    policy => policy.RequireAssertion(context =>
                        context.User.IsInRole("Admin") &&
                        context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true") ||
                        context.User.IsInRole("Super Admin")
                    ));
            });

            // 101 Custom logic for authorization, not just basic role/claims 
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Custom_EditRolePolicy",
                    policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));
            });

            //95
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin"));//  policy.RequireRole("Admin, Test Role") // If you need more roles in 1 policy
            });

            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>(); // <- dependency injection. If a class is using the IEmployeeRepository create a instance of MockEmployeeRepository and inject it to the class
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>(); // <- dependency injection. If a class is using the IEmployeeRepository create a instance of MockEmployeeRepository and inject it to the class
           
            services.AddSingleton<IAuthorizationHandler, CanEditOnluOtherAdminRolesAndClaimsHandler>();
            services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
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

            app.UseMvc(routeBuilder =>
            {
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
