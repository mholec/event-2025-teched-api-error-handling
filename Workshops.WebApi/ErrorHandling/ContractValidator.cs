using FluentValidation;
using Workshops.WebApi.Contracts.Problems;
using Workshops.WebApi.ErrorHandling.Exceptions;

namespace Workshops.WebApi.ErrorHandling;

/// <summary>
/// Validační wrapper pro kontrakty
/// </summary>
public class ContractValidator(IServiceProvider sp)
{
    public void EnsureValid<T>(T? contract)
    {
        var validationState = sp.GetService<RequestValidationState>();

        if (contract is null)
        {
            throw new ApiEmptyBodyException();
        }

        var validators = sp.GetServices<IValidator<T>>();

        foreach (var validator in validators)
        {
            var result = validator.Validate(contract);

            if (!result.IsValid)
            {
                validationState.AddErrors(result.Errors.Select(x => new ApiValidationError(x.ErrorMessage, x.PropertyName)).ToList());
            }
        }

        if (validationState.Errors.Any())
        {
            throw new ApiValidationException();
        }
    }
}