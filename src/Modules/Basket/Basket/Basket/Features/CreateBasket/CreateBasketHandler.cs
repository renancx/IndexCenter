using Basket.Basket.Entities;
using FluentValidation;

namespace Basket.Basket.Features.CreateBasket
{
    public record CreateBasketCommand(ShoppingCartDto ShoppingCart) : ICommand<CreateBasketResult>;

    public record CreateBasketResult(Guid Id);

    public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
    {
        public CreateBasketCommandValidator() 
        {
            RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("Username is Required");
        }
    }

    internal class CreateBasketHandler(BasketDbContext dbContext) : ICommandHandler<CreateBasketCommand, CreateBasketResult> 
    {
        public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = CreateNewBasket(command.ShoppingCart);

            dbContext.ShoppingCart.Add(shoppingCart);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateBasketResult(shoppingCart.Id);
        }

        private ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCartDto)
        {
            var newBasket = ShoppingCart.Create(
                Guid.NewGuid(),
                shoppingCartDto.UserName);

            shoppingCartDto.Items.ForEach(item =>
            {
                newBasket.AddItem(
                    item.ProductId,
                    item.Quantity,
                    item.Color,
                    item.Price,
                    item.ProductName);
            });

            return newBasket;
        }
    }
}
