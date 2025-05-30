using Microsoft.AspNetCore.Diagnostics;
using Workshops.WebApi.Contracts.Problems;
using Workshops.WebApi.ErrorHandling.Exceptions;

namespace Workshops.WebApi.ErrorHandling.Handlers;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken ctn)
    {
        if (ex is not ApiNotFoundException exception)
        {
            return false;
        }

        var error = new ApiProblem()
        {
            Title = exception.Message,
            Status = StatusCodes.Status404NotFound
        };

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        await httpContext.Response.WriteAsJsonAsync(error, ctn);
        return true;
    }
}