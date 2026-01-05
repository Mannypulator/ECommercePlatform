using BuildingBlocks.CQRS;
using FluentValidation;

namespace Catalog.API.Features.Products.CreateProduct;

// 1. The Command Definition
// It returns a CreateProductResult (just the ID)
public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price)
    : ICommand<CreateProductResult>;

// 2. The Result Definition
public record CreateProductResult(Guid Id);

// 3. The Validator (Rules)
// This runs automatically because of our BuildingBlocks ValidationBehavior
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}