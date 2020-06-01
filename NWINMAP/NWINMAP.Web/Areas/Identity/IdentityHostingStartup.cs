using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
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
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityWebContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("IdentityWebContextConnection")));

                services.AddDefaultIdentity<IdentityWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<IdentityWebContext>();
            });
        }
    }
}