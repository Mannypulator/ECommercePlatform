using BuildingBlocks.CQRS;
using FluentValidation;

namespace Catalog.API.Features.Products.DeleteProduct;

// 1. Command
public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

// 2. Result
public record DeleteProductResult(bool IsSuccess);

// 3. Validator
public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
    }
}