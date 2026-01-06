using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.API.Models;
using Catalog.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Features.Products.GetProductById;

public class GetProductByIdHandler
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly CatalogDbContext _dbContext;

    public GetProductByIdHandler(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .AsNoTracking() // Optimization for Read-Only queries
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (product is null)
        {
            throw new NotFoundException(nameof(Product), query.Id);
        }

        return new GetProductByIdResult(product);
    }
}