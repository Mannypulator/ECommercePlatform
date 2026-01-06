using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Features.Products.GetProducts;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10)
    : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);