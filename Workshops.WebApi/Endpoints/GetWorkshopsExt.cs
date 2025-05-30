using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshops.Data;
using Workshops.WebApi.Contracts;
using Workshops.WebApi.Contracts.Workshops;
using Workshops.WebApi.Mapping;

namespace Workshops.WebApi.Endpoints;

public static class GetWorkshopsExt
{
    /// <summary>
    /// Seznam workshopu
    /// https://workshopy.apidog.io/seznam-workshop%C5%AF-16058779e0
    /// </summary>
    public static RouteHandlerBuilder GetWorkshops(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/workshops", (WorkshopsFilter filter, AppDbContext db) =>
        {
            var courses = db.Courses.AsNoTracking();

            if (!string.IsNullOrEmpty(filter.SearchQuery))
            {
                courses = courses.Where(x => x.Name.Contains(filter.SearchQuery));
            }

            var result = new CollectionResult<Workshop>()
            {
                TotalItems = courses.Count()
            };

            courses = filter.Sort?.ToLowerInvariant() switch
            {
                "+id" => courses.OrderBy(x => x.Id),
                "+slug" => courses.OrderBy(x => x.Slug),
                "+title" => courses.OrderBy(x => x.Name),
                "+startdate" => courses.OrderBy(x => x.StartDate),
                "+capacity" => courses.OrderBy(x => x.Capacity),
                "+price" => courses.OrderBy(x => x.Price),
                "-id" => courses.OrderByDescending(x => x.Id),
                "-slug" => courses.OrderByDescending(x => x.Slug),
                "-title" => courses.OrderByDescending(x => x.Name),
                "-startdate" => courses.OrderByDescending(x => x.StartDate),
                "-capacity" => courses.OrderByDescending(x => x.Capacity),
                "-price" => courses.OrderByDescending(x => x.Price),
                _ => courses
            };

            courses = courses.Skip(filter.Skip).Take(filter.Take);
            result.Items = courses.ToDto().ToList();

            return Results.Ok(result);
        });
    }
}