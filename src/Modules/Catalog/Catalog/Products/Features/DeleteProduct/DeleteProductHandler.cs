
namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid Id)
        : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);

    internal class DeleteProductHandler(CatalogDbContext dbContext)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await dbContext.Product.FindAsync([command.Id], cancellationToken);

            if (product is null)
                throw new Exception($"Product not found: {command.Id}");

            dbContext.Product.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
