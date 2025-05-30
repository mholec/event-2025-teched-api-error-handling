using Microsoft.EntityFrameworkCore;
using Workshops.Data;
using Workshops.Data.Entities;
using Workshops.WebApi.Contracts;
using Workshops.WebApi.Contracts.Workshops;
using Workshops.WebApi.ErrorHandling;
using Workshops.WebApi.ErrorHandling.Exceptions;
using Workshops.WebApi.Mapping;

namespace Workshops.WebApi.Endpoints;

public static class UpdateWorkshopExt
{
    /// <summary>
    /// Aktualizace workshopu
    /// https://workshopy.apidog.io/aktualizace-workshopu-16058883e0
    /// </summary>
    public static RouteHandlerBuilder UpdateWorkshop(this IEndpointRouteBuilder app)
    {
        return app.MapPut("/workshops/{id:apid}", (string id, AppDbContext db, WorkshopUpdate? model, ContractValidator validator) =>
        {
            validator.EnsureValid(model);

            Course? course = db.Courses.FirstOrDefault(x => x.Id == id);

            if (course == null)
            {
                throw new ApiNotFoundException<Workshop>();
            }

            course.StartDate = model.StartDate;
            course.Price = model.Price;
            course.Capacity = model.Capacity;
            course.Name = model.Title;

            db.SaveChanges();

            var workshop = db.Courses
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ToDto().First();

            return Results.Ok(workshop);
        });
    }
}