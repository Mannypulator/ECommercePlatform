using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Repository;
using Catalog.API.Models;

namespace Catalog.API.Features.Products.DeleteProduct;

public class DeleteProductHandler
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly CatalogDbContext _dbContext;

    public DeleteProductHandler(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FindAsync(new object[] { command.Id }, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product), command.Id);
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}