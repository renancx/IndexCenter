using Basket.Basket.Entities;
using Basket.Basket.Exceptions;

namespace Basket.Data.Repository
{
    public class BasketRepository(BasketDbContext dbContext) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            var query = dbContext.ShoppingCart
                .Include(x => x.Items)
                .Where(x => x.UserName == userName);

            if (asNoTracking)
            {
                query.AsNoTracking();
            }

            var basket = await query.SingleOrDefaultAsync(cancellationToken);

            return basket ?? throw new BasketNotFoundException(userName);
        }

        public async Task<ShoppingCart> CreateBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            dbContext.ShoppingCart.Add(basket);
            await dbContext.SaveChangesAsync(cancellationToken);

            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await GetBasket(userName, asNoTracking: false, cancellationToken);

            dbContext.ShoppingCart.Remove(basket);
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
