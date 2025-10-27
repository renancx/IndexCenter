using Basket.Basket.Exceptions;
using Mapster;

namespace Basket.Basket.Features.GetBasket
{
    public record GetbasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCartDto ShoppingCart);

    internal class GetBasketHandler(BasketDbContext dbContext) : IQueryHandler<GetbasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetbasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await dbContext.ShoppingCart
                .AsNoTracking()
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.UserName == query.UserName, cancellationToken);

            if (basket is null)
            {
                throw new BasketNotFoundException(query.UserName);
            }

            var basketDto = basket.Adapt<ShoppingCartDto>();
             
            return new GetBasketResult(basketDto);
        }
    }
}
