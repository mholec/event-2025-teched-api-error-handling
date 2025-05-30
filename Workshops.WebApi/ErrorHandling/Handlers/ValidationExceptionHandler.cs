using Microsoft.AspNetCore.Diagnostics;
using Workshops.WebApi.Contracts.Problems;
using Workshops.WebApi.ErrorHandling.Exceptions;

namespace Workshops.WebApi.ErrorHandling.Handlers;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken ctn)
    {
        if (ex is not ApiValidationException exception)
        {
            return false;
        }

        var validationState = httpContext.RequestServices.GetService<RequestValidationState>();
        if (exception.Error != null)
        {
            validationState.AddError(exception.Error);
        }

        var error = new ApiValidationProblem(validationState.Errors.ToList())
        {
            Title = "Validation Failed",
            Status = StatusCodes.Status400BadRequest,
            Detail = "See errors field for details."
        };

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(error, ctn);
        return true;
    }
}