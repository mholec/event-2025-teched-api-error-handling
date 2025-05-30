using System.Text.RegularExpressions;
using FluentValidation;

namespace Workshops.WebApi.Contracts.Registrations;

public class RegistrationsFilterValidator : AbstractValidator<RegistrationsFilter>
{
    public RegistrationsFilterValidator()
    {
        RuleFor(x => x.WorkshopId).NotEmpty().WithMessage("WorkshopId is required");
        RuleFor(x => x.WorkshopId).Must(BeApid).WithMessage("WorkshopId must be a valid ID");
    }

    private bool BeApid(string apid)
    {
        if (string.IsNullOrEmpty(apid))
        {
            return true; // skip
        }

        return Regex.IsMatch(apid, "^[a-z0-9]{8}$");
    }
}