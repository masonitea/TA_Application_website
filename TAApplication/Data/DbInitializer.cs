using TAApplication.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TAApplication.Areas.Identity.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

/**
  Author:    Mason Austin
  Partner:   None
  Date:      10 / 19 / 2021
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Mason Austin - This work may not be copied for use in Academic Coursework.

  I, Mason Austin, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents:
    This file is used to initialize data for both TA_DB and TAUsersRolesDB.
    TA_DB needs both courses and application seeded.
    TAUsersRolesDB needs to seed users and roles.
*/


namespace TAApplication.Data
{
    public class DbInitializer
    {
        /**
         * Ensures the TA_DB is created. If there is no students, students are seeded into TA_DB
         */
        public static void Initialize(TA_DB context, IWebHostEnvironment environment)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (!context.Applications.Any())
            {
                TA_DB_Initializer.InitializeApplications(context);
            }

            // Look for any courses.
            if (!context.Courses.Any())
            {
                TA_DB_Initializer.InitializeCourses(context);
            }

            // Look for any enrollment data
            if (!context.EnrollmentDatas.Any())
            {
                TA_DB_Initializer.InitializeEnrollmentData(context, environment);
            }
        }
    }

    /**
     * Initializes 3 roles by checking if they're there and creating them if they're not there
     * Users are then initialized in the same fashion and are assigned roles depending on their email
     */
    public class IdentityInitializer
    {
        public static async Task Initialize(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var identityContext = services.GetService<TAUsersRolesDB>();

                try
                {
                    identityContext.Database.Migrate();
                }
                catch
                {

                }
                

                string[] roles = new string[] { "Administrator", "Professor", "Applicant" };

                // Ensure our roles exist
                foreach (string role in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(identityContext);

                    if (!identityContext.Roles.Any(r => r.Name == role))
                    {
                        var nr = new IdentityRole(role);
                        nr.NormalizedName = role.ToUpper();
                        await roleStore.CreateAsync(nr);
                    }
                }

                // All users
                var user1 = new TAUser
                {
                    Email = "admin@utah.edu",
                    NormalizedEmail = "ADMIN@UTAH.EDU",
                    UserName = "admin@utah.edu",
                    NormalizedUserName = "ADMIN@UTAH.EDU",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Unid="u9999999",
                    FirstName = "Scott",
                    LastName = "Jefferson"
                };

                var user2 = new TAUser
                {
                    Email = "professor@utah.edu",
                    NormalizedEmail = "PROFESSOR@UTAH.EDU",
                    UserName = "professor@utah.edu",
                    NormalizedUserName = "PROFESSOR@UTAH.EDU",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Unid="u0000008",
                    FirstName = "Michael",
                    LastName = "Jordan"
                };

                var user3 = new TAUser
                {
                    Email = "u0000000@utah.edu",
                    NormalizedEmail = "U0000000@UTAH.EDU",
                    UserName = "u0000000@utah.edu",
                    NormalizedUserName = "U0000000@UTAH.EDU",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Unid = "u0000000",
                    FirstName = "John",
                    LastName = "Doe"
                };

                var user4 = new TAUser
                {
                    Email = "u0000001@utah.edu",
                    NormalizedEmail = "U0000001@UTAH.EDU",
                    UserName = "u0000001@utah.edu",
                    NormalizedUserName = "U0000001@UTAH.EDU",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Unid= "u0000001",
                    FirstName = "Krishna",
                    LastName = "Patel"
                };

                var user5 = new TAUser
                {
                    Email = "u0000002@utah.edu",
                    NormalizedEmail = "U0000002@UTAH.EDU",
                    UserName = "u0000002@utah.edu",
                    NormalizedUserName = "U0000002@UTAH.EDU",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Unid="u0000002",
                    FirstName="Anneswa",
                    LastName="Ghosh"
                };

                var user6 = new TAUser
                {
                    Email = "u0000003@utah.edu",
                    NormalizedEmail = "U0000003@UTAH.EDU",
                    UserName = "u0000003@utah.edu",
                    NormalizedUserName = "U0000003@UTAH.EDU",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Unid="u0000003",
                    FirstName="Jesse",
                    LastName="Sinclair"
                };

                var user7 = new TAUser
                {
                    Email = "u0000004@utah.edu",
                    NormalizedEmail = "U0000004@UTAH.EDU",
                    UserName = "u0000004@utah.edu",
                    NormalizedUserName = "U0000004@UTAH.EDU",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Unid = "u0000004",
                    FirstName="Nhan",
                    LastName="Nguyen"
                };

                var user8 = new TAUser
                {
                    Email = "u0000005@utah.edu",
                    NormalizedEmail = "U0000005@UTAH.EDU",
                    UserName = "u0000005@utah.edu",
                    NormalizedUserName = "U0000005@UTAH.EDU",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Unid = "u0000005",
                    FirstName="Mason",
                    LastName="Austin"
                };

                TAUser[] users = new TAUser[8] {user1, user2, user3, user4, user5, user6, user7, user8};

                // Create each user if they don't already exist
                foreach(TAUser user in users) {
                   if (!identityContext.Users.Any(u => u.UserName == user.UserName))
                   {
                        var password = new PasswordHasher<TAUser>();
                        var hashed = password.HashPassword(user, "123ABC!@#def");
                        user.PasswordHash = hashed;

                        var userStore = new UserStore<TAUser>(identityContext);
                        await userStore.CreateAsync(user);
                        if (user.Email.StartsWith("admin"))
                        {
                            await AssignRoles(services, user.Email, new string[1] { "ADMINISTRATOR" });
                        }
                        else if (user.Email.StartsWith("professor"))
                        {
                            await AssignRoles(services, user.Email, new string[1] { "PROFESSOR" });
                        }
                        else
                        {
                            await AssignRoles(services, user.Email, new string[1] { "APPLICANT" });
                        }
                   }
                }

                // Save
                await identityContext.SaveChangesAsync();

                var DbContext = services.GetService<TA_DB>();
                if (!DbContext.Availabilities.Any())
                {
                    TA_DB_Initializer.InitializeAvailabilities(DbContext, identityContext);
                }
            }
        }

        /**
         * Helper method that assigns provided roles to the provided email.
         */
        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            var _userManager = services.GetService<UserManager<TAUser>>();
            TAUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);
            return result;
        }
    }
}
