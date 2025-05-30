using System.Text.RegularExpressions;

namespace Workshops.WebApi.Routing;

/// <summary>
/// Constraint pro validování ID typu APID (^[a-z0-9]{8}$)
/// </summary>
public partial class ApidRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        var routeValue = values.FirstOrDefault();
        return ApidRegex().IsMatch(routeValue.Value?.ToString() ?? "");
    }

    /// <summary>
    /// Regulární výraz pro validování APID
    /// </summary>
    [GeneratedRegex("^[a-z0-9]{8}$", RegexOptions.IgnoreCase, "cs-CZ")]
    private static partial Regex ApidRegex();
}