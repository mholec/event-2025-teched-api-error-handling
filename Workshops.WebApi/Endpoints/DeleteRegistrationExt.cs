using Microsoft.AspNetCore.Http.HttpResults;
using Workshops.Data;
using Workshops.WebApi.Contracts;
using Workshops.WebApi.Contracts.Registrations;
using Workshops.WebApi.ErrorHandling.Exceptions;

namespace Workshops.WebApi.Endpoints;

public static class DeleteRegistrationExt
{
    /// <summary>
    /// Odstranění registrace
    /// https://workshopy.apidog.io/odstran%C4%9Bn%C3%AD-registrace-16058924e0
    /// </summary>
    public static RouteHandlerBuilder DeleteRegistration(this IEndpointRouteBuilder app)
    {
        return app.MapDelete("/registrations/{id}", (Guid id, AppDbContext db) =>
        {
            var student = db.Students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                throw new ApiNotFoundException<Registration>();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Results.NoContent();
        });
    }
}