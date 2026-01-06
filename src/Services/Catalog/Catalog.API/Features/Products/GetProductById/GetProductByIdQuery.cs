using System;
using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Features.Products.GetProductById;

// 1. Query
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

// 2. Result
public record GetProductByIdResult(Product Product);
