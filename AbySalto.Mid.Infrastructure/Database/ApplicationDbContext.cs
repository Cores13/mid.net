using AbySalto.Mid.Domain.Entities;
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
        public virtual DbSet<CartProduct> CartProducts { get; set; }
        public virtual DbSet<UserFavorite> UserFavorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(a => a.Dimensions)
                .WithOne(a => a.Product)
                .HasForeignKey<Dimensions>(c => c.ProductId)
                .HasPrincipalKey<Product>(p => p.ApiId);

            modelBuilder.Entity<Product>()
                .HasOne(a => a.Meta)
                .WithOne(a => a.Product)
                .HasForeignKey<Meta>(c => c.ProductId)
                .HasPrincipalKey<Product>(p => p.ApiId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .HasPrincipalKey(p => p.ApiId);

            modelBuilder.Entity<CartProduct>()
                .Property(cp => cp.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(cp => cp.ProductId)
                .HasPrincipalKey(p => p.ApiId);

            modelBuilder.Entity<CartProduct>()
                .HasOne(ci => ci.Cart)
                .WithMany(p => p.Products)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<UserFavorite>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.UserFavorites)
                .HasForeignKey(ci => ci.ProductId)
                .HasPrincipalKey(p => p.ApiId);

            modelBuilder.Entity<UserFavorite>()
                .HasOne(ci => ci.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(ci => ci.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId);


            modelBuilder.Entity<User>().HasData(UserSeeder.Data);
        }
    }
}