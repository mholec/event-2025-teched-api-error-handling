using FluentValidation;

namespace Workshops.WebApi.Contracts.Workshops;

/// <summary>
/// Validační pravidla pro WorkshopCreate
/// </summary>
public class WorkshopCreateValidator : AbstractValidator<WorkshopCreate>
{
    public WorkshopCreateValidator()
    {
        RuleFor(x=> x.Slug).MaximumLength(300).WithMessage("Slug must not exceed 300 characters");
        RuleFor(x=> x.Title).MaximumLength(300).WithMessage("Title must not exceed 300 characters");
        RuleFor(x => x.Capacity).InclusiveBetween(0, 100).WithMessage("Capacity must be between 0 and 100");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.StartDate).Must(BeInFuture).WithMessage("Start date must be in the future");
    }

    private bool BeInFuture(DateTime date)
    {
        if (date < DateTime.Now)
        {
            return false;
        }

        return true;
    }
}