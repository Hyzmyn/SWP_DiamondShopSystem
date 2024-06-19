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

        //public DbSet<Role> Roles { get; set; }
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(GetConnectionString());

        private string? GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:DefaultConnection"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discount>()
                .Property(p => p.DiscountAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Gem>()
                .Property(p => p.FourC)
                .HasPrecision(18, 2);

            modelBuilder.Entity<GemPriceList>()
                .Property(p => p.CaratWeight)
                .HasPrecision(18, 2);

            modelBuilder.Entity<GemPriceList>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MaterialPriceList>()
                .Property(p => p.BuyPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<MaterialPriceList>()
                .Property(p => p.SellPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.PriceRate)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductionCost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ProductMaterial>()
                .Property(p => p.Weight)
                .HasPrecision(18, 2);

            //modelBuilder.Entity<Role>().HasData(
            //    new Role { RoleID = 1, RoleName = "Customer" },
            //    new Role { RoleID = 2, RoleName = "SalesStaff" },
            //    new Role { RoleID = 3, RoleName = "Manager" },
            //    new Role { RoleID = 4, RoleName = "Admin" }
            //);

            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Username = "User1", Password = "123", Email = "a@example.com", PhoneNumber = "1234567890", Address = "Address1", RoleID = 1, UserStatus = true, NiSize = "S" },
                new User { UserID = 2, Username = "User2", Password = "123", Email = "b@example.com", PhoneNumber = "0987654321", Address = "Address2", RoleID = 2, UserStatus = true, NiSize = "M" },
                new User { UserID = 3, Username = "User3", Password = "123", Email = "c@example.com", PhoneNumber = "1234567890", Address = "Address3", RoleID = 3, UserStatus = true, NiSize = "M" },
                new User { UserID = 4, Username = "User4", Password = "123", Email = "d@example.com", PhoneNumber = "0987654321", Address = "Address4", RoleID = 4, UserStatus = true, NiSize = "M" },
                new User { UserID = 5, Username = "User5", Password = "123", Email = "e@example.com", PhoneNumber = "1234567890", Address = "Address5", RoleID = 5, UserStatus = true, NiSize = "M" },
                new User { UserID = 6, Username = "User6", Password = "Password", Email = "user6@example.com", PhoneNumber = "0987654321", Address = "Address2", RoleID = 5, UserStatus = true, NiSize = "M" },
                new User { UserID = 7, Username = "User7", Password = "Password", Email = "user7@example.com", PhoneNumber = "0987654321", Address = "Address2", RoleID = 5, UserStatus = true, NiSize = "M" },
                new User { UserID = 8, Username = "User8", Password = "Password", Email = "user8@example.com", PhoneNumber = "0987654321", Address = "Address2", RoleID = 5, UserStatus = true, NiSize = "M" },
                new User { UserID = 9, Username = "User9", Password = "Password", Email = "user9@example.com", PhoneNumber = "0987654321", Address = "Address2", RoleID = 5, UserStatus = true, NiSize = "M" },
                new User { UserID = 10, Username = "User10", Password = "Password", Email = "user10@example.com", PhoneNumber = "0987654321", Address = "Address2", RoleID = 5, UserStatus = true, NiSize = "M" }
            );




        }
    }

}
