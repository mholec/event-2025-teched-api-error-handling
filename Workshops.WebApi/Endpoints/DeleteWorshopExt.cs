using Microsoft.AspNetCore.Http.HttpResults;
using Workshops.Data;
using Workshops.WebApi.Contracts;
using Workshops.WebApi.Contracts.Workshops;
using Workshops.WebApi.ErrorHandling.Exceptions;

namespace Workshops.WebApi.Endpoints;

public static class DeleteWorshopExt
{
    /// <summary>
    /// Odstranění workshopu
    /// https://workshopy.apidog.io/odstran%C4%9Bn%C3%AD-workshopu-16058896e0
    /// </summary>
    public static RouteHandlerBuilder DeleteWorkshop(this IEndpointRouteBuilder app)
    {
        return app.MapDelete("/workshops/{id:apid}", (string id, AppDbContext db) =>
        {
            var workshop = db.Courses.FirstOrDefault(x => x.Id == id);
            if (workshop == null)
            {
                if (id.StartsWith("work")) // demo (jako že existoval)
                {
                    return Results.Problem(statusCode:410, title:"Workshop has been deleted");
                }

                throw new ApiNotFoundException<Workshop>();
            }

            if (id == "work1055")
            {
                throw new Exception("Test exception for TechEd");
            }

            db.Courses.Remove(workshop);
            db.SaveChanges();

            return Results.NoContent();
        });
    }
}