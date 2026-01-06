using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Catalog.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Features.Products.GetProducts;

public class GetProductsHandler
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    private readonly CatalogDbContext _dbContext;

    public GetProductsHandler(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PageNumber ?? 1;
        var pageSize = query.PageSize ?? 10;

        var products = await _dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name) // Always order before paging!
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}