using Microsoft.EntityFrameworkCore;
using Workshops.Data;
using Workshops.WebApi.Contracts;
using Workshops.WebApi.Contracts.Workshops;
using Workshops.WebApi.ErrorHandling.Exceptions;
using Workshops.WebApi.Mapping;

namespace Workshops.WebApi.Endpoints;

public static class GetWorkshopExt
{
    /// <summary>
    /// Detail workshopu dle ID
    /// https://workshopy.apidog.io/detail-workshopu-16058859e0
    /// </summary>
    public static RouteHandlerBuilder GetWorkshop(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/workshops/{id:apid}", (string id, AppDbContext db) =>
        {
            var workshop = db.Courses
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ToDto()
                .FirstOrDefault();

            if (workshop == null)
            {
                if (id.StartsWith("work"))
                {
                    return Results.Problem(statusCode:410, title:"Workshop has been deleted");
                }

                throw new ApiNotFoundException<Workshop>();
            }

            return Results.Ok(workshop);
        }).WithName("GetWorkshopById");
    }


}