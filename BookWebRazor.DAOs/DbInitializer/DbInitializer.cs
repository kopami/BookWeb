using BookWebRazor.BusinessObjects.Enum;
using BookWebRazor.BusinessObjects.Model;
using BookWebRazor.DAOs.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            SeedCategory();
            SeedProduct();
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

        private void SeedCategory()
        {
            if (_dbContext.Categories.Any())
            {
                return;
            }
            var categories = new List<Category>()
                {
                    new() { Name = "Action", DisplayOrder = 1 }, //id is auto increment
                    new() { Name = "SciFic", DisplayOrder = 2 },
                    new() { Name = "History", DisplayOrder = 3 }
                };
            _dbContext.Categories.AddRange(categories);
            _dbContext.SaveChanges();
        }

        private void SeedProduct()
        {
            if (_dbContext.Products.Any())
            {
                return;
            }
            var products = new List<Product>()
            {
                new Product
                {
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SWD9999001",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1
                },
                new Product
                {
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1
                },
                new Product
                {
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 2
                },
                new Product
                {
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 3
                },
                new Product
                {
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId= 3
                },
                new Product
                {
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId= 3
                }
            };
            _dbContext.Products.AddRange(products);
            _dbContext.SaveChanges();
        }

    }
}
