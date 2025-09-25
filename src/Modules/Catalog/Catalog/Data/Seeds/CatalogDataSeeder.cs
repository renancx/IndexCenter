using Shared.Data.Seeds;

namespace Catalog.Data.Seeds
{
    public class CatalogDataSeeder(CatalogDbContext dbContext) : IDataSeeder
    {
        public async Task SeedAllAsync()
        {
            if (!await dbContext.Product.AnyAsync())
            {
                await dbContext.Product.AddRangeAsync(InitialData.Products);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
