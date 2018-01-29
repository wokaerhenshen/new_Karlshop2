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

namespace new_Karlshop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args); // Revised to enable seeding.

            // Seed the data when the application starts.
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    Initialize initializer = new Initialize(context);
                    initializer.InitializeData();
                    RoleRepo roleIni = new RoleRepo(context);
                    roleIni.CreateInitialRoles();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }





            }

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
