namespace Basket.DTOs
{
    public record ShoppingCartDto(Guid Id, string UserName, List<ShoppingCartItemDto> Items);
}
