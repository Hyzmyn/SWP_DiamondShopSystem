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
            modelBuilder.Entity<Discount>().HasData(
    new Discount { DiscountID = 1, DiscountCode = "CODE1", DiscountAmount = 10 },
    new Discount { DiscountID = 2, DiscountCode = "CODE2", DiscountAmount = 20 }
    // Thêm dữ liệu cho các bản ghi Discount khác tại đây
);

            modelBuilder.Entity<Feedback>().HasData(
        new Feedback { FeedbackID = 1, FeedbackText = "Good product" },
        new Feedback { FeedbackID = 2, FeedbackText = "Excellent service" }


            );

            modelBuilder.Entity<Gem>().HasData(
                new Gem { GemID = 1, GemName = "Ruby", GemCode = "CODE1", Fluorescence = "Strong Blue", Origin = "Sri Lanka", Polish = "Excellent", Proportion = "Excellent", Symmetry = "Excellent" },
                new Gem { GemID = 2, GemName = "Sapphire", GemCode = "CODE2", Fluorescence = "None", Origin = "Brazil", Polish = "Good", Proportion = "Good", Symmetry = "Good" }

            );

            modelBuilder.Entity<GemPriceList>().HasData(
                new GemPriceList { GemID = 1, Price = 100, Clarity = "VS1", Color = "Red", Cut = "Excellent", Origin = "Brazil" },
                new GemPriceList { GemID = 2, Price = 150, Clarity = "SI2", Color = "Blue", Cut = "Good", Origin = "India" }

            );

            modelBuilder.Entity<MaterialPriceList>().HasData(
                new MaterialPriceList { ID = -1 },
                new MaterialPriceList { ID = -2 }

            );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderID = 1, UserID = 1, Note = "Sample note" },
                new Order { OrderID = 2, UserID = 2, Note = "Another note" }

            );

            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { OrderDetailID = 1, OrderID = 1, ProductID = 1, NiSize = "Small" },
                new OrderDetail { OrderDetailID = 2, OrderID = 2, ProductID = 2, NiSize = "Large" }

            );
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductID = 1, ProductName = "Product1", CategoryID = 1, ImageUrl1 = "/images/product/1.jpg", ImageUrl2 = "/images/product/1.jpg", ProductCode = "PD001", ProductionCost = 250, PriceRate = 100 },
                new Product { ProductID = 2, ProductName = "Product2", CategoryID = 2, ImageUrl1 = "/images/product/2.jpg", ImageUrl2 = "/images/product/1.jpg", ProductCode = "PD002", ProductionCost = 30, PriceRate = 70 }



            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { CategoryID = 1, CategoryName = "Category1", CategoryType = "Category1" },
                new ProductCategory { CategoryID = 2, CategoryName = "Category 2", CategoryType = "Category2" }
            );

            modelBuilder.Entity<ProductGem>().HasData(
                new ProductGem { ProductID = 1, GemID = 1 },
                new ProductGem { ProductID = 2, GemID = 2 }

            );

            modelBuilder.Entity<ProductMaterial>().HasData(
                new ProductMaterial { MaterialID = 1 },
                new ProductMaterial { MaterialID = 2 }

            );

            modelBuilder.Entity<Warranty>().HasData(
                new Warranty { WarrantyID = 1, ProductID = 1, Instance = "Instance2" },
                new Warranty { WarrantyID = 2, ProductID = 2, Instance = "Instance1" }

            );



        }
    }

}
