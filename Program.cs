using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using new_Karlshop.Data;
using new_Karlshop.Repository;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Identity;

namespace new_Karlshop
{
    public class Program
    {
        public static void Main(string[] args)
        {

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();

            try
            {
                Log.Information("Starting web host");


                var host = BuildWebHost(args); // Revised to enable seeding.

                // Seed the data when the application starts.
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<ApplicationDbContext>();
                        var RoleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                        var userManager = services.GetRequiredService<UserManager<Models.ApplicationUser>>();
                        //RoleRepo roleIni = new RoleRepo(context);
                        //roleIni.CreateInitialRoles();


                        // This is another way to create roles
                        // I think this way is better
                        string[] roleNames = { "Admin", "MemberShip", "Customer", "Seller" };
                        Task<IdentityResult> roleResult;

                        foreach (var roleName in roleNames)
                        {
                            var roleExist = RoleManager.Roles.Where(r => r.Name == roleName).FirstOrDefault();
                            if (roleExist == null)
                            {
                                //create the roles and seed them to the database: Question 1
                                roleResult = RoleManager.CreateAsync(new IdentityRole(roleName));
                            }
                        }


                        Initialize initializer = new Initialize(context,userManager);
                        initializer.InitializeData();


                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while seeding the database.");
                    }

                }

                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
