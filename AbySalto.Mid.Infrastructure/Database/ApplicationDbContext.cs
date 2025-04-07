using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Seeders;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.Entity<User>().HasData(UserSeeder.Data);
        }
    }
}