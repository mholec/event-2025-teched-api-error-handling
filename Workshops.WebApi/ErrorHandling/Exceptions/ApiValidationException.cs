using FluentValidation.Results;
using Workshops.WebApi.Contracts.Problems;

namespace Workshops.WebApi.ErrorHandling.Exceptions;

public class ApiValidationException : Exception
{
    public ApiValidationError Error { get; set; }

    public ApiValidationException()
    {
    }

    public ApiValidationException(string message, string property = null) : base(message)
    {
        Error = new ApiValidationError(message, property);
    }

    public ApiValidationException(ValidationFailure failure) : base(failure.ErrorMessage)
    {
        Error = new ApiValidationError(failure.ErrorMessage, failure.PropertyName);
    }
}