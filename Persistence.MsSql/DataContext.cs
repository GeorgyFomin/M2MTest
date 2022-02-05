using Entities.Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.MsSql
{
    public class DataContext : DbContext
    {
        public DbSet<Product>? Products { get; set; }
        public DbSet<Ingredient>? Ingredients { get; set; }
        public DataContext() { }
        public DataContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductIngredient>().HasKey(k => new { k.IngredientId, k.ProductId });
        }

    }
}