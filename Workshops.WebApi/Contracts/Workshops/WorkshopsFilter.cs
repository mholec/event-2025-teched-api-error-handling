using System.Reflection;
using FluentValidation.Results;
using Workshops.WebApi.ErrorHandling.Exceptions;

namespace Workshops.WebApi.Contracts.Workshops;

/// <summary>
/// Kontrakt pro filtrování seznamu workshopů
/// Slouží pro mapování query parameterů
/// </summary>
public class WorkshopsFilter : PagingFilter, IBindableFromHttpContext<WorkshopsFilter>
{
    public string? SearchQuery { get; set; }
    public string? Sort { get; set; }

    public static async ValueTask<WorkshopsFilter> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        var filter = await PagingFilter.BindAsync(context, parameter);
        context.Request.Query.TryGetValue("sort", out var sort);

        var ordercheck = sort.FirstOrDefault() ?? "+";

        if (!(ordercheck[0] == '+' || ordercheck[0] == '-'))
        {
            var failure = new ValidationFailure("sort", $"The sort parameter must start with '+' or '-'.");
            throw new ApiValidationException(failure);
        }

        context.Request.Query.TryGetValue("searchQuery", out var searchQuery);

        return new WorkshopsFilter
        {
            Page = filter.Page,
            PageSize = filter.PageSize,
            Sort = sort,
            SearchQuery = searchQuery
        };
    }
}