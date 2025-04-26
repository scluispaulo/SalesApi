using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(item => item.Quantity)
            .LessThanOrEqualTo(20)
            .WithMessage("Cannot sell more than 20 items of the same product.");
    }   
}