using Microsoft.AspNetCore.Routing;

namespace Basket.Basket.Features.CreateBasket
{
    public record CreateBasketRequest(ShoppingCartDto ShoppingCart);
    public record CreateBasetResponse(Guid Id);


    public class CreateBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}
