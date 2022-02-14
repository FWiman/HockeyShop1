using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HockeyShop1.Models
{
    public partial class HockeyShop1Context : DbContext
    {
        public HockeyShop1Context()
        {
        }

        public HockeyShop1Context(DbContextOptions<HockeyShop1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppinCart> ShoppinCarts { get; set; }
        public virtual DbSet<Transport> Transports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:newtonservertest.database.windows.net,1433;Initial Catalog=DemoDB;Persist Security Info=False;User ID=serveradmin;Password=Liverpool5487;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Finnish_Swedish_CI_AS");

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Zip Code");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders.CustomerId");

                entity.HasOne(d => d.Transport)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TransportId)
                    .HasConstraintName("FK_Orders.TransportId");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("Order Details");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Order Details.OrderId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Order Details.ProductId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Color)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ModelName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Products.BrandId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products.CategoryId");
            });

            modelBuilder.Entity<ShoppinCart>(entity =>
            {
                entity.ToTable("Shoppin Cart");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ShoppinCarts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Shoppin Cart.CustomerId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppinCarts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Shoppin Cart.ProductId");
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.ToTable("Transport");

                entity.Property(e => e.Method)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
