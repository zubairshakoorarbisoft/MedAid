using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MedAidAPI.Areas.Identity.Data;
using MedAidAPI.Models;

[assembly: HostingStartup(typeof(MedAidAPI.Areas.Identity.IdentityHostingStartup))]
namespace MedAidAPI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MedAidAPIContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MedAidAPIConnectionString")));

                services.AddDefaultIdentity<MedAidAPIUser>()
                    .AddEntityFrameworkStores<MedAidAPIContext>();
            });
        }
    }
}