using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DiamondShopContext : DbContext
    {
        public DiamondShopContext()
        { }

        public DiamondShopContext(DbContextOptions<DiamondShopContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Gem> Gems { get; set; }
        public DbSet<ProductGem> ProductGems { get; set; }
        public DbSet<MaterialPriceList> MaterialPriceLists { get; set; }
        public DbSet<GemPriceList> GemPriceLists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Warranty> Warranties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Customer" },
                new Role { RoleID = 2, RoleName = "SalesStaff" },
                new Role { RoleID = 3, RoleName = "Manager" },
                new Role { RoleID = 4, RoleName = "Admin" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Username = "User1", Password = "Password1", Email = "user1@example.com", PhoneNumber = "1234567890", Address = "Address1", RoleID = 1, UserStatus = true, NiSize = "S" },
                new User { UserID = 2, Username = "User2", Password = "Password2", Email = "user2@example.com", PhoneNumber = "0987654321", Address = "Address2", RoleID = 2, UserStatus = true, NiSize = "M" }
            );
        }
    }

}
