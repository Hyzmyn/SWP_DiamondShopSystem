using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository.Repositories;

public partial class DiamondShopContext : DbContext
{
    public DiamondShopContext()
    {
    }

    public DiamondShopContext(DbContextOptions<DiamondShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<FeedBack> FeedBacks { get; set; }

    public virtual DbSet<Gem> Gems { get; set; }

    public virtual DbSet<GemPriceList> GemPriceLists { get; set; }

    public virtual DbSet<MaterialPriceList> MaterialPriceLists { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductGem> ProductGems { get; set; }

    public virtual DbSet<ProductMaterial> ProductMaterials { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Warranty> Warranties { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog= DiamondShop;User ID=sa;Password=sa123456;Trusted_Connection=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Contacts__C3905BAF7EB5ABE4");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.AddressDelivery)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumberDelivery)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF68FF28653");

            entity.Property(e => e.DiscountId)
                .ValueGeneratedNever()
                .HasColumnName("DiscountID");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DiscountCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Order).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Discounts__Order__5CD6CB2B");
        });

        modelBuilder.Entity<FeedBack>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__FeedBack__6A4BEDF61E7C6699");

            entity.ToTable("FeedBack");

            entity.Property(e => e.FeedbackId)
                .ValueGeneratedNever()
                .HasColumnName("FeedbackID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.FeedbackText).HasColumnType("text");
            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.FeedBacks)
                .HasForeignKey(d => d.OrderDetailId)
                .HasConstraintName("FK__FeedBack__OrderD__5FB337D6");

            entity.HasOne(d => d.User).WithMany(p => p.FeedBacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__FeedBack__UserID__60A75C0F");
        });

        modelBuilder.Entity<Gem>(entity =>
        {
            entity.HasKey(e => e.GemId).HasName("PK__Gem__F101D5A010D0C25A");

            entity.ToTable("Gem");

            entity.Property(e => e.GemId)
                .ValueGeneratedNever()
                .HasColumnName("GemID");
            entity.Property(e => e.Fluorescence)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FourC).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.GemCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GemName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Origin)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Polish)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Proportion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Symmetry)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GemPriceList>(entity =>
        {
            entity.HasKey(e => new { e.GemId, e.EffDate }).HasName("PK__GemPrice__6CE7B5BC896B6D2B");

            entity.ToTable("GemPriceList");

            entity.HasIndex(e => e.GemId, "UQ__GemPrice__F101D5A10F5C96A8").IsUnique();

            entity.Property(e => e.GemId).HasColumnName("GemID");
            entity.Property(e => e.CaratWeight).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Clarity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cut)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Origin)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Gem).WithOne(p => p.GemPriceList)
                .HasForeignKey<GemPriceList>(d => d.GemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GemPriceL__GemID__4F7CD00D");
        });

        modelBuilder.Entity<MaterialPriceList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC27F9D27138");

            entity.ToTable("MaterialPriceList");

            entity.HasIndex(e => e.MaterialId, "UQ__Material__C5061316F66301F5").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BuyPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MaterialId).HasColumnName("MaterialID");
            entity.Property(e => e.SellPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Material).WithOne(p => p.MaterialPriceList)
                .HasPrincipalKey<ProductMaterial>(p => p.MaterialId)
                .HasForeignKey<MaterialPriceList>(d => d.MaterialId)
                .HasConstraintName("FK__MaterialP__EffDa__4BAC3F29");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFC10DE8D9");

            entity.HasIndex(e => e.OrderDetailId, "UQ__Orders__D3B9D30DD438FE4F").IsUnique();

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.Note).HasColumnType("text");
            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.TimeOrder).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.OrderNavigation).WithOne(p => p.Order)
                .HasForeignKey<Order>(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__OrderID__5629CD9C");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__UserID__5535A963");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30C01390F85");

            entity.Property(e => e.OrderDetailId)
                .ValueGeneratedNever()
                .HasColumnName("OrderDetailID");
            entity.Property(e => e.NiSize)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__59063A47");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderDeta__Produ__59FA5E80");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6ED3E10F3D0");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.GemCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MaterialCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PriceRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductionCost).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product__Categor__412EB0B6");

            entity.HasOne(d => d.ProductNavigation).WithOne(p => p.Product)
                .HasForeignKey<Product>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Product__4222D4EF");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__ProductC__19093A2BCCC83A7A");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CategoryType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductGem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductGem");

            entity.HasIndex(e => e.ProductId, "UQ__ProductG__B40CC6EC854B66F9").IsUnique();

            entity.Property(e => e.GemId).HasColumnName("GemID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Gem).WithMany()
                .HasForeignKey(d => d.GemId)
                .HasConstraintName("FK__ProductGe__GemID__47DBAE45");

            entity.HasOne(d => d.Product).WithOne()
                .HasForeignKey<ProductGem>(d => d.ProductId)
                .HasConstraintName("FK__ProductGe__Produ__46E78A0C");
        });

        modelBuilder.Entity<ProductMaterial>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__ProductM__B40CC6EDDA3A3D9E");

            entity.ToTable("ProductMaterial");

            entity.HasIndex(e => e.MaterialId, "UQ__ProductM__C5061316D83A64E1").IsUnique();

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("ProductID");
            entity.Property(e => e.MaterialId)
                .IsRequired()
                .HasColumnName("MaterialID");
            entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A3A0ED669");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC2FE56CB5");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NiSize)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__398D8EEE");
        });

        modelBuilder.Entity<Warranty>(entity =>
        {
            entity.HasKey(e => e.WarrantyId).HasName("PK__Warranty__2ED318F351F9CC70");

            entity.ToTable("Warranty");

            entity.Property(e => e.WarrantyId)
                .ValueGeneratedNever()
                .HasColumnName("WarrantyID");
            entity.Property(e => e.Instance)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.Warranties)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Warranty__OrderI__6383C8BA");

            entity.HasOne(d => d.Product).WithMany(p => p.Warranties)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Warranty__Produc__6477ECF3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
