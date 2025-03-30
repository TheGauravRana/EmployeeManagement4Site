using EmployeeManagement.DTO;
using EmployeeManagement.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace EmployeeManagement
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;

        }
                
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC services
            services.AddControllersWithViews();

            // Register session services
            services.AddSession(options =>
            {
                // Set session timeout to 30 minutes (or as desired)
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                   .AddCookie(options =>
                   {
                       // Redirect unauthenticated users
                       options.LoginPath = "/Login/Index";
                       options.AccessDeniedPath = "/UnauthorizedUser";
                   });
            services.AddAuthorization(options =>
                   {
                       options.AddPolicy("AdminOnly", policy =>
                       policy.RequireClaim(ClaimTypes.Role, "Admin"));
                   });


            services.AddMvc().AddXmlDataContractSerializerFormatters();
            // Register the in-memory user repository as a singleton.
            services.AddSingleton<IUserRepository, MockUserRepository>();
            services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            services.AddSingleton<IEmployeeWorkRepository, MockEmployeeWorkRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IWebHostEnvironment env,
                              ILogger<Startup> logger)

        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            // Enable session middleware
            app.UseSession();

            // Enable authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();               
            });



        }
    }
}
