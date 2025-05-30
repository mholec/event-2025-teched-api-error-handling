using System.Net.Http.Headers;
using Workshops.WebApi.Contracts.Problems;

namespace Workshops.WebApi.ErrorHandling.Filters;

public class MediaTypeFilter : IEndpointFilter
{
    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        // var contentType = context.HttpContext.Request.Headers["Content-Type"].ToString();
        // if (string.IsNullOrEmpty(contentType))
        // {
        //     contentType = "application/json";
        // }
        //
        // if (contentType != "application/json")
        // {
        //     context.HttpContext.Response.StatusCode = 415;
        //     context.HttpContext.Response.Headers.Append("Accept", "application/json");
        //
        //     return new ApiProblem()
        //     {
        //         Title = "Unsupported Media Type",
        //         Status = 415,
        //         Detail = "Supported media type must be application/json",
        //     };
        // }


        var accept = context.HttpContext.Request.Headers.Accept.ToString();

        // akceptujeme nevyplněný Accept header (zvážit, zda není lepší vyhodit také 406)
        if (string.IsNullOrEmpty(accept))
        {
            accept = "*/*";
        }

        var mediaTypes = accept.Split(',')
            .Select(MediaTypeWithQualityHeaderValue.Parse)
            .OrderByDescending(mt => mt.Quality ?? 1);

        foreach (var mediaType in mediaTypes)
        {
            if (mediaType.MediaType is "*/*" or "application/json")
            {
                return await next(context);
            }
        }

        context.HttpContext.Response.StatusCode = 406;

        return new ApiProblem()
        {
            Title = "Not Acceptable",
            Status = 406,
            Detail = "Supported media type must be application/json or */*.",
        };
    }
}