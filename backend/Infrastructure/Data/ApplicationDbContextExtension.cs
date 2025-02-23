using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Data
{
    public static class ApplicationDbContextExtension
    {
        public static void Seed(this ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Email = "admin@example.com", PasswordHash = HashPassword("password") },
                    new User { Email = "user1@example.com", PasswordHash = HashPassword("password") }
                // Add more users as needed
                );
                context.SaveChanges();
            }
        }
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
