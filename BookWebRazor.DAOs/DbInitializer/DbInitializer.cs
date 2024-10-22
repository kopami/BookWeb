using BookWebRazor.BusinessObjects.Enum;
using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWebRazor.DAOs.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DbInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            if (_dbContext.Database.GetPendingMigrations().Count() > 0)
            {
                _dbContext.Database.Migrate();
            };

            SeedAdminUser();
        }

        private void SeedAdminUser()
        {
            // Check if an admin user already exists
            var adminUser = _dbContext.Account.FirstOrDefault(user => user.Role == RoleEnum.Admin.ToString());
            if (adminUser == null)
            {
                // If no admin user exists, create one
                var newAdminUser = new Account
                {
                    Email = "admin@example.com",
                    // Hash the password for security
                    Password = BCrypt.Net.BCrypt.HashPassword("12345678"),
                    Role = RoleEnum.Admin.ToString()
                };

                _dbContext.Account.Add(newAdminUser);
                _dbContext.SaveChanges();
            }
        }
    }
}
