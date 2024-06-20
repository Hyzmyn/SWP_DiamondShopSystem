﻿using Microsoft.EntityFrameworkCore;
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
    using System.Data;

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

<<<<<<< HEAD
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(GetConnectionString());
=======
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
>>>>>>> c91c109d5753a80fdb3cf7a2134a38ae363fcadf

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
                .HasPrecision(12, 2);

            modelBuilder.Entity<Gem>()
                .Property(p => p.FourC)
                .HasPrecision(12, 2);

            modelBuilder.Entity<GemPriceList>()
                .Property(p => p.CaratWeight)
                .HasPrecision(8, 2);

            modelBuilder.Entity<GemPriceList>()
                .Property(p => p.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<MaterialPriceList>()
                .Property(p => p.BuyPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<MaterialPriceList>()
                .Property(p => p.SellPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Order>()
                .Property(p => p.TotalPrice)
                .HasPrecision(12, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(p => p.Price)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.PriceRate)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductionCost)
                .HasPrecision(12, 2);

            modelBuilder.Entity<ProductMaterial>()
                .Property(p => p.Weight)
                .HasPrecision(12, 2);

<<<<<<< HEAD
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
=======
            //modelBuilder.Entity<Role>().HasData(
            //	new Role { RoleID = 1, RoleName = "Customer" },
            //	new Role { RoleID = 2, RoleName = "SalesStaff" },
            //	new Role { RoleID = 3, RoleName = "Manager" },
            //	new Role { RoleID = 4, RoleName = "Admin" }
            //);

            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Username = "User1", Password = "123", Email = "user1@example.com", PhoneNumber = "1234567890", Address = "Address1", RoleID = 1, UserStatus = true, NiSize = "S" },
                new User { UserID = 2, Username = "User2", Password = "123", Email = "user2@example.com", PhoneNumber = "0987654321", Address = "Address2", RoleID = 2, UserStatus = true, NiSize = "M" },
                new User { UserID = 3, Username = "User3", Password = "123", Email = "user3@example.com", PhoneNumber = "0987654321", Address = "Address3", RoleID = 3, UserStatus = true, NiSize = "M" },
                new User { UserID = 4, Username = "User4", Password = "123", Email = "user4@example.com", PhoneNumber = "0987654321", Address = "Address4", RoleID = 4, UserStatus = true, NiSize = "M" },
                new User { UserID = 5, Username = "User5", Password = "123", Email = "user5@example.com", PhoneNumber = "0987654321", Address = "Address5", RoleID = 5, UserStatus = true, NiSize = "M" },
                new User { UserID = 6, Username = "User6", Password = "Password", Email = "user6@example.com", PhoneNumber = "0987654321", Address = "Address", RoleID = 5, UserStatus = true, NiSize = "M" },
                new User { UserID = 7, Username = "User7", Password = "Password", Email = "user7@example.com", PhoneNumber = "0987654321", Address = "Address", RoleID = 5, UserStatus = true, NiSize = "M" },
                new User { UserID = 8, Username = "User8", Password = "Password", Email = "user8@example.com", PhoneNumber = "0987654321", Address = "Address", RoleID = 5, UserStatus = true, NiSize = "M" },
                new User { UserID = 9, Username = "User9", Password = "Password", Email = "user9@example.com", PhoneNumber = "0987654321", Address = "Address", RoleID = 5, UserStatus = true, NiSize = "M" }
            );

            modelBuilder.Entity<Gem>().HasData(
                new Gem { GemID = 1, GemCode = "EMGR-001", GemName = "Emerald", Origin = "Brazil", FourC = 4.5m, Proportion = "Excellent", Polish = "Excellent", Symmetry = "Excellent", Fluorescence = "None", Active = true },
                new Gem { GemID = 2, GemCode = "SAPP-002", GemName = "Sapphire", Origin = "Australia", FourC = 3.8m, Proportion = "Very Good", Polish = "Excellent", Symmetry = "Very Good", Fluorescence = "Faint", Active = true },
                new Gem { GemID = 3, GemCode = "DIAM-003", GemName = "Diamond", Origin = "Canada", FourC = 4.9m, Proportion = "Ideal", Polish = "Ideal", Symmetry = "Ideal", Fluorescence = "None", Active = true },
                new Gem { GemID = 4, GemCode = "RUBY-004", GemName = "Ruby", Origin = "Russia", FourC = 3.2m, Proportion = "Excellent", Polish = "Very Good", Symmetry = "Excellent", Fluorescence = "Faint", Active = true }
            );

            modelBuilder.Entity<GemPriceList>().HasData(
                new GemPriceList { GemID = 1, Origin = "Brazil", CaratWeight = 2.05m, Color = "D", Clarity = "VVS1", Cut = "Brilliant", Price = 10000m, EffDate = new DateTime(2023, 6, 1) },
                new GemPriceList { GemID = 2, Origin = "Australia", CaratWeight = 1.8m, Color = "E", Clarity = "VS2", Cut = "Cushion", Price = 8500m, EffDate = new DateTime(2023, 7, 15) },
                new GemPriceList { GemID = 3, Origin = "Canada", CaratWeight = 3.02m, Color = "F", Clarity = "IF", Cut = "Round Brilliant", Price = 25000m, EffDate = new DateTime(2023, 8, 30) },
                new GemPriceList { GemID = 4, Origin = "Russia", CaratWeight = 1.2m, Color = "J", Clarity = "SI1", Cut = "Oval", Price = 6000m, EffDate = new DateTime(2023, 9, 1) }
            );

            modelBuilder.Entity<MaterialPriceList>().HasData(
                new MaterialPriceList { ID = 1, MaterialID = 1, BuyPrice = 18.75m, SellPrice = 25.0m, EffDate = new DateTime(2023, 5, 1) },
                new MaterialPriceList { ID = 2, MaterialID = 2, BuyPrice = 14.50m, SellPrice = 20.0m, EffDate = new DateTime(2023, 6, 1) },
                new MaterialPriceList { ID = 3, MaterialID = 3, BuyPrice = 16.25m, SellPrice = 22.0m, EffDate = new DateTime(2023, 7, 1) },
                new MaterialPriceList { ID = 4, MaterialID = 4, BuyPrice = 19.00m, SellPrice = 26.0m, EffDate = new DateTime(2023, 8, 1) }
            );
