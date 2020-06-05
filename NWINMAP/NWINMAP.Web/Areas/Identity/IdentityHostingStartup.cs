﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NWINMAP.Web.Areas.Identity.Data;
using NWINMAP.Web.Data;

[assembly: HostingStartup(typeof(NWINMAP.Web.Areas.Identity.IdentityHostingStartup))]
namespace NWINMAP.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        const int SESSIONEXPIRATIONINMINUTES = 30;

        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityWebContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("IdentityWebContextConnection")));

                //services.AddDefaultIdentity<IdentityWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<IdentityWebContext>();

                services.AddDefaultIdentity<IdentityWebUser>()
                    .AddRoles<IdentityRole>()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<IdentityWebContext>();

                services.Configure<IdentityOptions>(options =>
                {
                    //options.SignIn.RequireConfirmedEmail = true;	

                    // Password settings	
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;

                    // Lockout settings	
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);// TimeSpan.FromMinutes(SESSIONEXPIRATIONINMINUTES);	
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    //options.Lockout.AllowedForNewUsers = false;

                    // User settings	
                    options.User.RequireUniqueEmail = true;
                });

                services.ConfigureApplicationCookie(options =>
                {

                    options.Cookie.Name = IdentityConstants.ApplicationScheme;
                    // Cookie settings	
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(SESSIONEXPIRATIONINMINUTES);
                    //options.Cookie.Expiration = TimeSpan.FromMinutes(SESSIONEXPIRATIONINMINUTES);
                    // If the LoginPath isn't set, ASP.NET Core defaults 	
                    // the path to /Account/Login.	
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    // If the AccessDeniedPath isn't set, ASP.NET Core defaults 	
                    // the path to /Account/AccessDenied.	
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                    options.SlidingExpiration = true;

                    options.Events.OnRedirectToLogin = ReplaceRedirector(StatusCodes.Status401Unauthorized, options.Events.OnRedirectToLogin);
                    options.Events.OnRedirectToAccessDenied = ReplaceRedirector(StatusCodes.Status403Forbidden, options.Events.OnRedirectToAccessDenied);
                });

                // Add application services.	
                //services.AddTransient<IEmailSender, EmailSender>();

            });
        }

        static Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirector(int statusCode, Func<RedirectContext<CookieAuthenticationOptions>, Task> existingRedirector) => context =>
        {
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                context.Response.StatusCode = statusCode;
                return Task.CompletedTask;
            }
            return existingRedirector(context);
        };
    }

    public sealed class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}