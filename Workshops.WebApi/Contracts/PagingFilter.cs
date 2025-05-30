using System.Reflection;
using Workshops.WebApi.ErrorHandling.Exceptions;

namespace Workshops.WebApi.Contracts;

/// <summary>
/// Třída pro stránkování... je dále používána například ve WorkshopsFilter
/// </summary>
public class PagingFilter : IBindableFromHttpContext<PagingFilter>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public int Take => PageSize;
    public int Skip => (Page - 1) * Take;


    public static async ValueTask<PagingFilter> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        context.Request.Query.TryGetValue("page", out var page);
        context.Request.Query.TryGetValue("pageSize", out var pageSize);

        var paging = new PagingFilter();
        if (int.TryParse(page, out int pageX))
        {
            paging.Page = pageX;
        }

        if (int.TryParse(pageSize, out int pageSizeX))
        {
            paging.PageSize = pageSizeX;
        }
        else
        {
            throw new ApiValidationException("Špatný stránkování", nameof(PageSize));
        }

        // else
        // {
        //     throw new ApiValidationException("Invalid value for PageSize", nameof(pageSize));
        // }

        return paging;
    }
}