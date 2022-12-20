using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TAApplication.Areas.Identity.Data;
using TAApplication.Data;

/**
  Author:    Mason Austin
  Partner:   None
  Date:      10 / 19 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    Handles the identity database (TAUsersRolesDB) start up process.
*/

[assembly: HostingStartup(typeof(TAApplication.Areas.Identity.IdentityHostingStartup))]
namespace TAApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TAUsersRolesDB>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TAUsersRolesDBConnection")));

                // Select options here
                services.AddDefaultIdentity<TAUser>(options => options.SignIn.RequireConfirmedAccount = true)
                   .AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<TAUsersRolesDB>()
                   .AddDefaultUI()
                   .AddDefaultTokenProviders();

                services.Configure<IdentityOptions>(options =>
                {
                    // Default Password settings.
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                });
            });
        }

    }
}