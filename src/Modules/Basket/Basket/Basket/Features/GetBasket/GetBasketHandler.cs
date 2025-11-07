using Basket.Data.Repository;
using Mapster;

namespace Basket.Basket.Features.GetBasket
{
    public record GetbasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCartDto ShoppingCart);

    internal class GetBasketHandler(IBasketRepository basketRepository) : IQueryHandler<GetbasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetbasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(query.UserName, asNoTracking: true, cancellationToken);

            var basketDto = basket.Adapt<ShoppingCartDto>();
             
            return new GetBasketResult(basketDto);
        }
    }
}
