﻿using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Seeders;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AbySalto.Mid.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Dimensions> Dimensions { get; set; }
        public virtual DbSet<Meta> Metas { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(a => a.Dimensions)
                .WithOne(a => a.Product)
                .HasForeignKey<Dimensions>(c => c.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(a => a.Meta)
                .WithOne(a => a.Product)
                .HasForeignKey<Meta>(c => c.ProductId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<CartProduct>()
                .HasKey(ci => new { ci.CartId, ci.ProductId });

            modelBuilder.Entity<CartProduct>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<CartProduct>()
                .HasOne(ci => ci.Cart)
                .WithMany(p => p.Products)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId);


            modelBuilder.Entity<User>().HasData(UserSeeder.Data);
        }
    }
}