using Microsoft.AspNetCore.Diagnostics;
using Workshops.WebApi.Contracts.Problems;
using Workshops.WebApi.ErrorHandling.Exceptions;

namespace Workshops.WebApi.ErrorHandling.Handlers;

public class EmptyBodyExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken ctn)
    {
        if (ex is not ApiEmptyBodyException exception)
        {
            return false;
        }

        var error = new ApiProblem
        {
            Title = "Request body is required",
            Status = StatusCodes.Status400BadRequest
        };

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(error, ctn);
        return true;
    }
}