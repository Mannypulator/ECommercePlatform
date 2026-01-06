using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Repository;
using Catalog.API.Models;

namespace Catalog.API.Features.Products.UpdateProduct;

public class UpdateProductHandler
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly CatalogDbContext _dbContext;

    public UpdateProductHandler(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        // 1. Find
        var product = await _dbContext.Products
            .FindAsync(new object[] { command.Id }, cancellationToken);

        // 2. Guard
        if (product is null)
        {
            throw new NotFoundException(nameof(Product), command.Id);
        }

        // 3. Update (Ideally via domain methods, but setters work for simple CRUD)
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        // 4. Save
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}