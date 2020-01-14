using System;
using iWriter.Areas.Identity.Data;
using iWriter.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(iWriter.Areas.Identity.IdentityHostingStartup))]
namespace iWriter.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<iWriterContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("iWriterContextConnection")));

                services.AddDefaultIdentity<iWriterUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<iWriterContext>();
            });
        }
    }
}