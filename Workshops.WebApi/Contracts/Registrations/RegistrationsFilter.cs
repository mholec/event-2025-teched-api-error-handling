using System.Reflection;

namespace Workshops.WebApi.Contracts.Registrations;

/// <summary>
/// Kontrakt pro filtrování seznamu registrací
/// </summary>
public class RegistrationsFilter : PagingFilter, IBindableFromHttpContext<RegistrationsFilter>
{
    public string? WorkshopId { get; set; }

    public static async ValueTask<RegistrationsFilter> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        var filter = await PagingFilter.BindAsync(context, parameter);
        context.Request.Query.TryGetValue("workshopId", out var workshopId);

        return new RegistrationsFilter()
        {
            Page = filter.Page,
            PageSize = filter.PageSize,
            WorkshopId = workshopId
        };
    }
}