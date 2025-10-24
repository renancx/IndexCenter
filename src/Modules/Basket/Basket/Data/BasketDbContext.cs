using Basket.Basket.Entities;
using System.Reflection;

namespace Basket.Data
{
    public class BasketDbContext : DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options) { }

        public DbSet<ShoppingCart> ShoppingCart => Set<ShoppingCart>();
        public DbSet<ShoppingCartItem> ShoppingCartItem => Set<ShoppingCartItem>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Basket");

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
