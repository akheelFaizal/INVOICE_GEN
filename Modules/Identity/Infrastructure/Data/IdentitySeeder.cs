using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceSystem.Identity.Core.Entities;
using InvoiceSystem.Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Identity.Infrastructure.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        Console.WriteLine("--- Identity Seeding Started ---");

        // 1. Ensure Roles exist
        var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
        var accountantRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Accountant");

        if (adminRole == null)
        {
            Console.WriteLine("Creating Admin role...");
            adminRole = new Role { Id = Guid.NewGuid(), Name = "Admin" };
            context.Roles.Add(adminRole);
        }

        if (accountantRole == null)
        {
            Console.WriteLine("Creating Accountant role...");
            accountantRole = new Role { Id = Guid.NewGuid(), Name = "Accountant" };
            context.Roles.Add(accountantRole);
        }

        await context.SaveChangesAsync();

        // 2. Ensure at least one Admin exists
        var anyAdmin = await context.UserRoles.AnyAsync(ur => ur.Role.Name == "Admin");
        Console.WriteLine($"Admin check: {(anyAdmin ? "Admin exists" : "No Admin found")}");

        if (!anyAdmin)
        {
            // Find the first user who doesn't have the admin role (or any user if no roles assigned)
            var userToPromote = await context.Users.FirstOrDefaultAsync();
            
            if (userToPromote != null)
            {
                Console.WriteLine($"Promoting user {userToPromote.Email} to Admin...");
                
                // Ensure we don't add duplicate role assignments
                var alreadyHasRole = await context.UserRoles.AnyAsync(ur => ur.UserId == userToPromote.Id && ur.RoleId == adminRole.Id);
                if (!alreadyHasRole)
                {
                    context.UserRoles.Add(new UserRole 
                    { 
                        UserId = userToPromote.Id, 
                        RoleId = adminRole.Id 
                    });
                    await context.SaveChangesAsync();
                    Console.WriteLine("Promotion successful. Please RE-LOGIN to get the new Admin token.");
                }
            }
            else
            {
                Console.WriteLine("No users found to promote. Register a user and RESTART the app.");
            }
        }

        Console.WriteLine("--- Identity Seeding Completed ---");
    }
}