>>>>>>> c91c109d5753a80fdb3cf7a2134a38ae363fcadf



            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { CategoryID = 1, CategoryName = "Rings", CategoryType = "Jewelry", CategoryStatus = true },
                new ProductCategory { CategoryID = 2, CategoryName = "Necklaces", CategoryType = "Jewelry", CategoryStatus = true },
                new ProductCategory { CategoryID = 3, CategoryName = "Bracelets", CategoryType = "Jewelry", CategoryStatus = true }
            );


            modelBuilder.Entity<Product>().HasData(
                new Product { ProductID = 1, ProductCode = "P001", ProductName = "Diamond Necklace", ImageUrl1 = "", ImageUrl2 = "", GemID = 1, MaterialID = 1, CategoryID = 1, ProductionCost = 150.0m, PriceRate = 2.5m },
                new Product { ProductID = 2, ProductCode = "P002", ProductName = "Gold Ring", ImageUrl1 = "", ImageUrl2 = "", GemID = 2, MaterialID = 2, CategoryID = 2, ProductionCost = 100.0m, PriceRate = 2.0m },
                new Product { ProductID = 3, ProductCode = "P003", ProductName = "Emerald Bracelet", ImageUrl1 = "", ImageUrl2 = "", GemID = 3, MaterialID = 3, CategoryID = 3, ProductionCost = 120.0m, PriceRate = 2.2m },
                new Product { ProductID = 4, ProductCode = "P004", ProductName = "Silver Earrings", ImageUrl1 = "", ImageUrl2 = "", GemID = 4, MaterialID = 4, CategoryID = 3, ProductionCost = 80.0m, PriceRate = 1.8m }
            );

            modelBuilder.Entity<ProductMaterial>().HasData(
                new ProductMaterial { MaterialID = 1, Weight = 20.5m },
                new ProductMaterial { MaterialID = 2, Weight = 15.7m },
                new ProductMaterial { MaterialID = 3, Weight = 18.2m },
                new ProductMaterial { MaterialID = 4, Weight = 22.1m }
            );


            modelBuilder.Entity<ProductGem>().HasData(
                new ProductGem { ProductID = 1, GemID = 1 },
                new ProductGem { ProductID = 2, GemID = 2 },
                new ProductGem { ProductID = 3, GemID = 3 },
                new ProductGem { ProductID = 4, GemID = 1 }

            );


            modelBuilder.Entity<Order>().HasData(
                new Order { OrderID = 1, UserID = 1, TotalPrice = 175.0m, TimeOrder = new DateTime(2023, 6, 1), Note = "Express delivery", OrderStatus = true },
                new Order { OrderID = 2, UserID = 2, TotalPrice = 170.0m, TimeOrder = new DateTime(2023, 6, 5), Note = "Standard delivery", OrderStatus = true },
                new Order { OrderID = 3, UserID = 3, TotalPrice = 120.0m, TimeOrder = new DateTime(2023, 6, 10), Note = "Pickup in-store", OrderStatus = false },
                new Order { OrderID = 4, UserID = 1, TotalPrice = 90.0m, TimeOrder = new DateTime(2023, 6, 15), Note = "Express delivery", OrderStatus = true },
                new Order { OrderID = 5, UserID = 2, TotalPrice = 60.0m, TimeOrder = new DateTime(2023, 6, 20), Note = "Standard delivery", OrderStatus = true }
            );

            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { OrderDetailID = 1, OrderID = 1, ProductID = 1, Quantity = 2, Price = 50.0m, NiSize = "7" },
                new OrderDetail { OrderDetailID = 2, OrderID = 1, ProductID = 2, Quantity = 1, Price = 75.0m, NiSize = "8" },
                new OrderDetail { OrderDetailID = 3, OrderID = 2, ProductID = 3, Quantity = 3, Price = 40.0m, NiSize = "6" },
                new OrderDetail { OrderDetailID = 4, OrderID = 2, ProductID = 4, Quantity = 1, Price = 90.0m, NiSize = "9" }
            );

            modelBuilder.Entity<Warranty>().HasData(
                new Warranty { WarrantyID = 1, OrderID = 1, ProductID = 1, BuyDate = new DateTime(2023, 6, 15), EndDate = new DateTime(2024, 6, 15), Instance = "ACME-01", WarrantyStatus = true },
                new Warranty { WarrantyID = 2, OrderID = 2, ProductID = 2, BuyDate = new DateTime(2023, 7, 1), EndDate = new DateTime(2024, 7, 1), Instance = "TECH-02", WarrantyStatus = true },
                new Warranty { WarrantyID = 3, OrderID = 3, ProductID = 3, BuyDate = new DateTime(2023, 12, 1), EndDate = new DateTime(2024, 12, 1), Instance = "ELITE-03", WarrantyStatus = true },
                new Warranty { WarrantyID = 4, OrderID = 4, ProductID = 4, BuyDate = new DateTime(2023, 9, 15), EndDate = new DateTime(2024, 9, 15), Instance = "BASIC-04", WarrantyStatus = true }
            );

            modelBuilder.Entity<Discount>().HasData(
                new Discount { DiscountID = 1, OrderID = 1, DiscountCode = "SUMMERSALE10", DiscountAmount = 10.0m, DiscountDate = new DateTime(2023, 6, 15), DiscountStatus = true },
                new Discount { DiscountID = 2, OrderID = 2, DiscountCode = "NEWCUSTOMER15", DiscountAmount = 15.0m, DiscountDate = new DateTime(2023, 7, 1), DiscountStatus = true },
                new Discount { DiscountID = 3, OrderID = 3, DiscountCode = "HOLIDAYDISCOUNT20", DiscountAmount = 20.0m, DiscountDate = new DateTime(2023, 12, 1), DiscountStatus = true },
                new Discount { DiscountID = 4, OrderID = 4, DiscountCode = "SPECIALOFFER5", DiscountAmount = 5.0m, DiscountDate = new DateTime(2023, 9, 15), DiscountStatus = true }
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
