using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Repository;
using Mapster;

namespace Catalog.API.Features.Products.CreateProduct;

public class CreateProductHandler
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly CatalogDbContext _dbContext;

    public CreateProductHandler(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // 1. Map Command to Domain Entity
        // Note: You can use Mapster or manual mapping. 
        // For a rich domain model, manual mapping via a Factory method is often safer.
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // 2. Save to Database
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        // 3. Return Result
        return new CreateProductResult(product.Id);
    }
}