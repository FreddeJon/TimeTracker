using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServer;

public static class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetService<IdentityDbContext>();
        if (context != null)
        {
            context.Database.Migrate();
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole() {Name = "Admin", NormalizedName = "ADMIN"});
                context.Roles.Add(new IdentityRole() {Name = "User", NormalizedName = "USER"});

                context.SaveChanges();
            }
        }

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var admin = userMgr.FindByNameAsync("Admin").Result;
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@timetracker.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(admin, "Admin123!").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddToRoleAsync(admin, "Admin").Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("Admin created");
        }
        else
        {
            Log.Debug("Admin already exists");
        }

        var user = userMgr.FindByNameAsync("User").Result;
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = "User",
                Email = "user@timetracker.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(user, "User123!").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            result = userMgr.AddToRoleAsync(user, "User").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("User created");
        }
        else
        {
            Log.Debug("User already exists");
        }
    }
}
