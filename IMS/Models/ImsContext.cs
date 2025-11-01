using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IMS.Models
{
    public partial class ImsContext : DbContext
    {
        public ImsContext()
        {
        }

        public ImsContext(DbContextOptions<ImsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<UserActivityLog> UserActivityLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // configuration handled in Program.cs
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryName).HasMaxLength(100);
            });

            // ✅ Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 2)");
            });

            // ✅ User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserPassword).HasMaxLength(100);
                entity.Property(e => e.UserRole).HasMaxLength(50);
            });

            // ✅ Vendor
            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.VendorId);
                entity.Property(e => e.ContactNo).HasMaxLength(50);
            });

            // ✅ UserActivityLog
            modelBuilder.Entity<UserActivityLog>(entity =>
            {
                entity.ToTable("UserActivityLog"); // 👈 add this line
                entity.HasKey(e => e.LogId);
                entity.Property(e => e.Username).HasMaxLength(100);
                entity.Property(e => e.Action).HasMaxLength(100);
                entity.Property(e => e.Controller).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
