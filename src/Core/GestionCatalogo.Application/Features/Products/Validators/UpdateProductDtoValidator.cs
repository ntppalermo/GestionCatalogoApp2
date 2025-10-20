using FluentValidation;
using GestionCatalogo.Application.Features.Products.DTOs;

namespace GestionCatalogo.Application.Features.Products.Validators;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
		RuleFor(x => x.Name)
	  .NotEmpty().WithMessage("Name is required")
	  .Length(2, 100).WithMessage("Name must be between 2 and 100 characters");

		RuleFor(x => x.Description)
			.MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

		RuleFor(x => x.Price)
			.GreaterThan(0).WithMessage("Price must be greater than 0")
			.LessThan(999999.99m).WithMessage("Price cannot exceed 999,999.99");

		RuleFor(x => x.Stock)
			.GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");

		RuleFor(x => x.Category)
			.MaximumLength(50).WithMessage("Category cannot exceed 50 characters");

		RuleFor(x => x.Brand)
			.MaximumLength(100).WithMessage("Brand cannot exceed 100 characters");

		RuleFor(x => x.SKU)
			.MaximumLength(50).WithMessage("SKU cannot exceed 50 characters");

	}
}
